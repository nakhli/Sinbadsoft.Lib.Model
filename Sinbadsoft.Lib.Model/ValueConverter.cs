// <copyright file="ValueConverter.cs" company="Sinbadsoft">
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
using System.ComponentModel;

namespace Sinbadsoft.Lib.Model
{
    public static class ValueConverter
    {
        public static bool TryConvert<T>(object value, out T result, IFormatProvider format = null)
        {
            object obj;
            if (TryConvert(value, typeof(T), out obj, format))
            {
                result = (T)obj;
                return true;
            }

            result = default(T);
            return false;
        }

        public static bool TryConvert(object value, Type targetType, out object result, IFormatProvider format = null)
        {
            result = targetType.GetDefault();
            if (value == null || value == DBNull.Value)
            {
                return true;
            }

            Type sourceType = value.GetType();
            if (targetType == sourceType || targetType.IsAssignableFrom(sourceType))
            {
                result = value;
                return true;
            }

            if (targetType == typeof(Guid))
            {
                return TryConvertToGuid(value, ref result);
            }

            if (targetType == typeof(byte[]) && sourceType == typeof(Guid))
            {
                result = ((Guid)value).ToByteArray();
                return true;
            }

            if (targetType == typeof(TimeSpan) && sourceType == typeof(string))
            {
                TimeSpan timeSpan;
                if (!TimeSpan.TryParse((string)value, format, out timeSpan))
                {
                    return false;
                }

                result = timeSpan;
                return true;
            }

            if (targetType == typeof(string) && sourceType == typeof(TimeSpan))
            {
                result = ((TimeSpan)value).ToString();
                return true;
            }

            if (targetType == typeof(long) && sourceType == typeof(DateTime))
            {
                result = ((DateTime)value).ToBinary();
                return true;
            }

            if (targetType == typeof(DateTime) && sourceType == typeof(long))
            {
                result = DateTime.FromBinary((long)value);
                return true;
            }

            if (targetType.IsEnum)
            {
                return TryConvertToEnum(value, targetType, ref result);
            }

            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return TryConvertToNullable(value, targetType, ref result);
            }

            return TryConvertDefault(value, targetType, ref result, format);
        }

        private static bool TryConvertToNullable(object value, Type targetType, ref object result)
        {
            var nullableConverter = new NullableConverter(targetType);
            if (nullableConverter.CanConvertFrom(value.GetType()))
            {
                result = nullableConverter.ConvertFrom(value);
                return true;
            }

            return false;
        }

        private static bool TryConvertToGuid(object value, ref object result)
        {
            if (value.GetType() == typeof(string))
            {
                var stringValue = (string)value;
                Guid guidValue;
                if (Guid.TryParse(stringValue, out guidValue))
                {
                    result = guidValue;
                    return true;
                }

                return false;
            }

            if (value.GetType() == typeof(byte[]))
            {
                var bytes = (byte[])value;
                if (bytes.Length == 16)
                {
                    try
                    {
                        result = new Guid(bytes);
                        return true;
                    }
                    catch (ArgumentException)
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        private static bool TryConvertToEnum(object value, Type enumType, ref object result)
        {
            try
            {
                result = value.GetType() == typeof(string)
                    ? Enum.Parse(enumType, (string)value)
                    : Enum.ToObject(enumType, value);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        private static bool TryConvertDefault(object value, Type targetType, ref object result, IFormatProvider format = null)
        {
            try
            {
                if (targetType == typeof(string))
                {
                    result = Convert.ToString(value, format);
                    return true;
                }

                result = Convert.ChangeType(value, targetType, format);
                return true;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }

        private static object GetDefault(this Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}