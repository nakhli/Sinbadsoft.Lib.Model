// <copyright file="ToExpandoExtension.cs" company="Sinbadsoft">
// Copyright (c) Chaker Nakhli 2010-2012
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
// <date>2010/11/04</date>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Dynamic;
using System.Linq;

namespace Sinbadsoft.Lib.Model.ToExpando
{
    public static class ToExpandoExtension
    {
        /// <summary>
        /// Turns the given object <paramref name="obj"/> into an <see cref="ExpandoObject"/>.
        /// If <paramref name="obj"/> is:
        /// <list type="bullet">
        /// <item><description>
        /// <see langword="null"/> then a new empty <see cref="ExpandoObject"/> is returned.
        /// </description></item>
        /// <item><description>
        /// <see cref="NameValueCollection"/>, <see cref="IDictionary"/>, <see cref="ExpandoObject"/> or any
        /// <see cref="IDictionary{TKey,TValue}"/> then a new expando object with the same source key-value pairs 
        /// is created and returned. Keys are converted to strings if they are not of type <see cref="String"/>.
        /// </description></item>
        /// <item><description>
        /// <see cref="IDataRecord"/> then a new expando objet with the same column name-value pair of the current rader
        /// row is created and returned. All <see cref="DBNull"/> values are converted to <see langword="null"/> or to the default
        /// value of the hint type if a <paramref name="typeHint"/> parameter is provided.
        /// </description></item>
        /// </list>
        /// </summary>
        /// <param name="obj">The source object to convert to an expando object.</param>
        /// <param name="typeHint">Object or dictionary with values of type <see cref="Type"/>.</param>
        /// <param name="whitelist">Exhaustive set of properties to include.</param>
        /// <param name="blacklist">Poperties to exclude.</param>
        public static ExpandoObject ToExpando(
            this object obj,
            object typeHint = null,
            IEnumerable<string> whitelist = null,
            IEnumerable<string> blacklist = null)
        {
            if (obj == null)
            {
                return new ExpandoObject();
            }

            var includeLookup = whitelist == null ? null : whitelist.ToLookup(x => x);
            var excludeLookup = blacklist == null ? null : blacklist.ToLookup(x => x);

            if (whitelist != null && blacklist != null && includeLookup.Any() && includeLookup.Any())
            {
                // Slicing can't be done by both including *and* excluding properties
                throw new InvalidOperationException("Slicing is done using either a white list or a black list");
            }

            var types = typeHint == null
                ? null
                : typeHint.ToExpando().Where(p => p.Value is Type).ToDictionary(p => p.Key, p => (Type)p.Value);

            var result = new ExpandoObject();
            var resultDictionary = result as IDictionary<string, object>;

            ObjectConversionHelper.ProcessProperties(
                obj,
                (key, val) => resultDictionary[key] = ChangeType(key, val, types),
                key => FilterProperty(includeLookup, excludeLookup, key));
            return result;
        }

        private static bool FilterProperty(ILookup<string, string> includeLookup, ILookup<string, string> excludeLookup, string key)
        {
            return (includeLookup == null || includeLookup.Contains(key))
                   && (excludeLookup == null || !excludeLookup.Contains(key));
        }

        private static object ChangeType(string property, object value, IDictionary<string, Type> types)
        {
            Type targetType;
            object convertedValue;
            return types != null
                   && types.TryGetValue(property, out targetType)
                   && ValueConverter.TryConvert(value, targetType, out convertedValue) ? convertedValue : value;
        }
    }
}