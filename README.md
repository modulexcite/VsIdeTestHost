# MSTest.VSHost

An [ITestAdapter](https://msdn.microsoft.com/en-us/library/microsoft.visualstudio.testtools.testadapter.itestadapter.aspx) 
implementation for [hosting](https://msdn.microsoft.com/en-us/library/bb166558.aspx) MSTest runs in a Visual Studio 2015 
process. It will allow running integratin tests for Visual Studio extensions and is meant to replace the `VS IDE` host 
adapter which is no longer included in the Visual Studio SDK starting with version 2015 RC.
