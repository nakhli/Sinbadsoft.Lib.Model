# Installation
A [nuget](http://nuget.org/packages/Sinbadsoft.Lib.Model) is available, just run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console):
```powershell
Install-Package Sinbadsoft.Lib.Model
```

#Simple zero-config convention-based object to object mapper.

This library uses reflection to copy prperties of object of type `A` to object of type `B` based on property name.
Example:
```
UserDto userDto = /* Load from db or any other source ... */ repo.LoadUser();
User user = userDto.CopyTo<User>()
```

In this example, a new instance of `User` is created. A copy to an existing instance is also possible:
```
userDto.CopyTo(user);
```

Here is an example `User` and `UserDto` definitions:
```
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
```

You can notice that the mapper has automatically converted `byte[]` to `Guid` and `int` to `UserRole`. When the target property type is not assignable from the source target property, the mapper attempts to perform the following conversions:
* `string` and `byte[]` to/from `Guid`
* `string` to/from enum
* integral types (SByte, Int16, Int32, Int64, Byte, UInt16, UInt32, or UInt64) to/from enum; even if the integral type is different from the underlying type of the enum.
* `string` to/from `TimeSpan`
* `string` to/from `DateTime`
* struct T to/from `Nullable<T>`
* If none of the above rule is applicable, and target type is `string`, [<code>Convert.ToString</code>](http://msdn.microsoft.com/en-us/library/ms131014.aspx) is used.
* If none of the above rules is applicable, [<code>Convert.ChangeType</code>](http://msdn.microsoft.com/en-us/library/dtb69x08.aspx) is used.


The types [<code>IDictionary<></code>](http://msdn.microsoft.com/en-us/library/s4ys34ea), [<code>IDictionary</code>](http://msdn.microsoft.com/en-us/library/system.collections.idictionary), [<code>ExpandoObject</code>](http://msdn.microsoft.com/en-us/library/System.Dynamic.ExpandoObject.aspx) and [<code>NameValueCollection</code>](http://msdn.microsoft.com/en-us/library/System.Collections.Specialized.NameValueCollection.aspx) are treated in a special way when used as source type. Instead of copying the properties of these hashes, their keys and values are used instead.
Example:
```
User user = new Dictionary<string, object>
    {
        { "UniqueId", "21EC2020-3AEA-1069-A2DD-08002B30309D" },
        { "Name", "Fred" },
        { "Role", 1 }
    }.CopyTo<User>();
```
## ToExpando
The library provides a utility to copy any object to an [<code>ExpandoObject</code>](http://msdn.microsoft.com/en-us/library/System.Dynamic.ExpandoObject.aspx). In the example below, `userExpando` object have exactly the same properties of the soruce `user` object.
```
dynamic userExpando = user.ToExpando();
```

The resulting expando object can be sliced using a black list:
```
// userExpando dosen't contain property Role
userExpando = user.ToExpando(blacklist: new[] { "Role" });
```
or a white list:
```
// userExpando contains only Name and Role properties, nothing else
userExpando = user.ToExpando(whitelist: new[] { "Name", "Role" });
```

A different target type can be specified for a given source property. In the following example, the created expando has `UniqueId` property of type `byte[]`, converted form the original `UniqueId` property of type `Guid`.
```
userExpando = user.ToExpando(new { UniqueId = typeof(byte[]) });
```
# Related projects
* [automapper](https://github.com/AutoMapper/AutoMapper)

# License
Copyright 2012-2013 [Sinbadsoft](http://www.sinbadsoft.com).

Licensed under the Apache License, [Version 2.0](http://www.apache.org/licenses/LICENSE-2.0).
