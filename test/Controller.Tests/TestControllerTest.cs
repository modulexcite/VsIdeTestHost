// <copyright file="TestControllerTest.cs" company="Oleg Sych">
//  Copyright © 2015 Oleg Sych
// </copyright>

using Microsoft.VisualStudio.TestTools.TestAdapter;
using Xunit;

namespace VsIdeTestHost.Controller.Tests
{
    public class TestControllerTest
    {
        [Fact]
        public void ClassImplementsITestAdapterToIntegrateWithMSTest()
        {
            var controller = new TestController();
            Assert.IsAssignableFrom<ITestAdapter>(controller);
        }
    }
}
