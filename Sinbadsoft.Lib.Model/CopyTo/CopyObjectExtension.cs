// <copyright file="CopyObjectExtension.cs" company="Sinbadsoft">
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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sinbadsoft.Lib.Model.CopyTo
{
    public static class CopyObjectExtension
    {
        public static T CopyTo<T>(this object source, T target)
        {
            if (source == null || target == null)
            {
                return target;
            }

            const BindingFlags Bindings = BindingFlags.Instance | BindingFlags.Public;
            var targetProperties = typeof(T)
                .GetProperties(Bindings)
                .Where(p => p.CanWrite && p.GetIndexParameters().Length == 0)
                .ToDictionary(p => p.Name, p => p);

            ObjectConversionHelper.ProcessProperties(
                source,
                (key, val) =>
                    {
                        PropertyInfo targetProperty;
                        if (!targetProperties.TryGetValue(key, out targetProperty))
                        {
                            return;
                        }
                        
                        object targetValue;
                        if (ValueConverter.TryConvert(val, targetProperty.PropertyType, out targetValue))
                        {
                            targetProperty.SetValue(target, targetValue, null);
                        }
                    });

            return target;
        }

        public static T CopyTo<T>(this object source) where T : new()
        {
            return source.CopyTo(source == null ? default(T) : new T());
        }

        public static IEnumerable<TResult> CopyAll<TResult>(this IEnumerable source)
            where TResult : new()
        {
            foreach (object element in source)
            {
                yield return CopyTo<TResult>(element);
            }
        }
    }
}
