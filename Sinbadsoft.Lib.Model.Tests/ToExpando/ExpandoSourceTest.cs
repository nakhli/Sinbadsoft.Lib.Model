// <copyright file="ExpandoSourceTest.cs" company="Sinbadsoft">
// Copyright (c) Chaker Nakhli 2012-2013
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
using System.Dynamic;

using NUnit.Framework;

using Sinbadsoft.Lib.Model.ToExpando;

namespace Sinbadsoft.Lib.Model.Tests.ToExpando
{
    [TestFixture]
    public class ExpandoSourceTest
    {
        [Test]
        public void PropertySetPreservedAndSourceNotModified()
        {
            const string ValueFoo = "hello";
            var valueBar = new DateTime(2013, 12, 1, 23, 0, 0);
            var valueBaz = new object();
            var valueFuu = new TargetType();

            dynamic source = new ExpandoObject();
            source.Foo = ValueFoo;
            source.Bar = valueBar;
            source.Baz = valueBaz;
            source.Fuu = valueFuu;
            
            dynamic result = ((object)source).ToExpando();
            
            Assert.AreNotSame(source, result);
            
            Assert.AreEqual(source.Foo, result.Foo);
            Assert.AreEqual(source.Bar, result.Bar);
            Assert.AreEqual(source.Baz, result.Baz);
            Assert.AreEqual(source.Fuu, result.Fuu);

            Assert.AreEqual(ValueFoo, source.Foo);
            Assert.AreEqual(valueBar, source.Bar);
            Assert.AreEqual(valueBaz, source.Baz);
            Assert.AreEqual(valueFuu, source.Fuu);
        }

        [Test]
        public void DbNullTest()
        {
            dynamic result = new { Foo = DBNull.Value }.ToExpando();
            Assert.AreEqual(null, result.Foo);
        }
    }
}