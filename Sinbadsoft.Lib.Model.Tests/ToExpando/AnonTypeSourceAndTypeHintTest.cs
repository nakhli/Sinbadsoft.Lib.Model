// <copyright file="AnonTypeSourceAndTypeHintTest.cs" company="Sinbadsoft">
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
// <date>2012/02/21</date>

using System;

using NUnit.Framework;

using Sinbadsoft.Lib.Model.ToExpando;

namespace Sinbadsoft.Lib.Model.Tests.ToExpando
{
    [TestFixture]
    public class AnonTypeSourceAndTypeHintTest
    {
        [Test]
        public void IntToEnum()
        {
            dynamic result = new { Foo = 2 }.ToExpando(new { Foo = typeof(TargetType.TestEnum) });
            Assert.AreEqual(TargetType.TestEnum.Two, result.Foo);
        }

        [Test]
        public void DbNullToInt()
        {
            dynamic result = new { Foo = DBNull.Value }.ToExpando(new { Foo = typeof(int) });
            Assert.AreEqual(0, result.Foo);
        }

        [Test]
        public void DbNullToObject()
        {
            dynamic result = new { Foo = DBNull.Value }.ToExpando(new { Foo = typeof(object) });
            Assert.AreEqual(null, result.Foo);
        }

        [Test]
        public void BytesToGuid()
        {
            var guid = new Guid("21EC2020-3AEA-1069-A2DD-08002B30309D");
            var bytes = guid.ToByteArray();
            dynamic result = new { Bar = bytes }.ToExpando(new { Bar = typeof(Guid) });
            Assert.AreEqual(guid, result.Bar);
        }

        [Test]
        public void StringToLong()
        {
            dynamic result = new { Bar = "5000000000" }.ToExpando(new { Bar = typeof(long) });
            Assert.AreEqual(5000000000L, result.Bar);
        }

        [Test]
        public void StringToGuid()
        {
            var guid = new Guid("21EC2020-3AEA-1069-A2DD-08002B30309D");
            dynamic result = new { Bar = guid.ToString() }.ToExpando(new { Bar = typeof(Guid) });
            Assert.AreEqual(guid, result.Bar);
        }

        [Test]
        public void DateTimeToNullableDateTime()
        {
            var value = new DateTime(1078, 10, 30, 21, 0, 0);
            dynamic result = new { Bar = value }.ToExpando(new { Bar = typeof(DateTime?) });
            Assert.AreEqual(value, result.Bar);
        }

        [Test]
        public void NullableDateTimeToDateTime()
        {
            DateTime? value = new DateTime(1078, 10, 30, 21, 0, 0);
            dynamic result = new { Bar = value }.ToExpando(new { Bar = typeof(DateTime) });
            Assert.AreEqual(value, result.Bar);
        }
    }
}