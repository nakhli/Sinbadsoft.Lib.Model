// <copyright file="ObjectConversionHelper.cs" company="Sinbadsoft">
// Copyright (c) Chaker Nakhli 2010-2013
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at http://www.apache.org/licenses/LICENSE-2.0 Unless required by 
// applicable law or agreed to in writing, software distributed under the License
// is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing
// permissions and limitations under the License.
// </copyright>
// <author>Chaker Nakhli</author>
// <email>chaker.nakhli@sinbadsoft.com</email>
// <date>2013/01/20</date>
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;

namespace Sinbadsoft.Lib.Model
{
    internal class ObjectConversionHelper
    {
        public static void ProcessProperties(
            object obj,
            Action<string, object> process,
            Func<string, bool> filter = null)
        {
            filter = filter ?? (key => true);
            var objType = obj.GetType();

            if (objType == typeof(NameValueCollection))
            {
                var collection = (NameValueCollection)obj;   
                foreach (string key in collection)
                {
                    if (filter(key))
                    {
                        process(key, collection[key]);
                    }
                }
            }
            else if (obj is IDataRecord)
            {
                var reader = (IDataRecord)obj;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var property = reader.GetName(i);
                    if (filter(property))
                    {
                        var value = reader.GetValue(i);
                        process(property, value);
                    }
                }
            } 
            else if (obj is IDictionary
                || ImplementsRawGeneric(typeof(IDictionary<,>), objType))
            {
                dynamic dictionary = obj;
                foreach (var pair in dictionary)
                {
                    var key = pair.Key.ToString();
                    if (filter(key))
                    {
                        process(key, pair.Value);
                    }
                }
            }
            else
            {
                var properties = obj.GetType()
                    .GetProperties()
                    .Where(p => p.CanRead && p.GetIndexParameters().Length == 0 && filter(p.Name));
                foreach (var property in properties)
                {
                    process(property.Name, property.GetValue(obj, null));
                }
            }
        }

        private static bool ImplementsRawGeneric(Type generic, Type toCheck)
        {
            Type[] interfaces = toCheck.GetInterfaces();
            return interfaces
                .Select(interfaceType => interfaceType.IsGenericType ? interfaceType.GetGenericTypeDefinition() : interfaceType)
                .Any(current => generic == current);
        }
    }
}