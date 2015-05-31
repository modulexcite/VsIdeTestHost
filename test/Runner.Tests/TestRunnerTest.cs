// <copyright file="TestRunnerTest.cs" company="Oleg Sych">
//  Copyright © 2015 Oleg Sych
// </copyright>

using System;
using Microsoft.VisualStudio.TestTools.Common;
using Microsoft.VisualStudio.TestTools.Execution;
using Microsoft.VisualStudio.TestTools.TestAdapter;
using NSubstitute;
using Xunit;

namespace VsIdeTestHost.Runner.Tests
{
    public class TestRunnerTest
    {
        [Fact]
        public void ImplementsITestAdapterToSimplifyImplementationOfController()
        {
            Assert.True(typeof(ITestAdapter).IsAssignableFrom(typeof(TestRunner)));
        }

        [Fact]
        public void InheritsFromMarshalByRefObjectToAllowRemoteInvokationByController()
        {
            Assert.True(typeof(MarshalByRefObject).IsAssignableFrom(typeof(TestRunner)));
        }

        private static TestRunner CreateTestRunner(ITestAdapter adapter)
        {
            var factory = Substitute.For<TestAdapterFactory>();
            factory.CreateTestAdapter(Arg.Any<string>()).Returns(adapter);
            return new TestRunner(factory);
        }

        public class Constructor
        {
            [Fact]
            public void ThrowsArgumentNullExceptionWhenTestAdapterFactoryIsNullToFailFast()
            {
                var e = Assert.Throws<ArgumentNullException>(() => new TestRunner(null));
                Assert.EndsWith("factory", e.ParamName, StringComparison.OrdinalIgnoreCase);
            }
        }

        public class AbortTestRun
        {
            [Fact]
            public void AbortsTestRunOfInstantiatedAdapters()
            {
                var adapter = Substitute.For<ITestAdapter>();
                TestRunner runner = CreateTestRunner(adapter);

                runner.Run(Substitute.For<ITestElement>(), Substitute.For<ITestContext>());
                runner.AbortTestRun();

                adapter.Received().AbortTestRun();
            }
        }

        public class Cleanup
        {
            [Fact]
            public void CleansUpInstantiatedAdapters()
            {
                var adapter = Substitute.For<ITestAdapter>();
                TestRunner runner = CreateTestRunner(adapter);

                runner.Run(Substitute.For<ITestElement>(), Substitute.For<ITestContext>());
                runner.Cleanup();

                adapter.Received().Cleanup();
            }

            [Fact]
            public void ReleasesAdaptersForGarbageCollection()
            {
                var adapter = Substitute.For<ITestAdapter>();
                TestRunner runner = CreateTestRunner(adapter);

                runner.Run(Substitute.For<ITestElement>(), Substitute.For<ITestContext>());
                runner.Cleanup();
                runner.Cleanup();

                adapter.Received(1).Cleanup();
            }
        }

        public class PauseTestRun
        {
            [Fact]
            public void PausesTestRunOfInstantiatedAdapters()
            {
                var adapter = Substitute.For<ITestAdapter>();
                TestRunner runner = CreateTestRunner(adapter);

                runner.Run(Substitute.For<ITestElement>(), Substitute.For<ITestContext>());
                runner.PauseTestRun();

                adapter.Received().PauseTestRun();
            }
        }

        public class PreTestRunFinished
        {
            [Fact]
            public void NotifiesInstantiatedAdaptersAboutTestRunFinishing()
            {
                var adapter = Substitute.For<ITestAdapter>();
                TestRunner runner = CreateTestRunner(adapter);

                runner.Run(Substitute.For<ITestElement>(), Substitute.For<ITestContext>());
                var runContext = Substitute.For<IRunContext>();
                runner.PreTestRunFinished(runContext);

                adapter.Received().PreTestRunFinished(runContext);
            }
        }

        public class ReceiveMessage
        {
            [Fact]
            public void PassesMessageToInstantiatedAdapters()
            {
                var adapter = Substitute.For<ITestAdapter>();
                TestRunner runner = CreateTestRunner(adapter);

                runner.Run(Substitute.For<ITestElement>(), Substitute.For<ITestContext>());
                var message = new object();
                runner.ReceiveMessage(message);

                adapter.Received().ReceiveMessage(message);
            }
        }

        public class ResumeTestRun
        {
            [Fact]
            public void ResumesTestRunOfInstantiatedAdapters()
            {
                var adapter = Substitute.For<ITestAdapter>();
                TestRunner runner = CreateTestRunner(adapter);

                runner.Run(Substitute.For<ITestElement>(), Substitute.For<ITestContext>());
                runner.ResumeTestRun();

                adapter.Received().ResumeTestRun();
            }
        }

        public class Run
        {
            [Fact]
            public void InstantiatesAdapterOfTypeSpecifiedInTestElement()
            {
                var adapterFactory = Substitute.For<TestAdapterFactory>();
                var runner = new TestRunner(adapterFactory);

                var testElement = Substitute.For<ITestElement>();
                testElement.Adapter.Returns("TestAdapterType, TestAssembly");
                runner.Run(testElement, Substitute.For<ITestContext>());

                adapterFactory.Received().CreateTestAdapter(testElement.Adapter);
            }

            [Fact]
            public void InitializesAdapterWithRunContextObtainedDuringRunnerInitialize()
            {
                var adapter = Substitute.For<ITestAdapter>();
                TestRunner runner = CreateTestRunner(adapter);

                var runContext = Substitute.For<IRunContext>();
                runner.Initialize(runContext);
                runner.Run(Substitute.For<ITestElement>(), Substitute.For<ITestContext>());

                adapter.Received().Initialize(runContext);
            }

            [Fact]
            public void RunsInstantiatedAdapterWithGivenTestElementAndTestContext()
            {
                var adapter = Substitute.For<ITestAdapter>();
                TestRunner runner = CreateTestRunner(adapter);

                var testElement = Substitute.For<ITestElement>();
                var testContext = Substitute.For<ITestContext>();
                runner.Run(testElement, testContext);

                adapter.Received().Run(testElement, testContext);
            }

            [Fact]
            public void WhenCalledMultipleTimesCreatesSingleInstanceOfAdapterType()
            {
                var adapterFactory = Substitute.For<TestAdapterFactory>();
                var runner = new TestRunner(adapterFactory);

                var testElement = Substitute.For<ITestElement>();
                testElement.Adapter.Returns("TestAdapterType, TestAssembly");
                runner.Run(testElement, Substitute.For<ITestContext>());
                runner.Run(testElement, Substitute.For<ITestContext>());

                adapterFactory.Received(1).CreateTestAdapter(testElement.Adapter);
            }

            [Fact]
            public void WhenCalledMultipleTimesInitializesAdapterOnce()
            {
                var adapter = Substitute.For<ITestAdapter>();
                TestRunner runner = CreateTestRunner(adapter);
                runner.Initialize(Substitute.For<IRunContext>());

                var testElement = Substitute.For<ITestElement>();
                runner.Run(testElement, Substitute.For<ITestContext>());
                runner.Run(testElement, Substitute.For<ITestContext>());

                adapter.Received(1).Initialize(Arg.Any<IRunContext>());
            }
        }

        public class StopTestRun
        {
            [Fact]
            public void StopsTestRunOfInstantiatedAdapters()
            {
                var adapter = Substitute.For<ITestAdapter>();
                TestRunner runner = CreateTestRunner(adapter);

                runner.Run(Substitute.For<ITestElement>(), Substitute.For<ITestContext>());
                runner.StopTestRun();

                adapter.Received().StopTestRun();
            }
        }
    }
}
