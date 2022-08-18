# Azure SignalR / Unity (dotnetstandard 2.1) Sample
------

## A worked sample of using Azure SignalR service in serverless mode from Unity 2021.3.7f1 and later with NetStandard 2.1 and dependency management

### /NetStandardShimLibrarySample/ folder contains 5 projects
- NetStandardShimLibrarySample.csproj - contains the Net Standard Shim Library Sample (a simple wrapper around the Azure SignalR libraries).
- UnityScripts - a sample of how to consume the base library and use it in Unity in c# scripts *once you have dropped the dependencies and NetStandardShimLibrarySample.dll into your assets folder, of course.*
- FunctionSignalR - a sample of how to respond to messages server side and do serverless endpoint negotiation.
- TestConsoleApp - a simple test shim to run the library in the console and check it works in .net 6 as well as in Unity.
- FindDeploymentDependenciesSampleScript - a script (downloaddeps.ps1) that finds the Azure SignalR dependencies in the Unity project and copies them to a local folder to avoid the use of nuget directly in unity (this script is based on this netstandard 2.0 script https://github.com/evanlindsey/Unity-WebGL-SignalR/blob/master/Unity/Assets/Plugins/SignalR/lib/signalr.ps1)

The shim layer is used to allow the Unity project to use the Azure SignalR libraries and consists of only three methods.

Note that there are other tools out there that could help you, but as far as I know right now such as the one referenced above, but they do not allow you to use the latest versions of the SignalR libraries due to internal version conflicts, and the latest binaries are needed to be able to use the Azure SignalR service. 

These tools will allow you to use the latest versions of the libraries, but you will need to use the at least version 2021.3.7f1 of Unity, which supports netstandard2.1.

This compiles and runs on Windows to check the dependency chain and shim, but has not been compatibility tested on all possible unity devices - if it doesn't run on iOS or Android, then there is likely a problem with Unity's netstandard2.1 / mono support. 

``` csharp 
    // Connects and negotiates the connection with SignalR service.
    Task StartConnectionAsync(string endpoint); 

    // Call StartConnectionAsync as follows 
    using NetStandardSignalRShim;
    SignalRShim srsh = new SignalRShim();
    await srsh.StartConnectionAsync("http://localhost:7071/api");

    // Binds a delegate (An Action<T>) / callback to take action when a message is received by the SignalR listener
    void BindIncomingListener<T>(string HubFunctionName, Action<T> ListenerDelegateCallBack);

    // Call BindIncomingListener as follows
    srsh.BindIncomingListener<string>("ReceiveMessageString",
        (message) =>
        {
            Console.WriteLine(message);
        }
    );

    // Sends a message to the SignalR service.
    Task SendMessageAsync(string HubMethod, string message);

    // You can send a message as follows
    await srsh.SendMessageAsync("SendMessageFromClient", "Bish");
    

    // And there you have pretty much everything you need to use the Azure SignalR service in Unity.
    // You also have the entire contents of the sample console app too :) 

``` 

This is provided as-is as POC standard sample code, with no warranty implied or otherwise and is NOT intended to be used in production.
