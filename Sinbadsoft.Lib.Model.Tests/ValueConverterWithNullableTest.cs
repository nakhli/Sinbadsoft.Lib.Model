// <copyright file="ValueConverterWithNullableTest.cs" company="Sinbadsoft">
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
// <date>2012/03/11</date>
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Sinbadsoft.Lib.Model.Tests
{
    [TestFixture]
    public class ValueConverterWithNullableTest
    {
        [Test]
        public void IntNullableToNullable()
        {
            ConvertAssert.IsConvertedTo<int?, int?>(5, 5);   
        }

        [Test]
        public void IntToAndFromNullable()
        {
            CheckConvertToAndFromNullable(3);
        }

        [Test]
        public void DateTimeToAndFromNullable()
        {
            CheckConvertToAndFromNullable(new DateTime(1978, 10, 11));
        }

        [Test]
        public void DoubleToAndFromNullable()
        {
            CheckConvertToAndFromNullable(3.14);
        }

        [Test]
        public void KeyValuePairToAndFromNullable()
        {
            CheckConvertToAndFromNullable(new KeyValuePair<int, object>(14, new object()));
        }

        private static void CheckConvertToAndFromNullable<T>(T value) where T : struct
        {
            ConvertAssert.IsConvertedTo<T, T?>(value, value);
            ConvertAssert.IsConvertedTo<T?, T>(value, value);
        }
    }
}