# eShopOnWeb in VB.NET (.NET Core 3.1)
By Mohammad Hamdy Ghanem
Based on Microsoft Full web sample [eShopOnWeb](https://github.com/dotnet-architecture/eShopOnWeb)

eShopOnWeb is a sample ASP.NET Core reference application, powered by Microsoft, demonstrating a single-process (monolithic) application architecture.
I created a VB.NET version of this app, using [Vazor](https://github.com/VBAndCs/Vazor) and [ZML](https://github.com/VBAndCs/ZML) to design MVC views and Razor Pages, because ASP.NET Core Doesn't provide a razor syntax that supports VB.NET.
I hope VB.NEt developers found it useful, and start creating their wep applications targeting ASP.NET Core 3.1 using VB.NET and Vazor.

# Note:
The solution contains 3 projects:
1. ApplicationCore (C#)
the repo contains a VB.NET version named ApplicationCore.VB, but if you referenced it in the solution, it will causes a runtime exception. I will continue chasing this error until I fix it. I Hope you can help in this.
2. Infrastructure (VB.NET)
3. Web.vb (VB.NET)

Eng. Mohammed Hamdy Ghanem,
Egypt.
