# eShopOnWeb in VB.NET (.NET Core 3.1)
By Mohammad Hamdy Ghanem
Based on Microsoft Full web sample [eShopOnWeb](https://github.com/dotnet-architecture/eShopOnWeb)

[eShopOnWeb](https://github.com/dotnet-architecture/eShopOnWeb) is a sample ASP.NET Core reference application, powered by Microsoft, demonstrating a single-process (monolithic) application architecture.
I created a VB.NET version of this app, using [Vazor](https://github.com/VBAndCs/Vazor) and [ZML](https://github.com/VBAndCs/ZML) to design MVC views and Razor Pages, because ASP.NET Core Doesn't provide a razor syntax that supports VB.NET.
I hope VB.NEt developers found it useful, and start creating their web applications targeting ASP.NET Core 3.1 using VB.NET and Vazor.

The solution contains 3 projects:
1. ApplicationCore.vb: 
a VB.NET .NET standard library project that contains database entities and expressions needed to configure EF Core.
2. Infrastructure.vb: 
a VB.NET .NET standard library project that defins the data repository, database context, data megrations, and other services.
3. Web.vb: 
a VB.NET ASP.NET Core 3.1 project, that contains MVC views and Razor Pages that designs the website.

# Behind the scenes:
Read about [Vazor history](https://github.com/VBAndCs/Vazor/blob/master/A-Vazor-story.md): how the idea was born, and grown until eShopOnWeb.vb was possible.

Eng. Mohammed Hamdy Ghanem,
Egypt.
