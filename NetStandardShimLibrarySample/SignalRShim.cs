using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using System;
namespace NetStandardSignalRShim
{
    public class SignalRShim : ISignalRShim
    {
        public HubConnection connection { get; private set; }

        public async Task StartConnectionAsync(string endpoint)
        {
            connection = new HubConnectionBuilder()
                .WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.Zero, TimeSpan.FromSeconds(10) })
                .WithUrl(endpoint)
                .Build();
            await connection.StartAsync();
        }

        public void BindIncomingListener<T>(string HubFunctionName, Action<T> ListenerDelegateCallBack)
        {

            connection.On<T>(HubFunctionName, ListenerDelegateCallBack);

        }
        public async Task SendMessageAsync(string HubMethod, string message)
        {

            await connection.InvokeAsync(HubMethod, message);

        }
    }
}
