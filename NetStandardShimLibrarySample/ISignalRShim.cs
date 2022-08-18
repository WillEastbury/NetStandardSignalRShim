using System;
using System.Threading.Tasks;

namespace NetStandardSignalRShim
{
    public interface ISignalRShim
    {
        
        void BindIncomingListener<T>(string HubFunctionName, Action<T> ListenerDelegateCallBack);
        Task StartConnectionAsync(string endpoint);
        Task SendMessageAsync(string HubMethod, string message);
    }
}