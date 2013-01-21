// <copyright file="DocumentationExamplesTest.cs" company="Sinbadsoft">
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
// <date>2013/01/20</date>
using System;
using System.Collections.Generic;

using NUnit.Framework;

using Sinbadsoft.Lib.Model.CopyTo;
using Sinbadsoft.Lib.Model.ToExpando;

namespace Sinbadsoft.Lib.Model.Tests
{
    public class DocumentationExamplesTest
    {
        [Test]
        public void CopyToTest()
        {
            var uniqueId = new Guid("21EC2020-3AEA-1069-A2DD-08002B30309D");
            
            UserDto userdto = /* repository.LoadUserFromDb(); */ new UserDto()
                {
                    Name = "Robert", 
                    Role = 2,
                    UniqueId = uniqueId.ToByteArray()
                };

            // create and copy to new instance
            User user = userdto.CopyTo<User>();
            
            Assert.AreEqual(user.Name, "Robert");
            Assert.AreEqual(user.Role, User.UserRole.Admin);
            Assert.AreEqual(user.UniqueId, uniqueId);

            // Copy to existing instance
            user = new User();
            userdto.CopyTo(user);

            Assert.AreEqual(user.Name, "Robert");
            Assert.AreEqual(user.Role, User.UserRole.Admin);
            Assert.AreEqual(user.UniqueId, uniqueId);

            user = new Dictionary<string, object>
                {
                    { "UniqueId", "21EC2020-3AEA-1069-A2DD-08002B30309D" },
                    { "Name", "Fred" },
                    { "Role", 1 }
                }.CopyTo<User>();

            Assert.AreEqual(user.Name, "Fred");
            Assert.AreEqual(user.Role, User.UserRole.Editor);
            Assert.AreEqual(user.UniqueId, uniqueId);
        }

        [Test]
        public void ToExpandoTest()
        {
            User user = new User { Name = "Fred", Role = User.UserRole.Admin, UniqueId = Guid.NewGuid() };
            dynamic userExpando = user.ToExpando();

            Assert.AreEqual(user.Name, userExpando.Name);
            Assert.AreEqual(user.Role, userExpando.Role);
            Assert.AreEqual(user.UniqueId, userExpando.UniqueId);

            // change types
            userExpando = user.ToExpando(new { Role = typeof(string), UniqueId = typeof(byte[]) });
            Assert.AreEqual(user.Role.ToString(), userExpando.Role);
            Assert.AreEqual(user.UniqueId.ToByteArray(), userExpando.UniqueId);


            // slice
            // userExpando dosen't contain property Role
            userExpando = user.ToExpando(blacklist: new[] { "Role" });
            Assert.That(((IDictionary<string, object>)userExpando).Keys, Is.EquivalentTo(new[] { "Name", "UniqueId" }));

            // userExpando contains only Name and Role properties, nothing else
            userExpando = user.ToExpando(whitelist: new[] { "Name", "Role" });
            Assert.That(((IDictionary<string, object>)userExpando).Keys, Is.EquivalentTo(new[] { "Name", "Role" }));
        }

        public class User
        {
            public Guid UniqueId { get; set; }

            public string Name { get; set; }

            public UserRole Role { get; set; }

            public enum UserRole { User, Editor, Admin }
        }

        public class UserDto
        {
            public byte[] UniqueId { get; set; }

            public string Name { get; set; }

            public int Role { get; set; }
        }
    }
}