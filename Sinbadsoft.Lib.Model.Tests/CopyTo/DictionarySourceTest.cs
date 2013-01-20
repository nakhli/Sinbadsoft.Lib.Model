// <copyright file="DictionarySourceTest.cs" company="Sinbadsoft">
// Copyright (c) Chaker Nakhli 2013
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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;

using NUnit.Framework;

using Sinbadsoft.Lib.Model.CopyTo;

namespace Sinbadsoft.Lib.Model.Tests.CopyTo
{
    public class DictionarySourceTest
    {
        [Test]
        public void GenericDictionarySource()
        {
            var source = new Dictionary<string, object>();

            // guid
            var guidValue = new Guid("21EC2020-3AEA-1069-A2DD-08002B30309D");
            source["GuidPty"] = guidValue;
            Assert.AreEqual(guidValue, source.CopyTo<TargetType>().GuidPty);

            source["GuidPty"] = guidValue.ToString();
            Assert.AreEqual(guidValue, source.CopyTo<TargetType>().GuidPty);

            source["GuidPty"] = guidValue.ToByteArray();
            Assert.AreEqual(guidValue, source.CopyTo<TargetType>().GuidPty);

            // int
            source["IntPty"] = 345;
            Assert.AreEqual(345, source.CopyTo<TargetType>().IntPty);

            source["IntPty"] = 345.ToString(CultureInfo.InvariantCulture);
            Assert.AreEqual(345, source.CopyTo<TargetType>().IntPty);

            source["IntPty"] = (long)345;
            Assert.AreEqual(345, source.CopyTo<TargetType>().IntPty);

            source["IntPty"] = int.MaxValue;
            Assert.AreEqual(int.MaxValue, source.CopyTo<TargetType>().IntPty);

            // enum pty
            source["EnumPty"] = TargetType.TestEnum.Three;
            Assert.AreEqual(TargetType.TestEnum.Three, source.CopyTo<TargetType>().EnumPty);

            source["EnumPty"] = TargetType.TestEnum.Three.ToString();
            Assert.AreEqual(TargetType.TestEnum.Three, source.CopyTo<TargetType>().EnumPty);

            source["EnumPty"] = (int)TargetType.TestEnum.Three;
            Assert.AreEqual(TargetType.TestEnum.Three, source.CopyTo<TargetType>().EnumPty);

            source["EnumPty"] = (byte)TargetType.TestEnum.Three;
            Assert.AreEqual(TargetType.TestEnum.Three, source.CopyTo<TargetType>().EnumPty);

            source["EnumPty"] = (long)TargetType.TestEnum.Three;
            Assert.AreEqual(TargetType.TestEnum.Three, source.CopyTo<TargetType>().EnumPty);

            // obj pty
            var obj = new object();
            source["ObjPty"] = obj;
            Assert.AreEqual(obj, source.CopyTo<TargetType>().ObjPty);
        }

        [Test]
        public void NameValueCollectionSource()
        {
            var source = new NameValueCollection();

            // guid
            var guidValue = new Guid("21EC2020-3AEA-1069-A2DD-08002B30309D");

            source["GuidPty"] = guidValue.ToString();
            Assert.AreEqual(guidValue, source.CopyTo<TargetType>().GuidPty);

            // int
            source["IntPty"] = 345.ToString(CultureInfo.InvariantCulture);
            Assert.AreEqual(345, source.CopyTo<TargetType>().IntPty);

            // enum pty
            source["EnumPty"] = TargetType.TestEnum.Three.ToString();
            Assert.AreEqual(TargetType.TestEnum.Three, source.CopyTo<TargetType>().EnumPty);
        }

        [Test]
        public void NonGenericDictionarySource()
        {
            var source = new Hashtable();

            // guid
            var guidValue = new Guid("21EC2020-3AEA-1069-A2DD-08002B30309D");
            source["GuidPty"] = guidValue;
            Assert.AreEqual(guidValue, source.CopyTo<TargetType>().GuidPty);

            source["GuidPty"] = guidValue.ToString();
            Assert.AreEqual(guidValue, source.CopyTo<TargetType>().GuidPty);

            source["GuidPty"] = guidValue.ToByteArray();
            Assert.AreEqual(guidValue, source.CopyTo<TargetType>().GuidPty);

            // int
            source["IntPty"] = 345;
            Assert.AreEqual(345, source.CopyTo<TargetType>().IntPty);

            source["IntPty"] = 345.ToString(CultureInfo.InvariantCulture);
            Assert.AreEqual(345, source.CopyTo<TargetType>().IntPty);

            source["IntPty"] = (long)345;
            Assert.AreEqual(345, source.CopyTo<TargetType>().IntPty);

            source["IntPty"] = int.MaxValue;
            Assert.AreEqual(int.MaxValue, source.CopyTo<TargetType>().IntPty);

            // enum pty
            source["EnumPty"] = TargetType.TestEnum.Three;
            Assert.AreEqual(TargetType.TestEnum.Three, source.CopyTo<TargetType>().EnumPty);

            source["EnumPty"] = TargetType.TestEnum.Three.ToString();
            Assert.AreEqual(TargetType.TestEnum.Three, source.CopyTo<TargetType>().EnumPty);

            source["EnumPty"] = (int)TargetType.TestEnum.Three;
            Assert.AreEqual(TargetType.TestEnum.Three, source.CopyTo<TargetType>().EnumPty);

            source["EnumPty"] = (byte)TargetType.TestEnum.Three;
            Assert.AreEqual(TargetType.TestEnum.Three, source.CopyTo<TargetType>().EnumPty);

            source["EnumPty"] = (long)TargetType.TestEnum.Three;
            Assert.AreEqual(TargetType.TestEnum.Three, source.CopyTo<TargetType>().EnumPty);

            // obj pty
            var obj = new object();
            source["ObjPty"] = obj;
            Assert.AreEqual(obj, source.CopyTo<TargetType>().ObjPty);
        }
    }
}