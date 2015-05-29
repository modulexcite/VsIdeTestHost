// <copyright file="AssemblyInfo.cs" company="Oleg Sych">
//  Copyright © 2015 Oleg Sych
// </copyright>

using System.Reflection;
using System.Runtime.InteropServices;

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif 

[assembly: AssemblyProduct("Visual Studio host adapter for MSTest")]
[assembly: AssemblyCompany("Oleg Sych")]
[assembly: AssemblyCopyright("Copyright @ 2015 Oleg Sych")]
[assembly: ComVisible(false)]

// Version attributes are patched by build
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
