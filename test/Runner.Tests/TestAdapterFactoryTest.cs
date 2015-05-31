// <copyright file="TestAdapterFactoryTest.cs" company="Oleg Sych">
//  Copyright © 2015 Oleg Sych
// </copyright>

using Microsoft.VisualStudio.TestTools.Common;
using Microsoft.VisualStudio.TestTools.Execution;
using Microsoft.VisualStudio.TestTools.TestAdapter;
using Xunit;

namespace VsIdeTestHost.Runner.Tests
{
    public class TestAdapterFactoryTest
    {
        [Fact]
        public void CreateTestAdapterReturnsInstanceOfTypeWithGivenName()
        {
            var factory = new TestAdapterFactory();
            string stubAdapterTypeName = typeof(StubTestAdapter).AssemblyQualifiedName;
            ITestAdapter adapter = factory.CreateTestAdapter(stubAdapterTypeName);
            Assert.IsType<StubTestAdapter>(adapter);
        }

        public sealed class StubTestAdapter : ITestAdapter
        {
            void IBaseAdapter.AbortTestRun()
            {
            }

            void IBaseAdapter.Cleanup()
            {
            }

            void ITestAdapter.Initialize(IRunContext runContext)
            {
            }

            void IBaseAdapter.PauseTestRun()
            {
            }

            void ITestAdapter.PreTestRunFinished(IRunContext runContext)
            {
            }

            void ITestAdapter.ReceiveMessage(object message)
            {
            }

            void IBaseAdapter.ResumeTestRun()
            {
            }

            void IBaseAdapter.Run(ITestElement testElement, ITestContext testContext)
            {
            }

            void IBaseAdapter.StopTestRun()
            {
            }
        }
    }
}
