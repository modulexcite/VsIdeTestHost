// <copyright file="TestRunner.cs" company="Oleg Sych">
//  Copyright © 2015 Oleg Sych
// </copyright>

using System;
using System.Collections.Concurrent;
using Microsoft.VisualStudio.TestTools.Common;
using Microsoft.VisualStudio.TestTools.Execution;
using Microsoft.VisualStudio.TestTools.TestAdapter;

namespace VsIdeTestHost.Runner
{
    /// <summary>
    /// Created by the hosting process receive .NET remoting calls from the test controller and 
    /// run tests using locally installed test adapters.
    /// </summary>
    public class TestRunner : MarshalByRefObject, ITestAdapter
    {
        private readonly TestAdapterFactory factory;
        private readonly ConcurrentDictionary<string, ITestAdapter> adapters;
        private IRunContext runContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestRunner"/> class.
        /// </summary>
        public TestRunner(TestAdapterFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            this.factory = factory;
            this.adapters = new ConcurrentDictionary<string, ITestAdapter>();
        }

        /// <summary>
        /// Aborts test run of the previously instantiated test adapters.
        /// </summary>
        public void AbortTestRun()
        {
            this.ForEachAdapter(adapter => adapter.AbortTestRun());
        }

        /// <summary>
        /// Cleans up and releases the previously instantiated test adapters.
        /// </summary>
        public void Cleanup()
        {
            this.ForEachAdapter(adapter => adapter.Cleanup());
            this.adapters.Clear();
        }

        /// <summary>
        /// Stores the given <see cref="IRunContext"/> to for future initialization of test adapters running tests.
        /// </summary>
        public void Initialize(IRunContext runContext)
        {
            this.runContext = runContext;
        }

        /// <summary>
        /// Pauses test run of the previously instantiated test adapters.
        /// </summary>
        public void PauseTestRun()
        {
            this.ForEachAdapter(adapter => adapter.PauseTestRun());
        }

        /// <summary>
        /// Notifies the previously instantiated test adapters about test run finishing.
        /// </summary>
        public void PreTestRunFinished(IRunContext runContext)
        {
            this.ForEachAdapter(adapter => adapter.PreTestRunFinished(runContext));
        }

        /// <summary>
        /// Passes a <paramref name="message"/> to the previously instantiated test adapters.
        /// </summary>
        public void ReceiveMessage(object message)
        {
            this.ForEachAdapter(adapter => adapter.ReceiveMessage(message));
        }

        /// <summary>
        /// Resumes test run of the previously instantiated test adapters.
        /// </summary>
        public void ResumeTestRun()
        {
            this.ForEachAdapter(adapter => adapter.ResumeTestRun());
        }

        /// <summary>
        /// Runs a test using the <see cref="ITestAdapter"/> of type specified in the <see cref="ITestElement"/>.
        /// </summary>
        public void Run(ITestElement testElement, ITestContext testContext)
        {
            ITestAdapter adapter = this.adapters.GetOrAdd(testElement.Adapter, this.CreateTestAdapter);
            adapter.Run(testElement, testContext);
        }

        /// <summary>
        /// Stops test run of the previously instantiated test adapters.
        /// </summary>
        public void StopTestRun()
        {
            this.ForEachAdapter(adapter => adapter.StopTestRun());
        }

        private ITestAdapter CreateTestAdapter(string typeName)
        {
            ITestAdapter adapter = this.factory.CreateTestAdapter(typeName);
            adapter.Initialize(this.runContext);
            return adapter;
        }

        private void ForEachAdapter(Action<ITestAdapter> action)
        {
            foreach (ITestAdapter adapter in this.adapters.Values)
            {
                action(adapter);
            }
        }
    }
}
