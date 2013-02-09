// <copyright file="ValueConverterTest.cs" company="Sinbadsoft">
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
using System.Globalization;
using System.Linq;

using NUnit.Framework;

namespace Sinbadsoft.Lib.Model.Tests
{
    [TestFixture]
    public class ValueConverterTest
    {
        [Test]
        public void IntToAndFromLong()
        {
            CheckConvertToAndFrom(1990, 1990L);
        }

        [Test]
        public void IntToAndFromWrappedLong()
        {
            CheckConvertToAndFrom(1990, (object)1990L);
        }

        [Test]
        public void TimeSpanToAndFromString()
        {
            var timespan = new TimeSpan(4, 5, 30, 134);
            CheckConvertToAndFrom(timespan, timespan.ToString());
        }

        [Test]
        public void DateTimeToAndFromString()
        {
            var formats = new[] { string.Empty, "en-US", "de-DE", "fr-FR" }
                .Select(CultureInfo.GetCultureInfo);
            foreach (var format in formats)
            {
                foreach (DateTimeKind kind in Enum.GetValues(typeof(DateTimeKind)))
                {
                    var date = new DateTime(1978, 10, 30, 21, 15, 30, kind);
                    CheckConvertToAndFrom(date, date.ToString(format), format);
                }
            }
        }

        [Test]
        public void DateTimeToAndFromLong()
        {
            var dateTime = new DateTime(1978, 10, 30, 21, 15, 30, DateTimeKind.Local);
            CheckConvertToAndFrom(dateTime, dateTime.ToBinary());

            dateTime = new DateTime(1978, 10, 30, 21, 15, 30, DateTimeKind.Utc);
            CheckConvertToAndFrom(dateTime, dateTime.ToBinary());
        }

        [Test]
        public void GuidToAndFromByteArray()
        {
            var guid = new Guid("21EC2020-3AEA-1069-A2DD-08002B30309D");
            CheckConvertToAndFrom(guid, guid.ToByteArray());
        }

        [Test]
        public void GuidToAndFromString()
        {
            var guid = new Guid("21EC2020-3AEA-1069-A2DD-08002B30309D");
            CheckConvertToAndFrom(guid, guid.ToString());
        }

        [Test]
        public void EnumToAndFromUnderlyingType()
        {
            CheckConvertToAndFrom(TargetType.TestEnum.Three, 3);
        }

        [Test]
        public void EnumToAndFromNotUnderlyingType()
        {
            CheckConvertToAndFrom(TargetType.TestEnum.Three, (long)3);
        }

        [Test]
        public void EnumToAndFromString()
        {
            CheckConvertToAndFrom(TargetType.TestEnum.Three, "Three");
        }

        private static void CheckConvertToAndFrom<T1, T2>(T1 value1, T2 value2, IFormatProvider format = null)
        {
            ConvertAssert.IsConvertedTo(value1, value2, format);
            ConvertAssert.IsConvertedTo(value2, value1, format);
        }
    }
}