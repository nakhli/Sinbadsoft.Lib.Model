// <copyright file="ConvertAssert.cs" company="Sinbadsoft">
// Copyright (c) Chaker Nakhli 2012
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
// <date>2012/03/24</date>

using System;

using NUnit.Framework;

namespace Sinbadsoft.Lib.Model.Tests
{
    public static class ConvertAssert
    {
        public static void IsConvertedTo<TSource, TTarget>(TSource value, TTarget expected, IFormatProvider format = null)
        {
            object result;
            ValueConverter.TryConvert(value, typeof(TTarget), out result, format);
            TTarget convertedValue = default(TTarget);
            Assert.DoesNotThrow(() => convertedValue = (TTarget)result);
            Assert.AreEqual(expected, convertedValue);
        }
    }
}