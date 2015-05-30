// <copyright file="TestRunnerTest.cs" company="Oleg Sych">
//  Copyright © 2015 Oleg Sych
// </copyright>

using Microsoft.VisualStudio.TestTools.TestAdapter;
using Xunit;

namespace VsIdeTestHost.Runner.Tests
{
    public class TestRunnerTest
    {
        [Fact]
        public void ClassImplementsITestAdapterToSimplifyImplementationOfController()
        {
            var runner = new TestRunner();
            Assert.IsAssignableFrom<ITestAdapter>(runner);
        }
    }
}
