using NetStandardSignalRShim;

SignalRShim srsh = new SignalRShim();
await srsh.StartConnectionAsync("http://localhost:7071/api");
srsh.BindIncomingListener<string>("ReceiveMessageString",
    (message) =>
    {
        Console.WriteLine(message);
    }
);
await srsh.SendMessageAsync("SendMessageFromClient", "Bish");
Console.ReadLine();
