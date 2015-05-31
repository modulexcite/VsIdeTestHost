// <copyright file="TestAdapterFactory.cs" company="Oleg Sych">
//  Copyright © 2015 Oleg Sych
// </copyright>

using Microsoft.VisualStudio.TestTools.TestAdapter;

namespace VsIdeTestHost.Runner
{
    /// <summary>
    /// Represents an object responsible for creating <see cref="ITestAdapter"/> instances.
    /// </summary>
    public abstract class TestAdapterFactory
    {
        /// <summary>
        /// Creates an <see cref="ITestAdapter"/> instance with the given type name.
        /// </summary>
        public abstract ITestAdapter CreateTestAdapter(string typeName);
    }
}
