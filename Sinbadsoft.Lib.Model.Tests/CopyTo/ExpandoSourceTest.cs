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
// <date>2012/01/22</date>

using System;
using System.Dynamic;

using NUnit.Framework;

using Sinbadsoft.Lib.Model.CopyTo;

namespace Sinbadsoft.Lib.Model.Tests.CopyTo
{
    using System.Globalization;

    public class ExpandoSourceTest
    {
        [Test]
        public void StringToGuid()
        {
            dynamic source = new ExpandoObject();
            source.GuidPty = "21EC2020-3AEA-1069-A2DD-08002B30309D";
            Assert.AreEqual(
                new Guid("21EC2020-3AEA-1069-A2DD-08002B30309D"),
                ((object)source).CopyTo<TargetType>().GuidPty);
        }

        [Test]
        public void ByteToGuid()
        {
            dynamic source = new ExpandoObject();
            source.GuidPty = new Guid("21EC2020-3AEA-1069-A2DD-08002B30309D").ToByteArray();
            Assert.AreEqual(
                new Guid("21EC2020-3AEA-1069-A2DD-08002B30309D"),
                ((object)source).CopyTo<TargetType>().GuidPty);
        }

        [Test]
        public void LongToLong()
        {
            dynamic source = new ExpandoObject();
            source.LongPty = 5L;
            Assert.AreEqual(
                5L,
                ((object)source).CopyTo<TargetType>().LongPty);
        }

        [Test]
        public void ObjectToObject()
        {
            var value = new object();
            dynamic source = new ExpandoObject();
            source.ObjPty = value;
            Assert.AreEqual(
                value,
                ((object)source).CopyTo<TargetType>().ObjPty);
        }

        [Test]
        public void ExpandoObjectSource()
        {
            dynamic source = new ExpandoObject();
            object sourceAsObject = source;

            // guid
            var guidValue = new Guid("21EC2020-3AEA-1069-A2DD-08002B30309D");
            source.GuidPty = guidValue;
            Assert.AreEqual(guidValue, sourceAsObject.CopyTo<TargetType>().GuidPty);

            source.GuidPty = guidValue.ToString();
            Assert.AreEqual(guidValue, sourceAsObject.CopyTo<TargetType>().GuidPty);

            source.GuidPty = guidValue.ToByteArray();
            Assert.AreEqual(guidValue, sourceAsObject.CopyTo<TargetType>().GuidPty);

            // int
            source.IntPty = 345;
            Assert.AreEqual(345, sourceAsObject.CopyTo<TargetType>().IntPty);

            source.IntPty = 345.ToString(CultureInfo.InvariantCulture);
            Assert.AreEqual(345, sourceAsObject.CopyTo<TargetType>().IntPty);

            source.IntPty = (long)345;
            Assert.AreEqual(345, sourceAsObject.CopyTo<TargetType>().IntPty);

            source.IntPty = int.MaxValue;
            Assert.AreEqual(int.MaxValue, sourceAsObject.CopyTo<TargetType>().IntPty);

            // enum pty
            source.EnumPty = TargetType.TestEnum.Three;
            Assert.AreEqual(TargetType.TestEnum.Three, sourceAsObject.CopyTo<TargetType>().EnumPty);

            source.EnumPty = TargetType.TestEnum.Three.ToString();
            Assert.AreEqual(TargetType.TestEnum.Three, sourceAsObject.CopyTo<TargetType>().EnumPty);

            source.EnumPty = (int)TargetType.TestEnum.Three;
            Assert.AreEqual(TargetType.TestEnum.Three, sourceAsObject.CopyTo<TargetType>().EnumPty);

            source.EnumPty = (byte)TargetType.TestEnum.Three;
            Assert.AreEqual(TargetType.TestEnum.Three, sourceAsObject.CopyTo<TargetType>().EnumPty);

            source.EnumPty = (long)TargetType.TestEnum.Three;
            Assert.AreEqual(TargetType.TestEnum.Three, sourceAsObject.CopyTo<TargetType>().EnumPty);

            // obj pty
            var obj = new object();
            source.ObjPty = obj;
            Assert.AreEqual(obj, sourceAsObject.CopyTo<TargetType>().ObjPty);
        }
    }
}