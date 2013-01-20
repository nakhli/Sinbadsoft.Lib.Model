// <copyright file="DataRecordSourceTest.cs" company="Sinbadsoft">
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
using System.Collections.Generic;
using System.Globalization;

using NUnit.Framework;

using Sinbadsoft.Lib.Model.CopyTo;

namespace Sinbadsoft.Lib.Model.Tests.CopyTo
{
    using System.Data;

    public class DataRecordSourceTest
    {
        [Test]
        public void DataRecordMockSource()
        {
            var source = new DataRecordMock();

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
        
        internal class DataRecordMock : IDataRecord
        {
            private readonly SortedList<string, object> list = new SortedList<string, object>();

            public int FieldCount
            {
                get { return this.list.Count; }
            }

            object IDataRecord.this[int i]
            {
                get { throw new NotImplementedException(); }
            }

            public object this[string name]
            {
                get { throw new NotImplementedException(); }
                set { this.list[name] = value; }
            }

            public string GetName(int i)
            {
                return this.list.Keys[i];
            }

            public string GetDataTypeName(int i)
            {
                throw new NotImplementedException();
            }

            public Type GetFieldType(int i)
            {
                throw new NotImplementedException();
            }

            public object GetValue(int i)
            {
                return this.list.Values[i];
            }

            public int GetValues(object[] values)
            {
                throw new NotImplementedException();
            }

            public int GetOrdinal(string name)
            {
                throw new NotImplementedException();
            }

            public bool GetBoolean(int i)
            {
                throw new NotImplementedException();
            }

            public byte GetByte(int i)
            {
                throw new NotImplementedException();
            }

            public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
            {
                throw new NotImplementedException();
            }

            public char GetChar(int i)
            {
                throw new NotImplementedException();
            }

            public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
            {
                throw new NotImplementedException();
            }

            public Guid GetGuid(int i)
            {
                throw new NotImplementedException();
            }

            public short GetInt16(int i)
            {
                throw new NotImplementedException();
            }

            public int GetInt32(int i)
            {
                throw new NotImplementedException();
            }

            public long GetInt64(int i)
            {
                throw new NotImplementedException();
            }

            public float GetFloat(int i)
            {
                throw new NotImplementedException();
            }

            public double GetDouble(int i)
            {
                throw new NotImplementedException();
            }

            public string GetString(int i)
            {
                throw new NotImplementedException();
            }

            public decimal GetDecimal(int i)
            {
                throw new NotImplementedException();
            }

            public DateTime GetDateTime(int i)
            {
                throw new NotImplementedException();
            }

            public IDataReader GetData(int i)
            {
                throw new NotImplementedException();
            }

            public bool IsDBNull(int i)
            {
                throw new NotImplementedException();
            }
        }
    }
}