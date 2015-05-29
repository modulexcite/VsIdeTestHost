// <copyright file="Package.cs" company="Oleg Sych">
//  Copyright © 2015 Oleg Sych
// </copyright>

using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TestTools.TestAdapter;

namespace TestAdapterHost
{
    /// <summary>
    /// Provides a service that allows obtaining an <see cref="ITestAdapter"/> and 
    /// running tests in the Visual Studio process.
    /// </summary>
    [Guid("cabe79df-1d59-43c7-9354-f17310f19c8a")]
    [PackageRegistration(UseManagedResourcesOnly = true)]
    public sealed class Package : Microsoft.VisualStudio.Shell.Package
    {
    }
}
