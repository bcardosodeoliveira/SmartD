using Smartd.Bibliotecas.Helps;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Smartd.Bibliotecas
{
    public class ConnectionCheck
    {
        public static async Task<bool> HasInternet(string host = "google.com", int msTimeout = 5000)
        {
            await AsyncHelper.Wait();

            return await IsRemoteReachable(host, 80, msTimeout);
        }

        public static async Task<bool> IsRemoteReachable(string host, int port = 80, int msTimeout = 2000)
        {
            await AsyncHelper.Wait();

            if (string.IsNullOrEmpty(host))
                throw new ArgumentNullException(nameof(host));

            host = host.Replace("http://www.", string.Empty)
                       .Replace("http://", string.Empty)
                       .Replace("https://www.", string.Empty)
                       .Replace("https://", string.Empty)
                       .TrimEnd('/');

            return await Task.Run(() =>
            {
                try
                {
                    var clientDone = new ManualResetEvent(false);
                    var reachable = false;

                    var hostEntry = new DnsEndPoint(host, port);

                    using (var socket = new Socket(SocketType.Stream, ProtocolType.Tcp))
                    {
                        var socketEventArg = new SocketAsyncEventArgs { RemoteEndPoint = hostEntry };
                        socketEventArg.Completed += (s, e) =>
                        {
                            reachable = e.SocketError == SocketError.Success;

                            clientDone.Set();
                        };

                        clientDone.Reset();
                        bool socketWasAbleToConnect = false;

                        // we need to do this with the timout also here because when you are connected
                        // to a wifi which has no internet connection this line run in a endless timeout
                        Task.Run(() =>
                        {
                            socket.ConnectAsync(socketEventArg);
                            socketWasAbleToConnect = true;

                        }).Wait(msTimeout);

                        if (socketWasAbleToConnect)
                        {
                            clientDone.WaitOne(msTimeout);
                        }

                        return reachable;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }
    }
}
