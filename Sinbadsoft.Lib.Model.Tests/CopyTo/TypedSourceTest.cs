// <copyright file="CopyToWithTypedSourceTest.cs" company="Sinbadsoft">
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
// <date>2012/01/22</date>

using System;

using NUnit.Framework;

using Sinbadsoft.Lib.Model.CopyTo;

namespace Sinbadsoft.Lib.Model.Tests.CopyTo
{
    public class CopyToWithTypedSourceTest
    {
        [Test]
        public void NamedTypeStringToGuid()
        {
            Assert.AreEqual(
                new Guid("21EC2020-3AEA-1069-A2DD-08002B30309D"),
                new Source { GuidPty = "21EC2020-3AEA-1069-A2DD-08002B30309D" }.CopyTo<TargetType>().GuidPty);
        }

        [Test]
        public void StringToGuid()
        {
            Assert.AreEqual(
                new Guid("21EC2020-3AEA-1069-A2DD-08002B30309D"),
                new { GuidPty = "21EC2020-3AEA-1069-A2DD-08002B30309D" }.CopyTo<TargetType>().GuidPty);
        }

        [Test]
        public void ByteToGuid()
        {
            Assert.AreEqual(
                new Guid("21EC2020-3AEA-1069-A2DD-08002B30309D"),
                new { GuidPty = new Guid("21EC2020-3AEA-1069-A2DD-08002B30309D").ToByteArray() }.CopyTo<TargetType>().GuidPty);
        }

        [Test]
        public void LongToLong()
        {
            Assert.AreEqual(
                5L,
                new { LongPty = 5L }.CopyTo<TargetType>().LongPty);
        }

        [Test]
        public void ObjectToObject()
        {
            var value = new object();
            Assert.AreEqual(
                value,
                new { ObjPty = value }.CopyTo<TargetType>().ObjPty);
        }

        [Test]
        public void IntToEnum()
        {
            Assert.AreEqual(
                TargetType.TestEnum.Two,
                new { EnumPty = 2 }.CopyTo<TargetType>().EnumPty);
        }

        [Test]
        public void EnumToInt()
        {
            Assert.AreEqual(
                3,
                new { IntPty = TargetType.TestEnum.Three }.CopyTo<TargetType>().IntPty);
        }

        private class Source
        {
            public string GuidPty { get; set; }
        }
    }
}
