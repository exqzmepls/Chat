﻿using Common.ClientListeners;
using Common.ClientWriters;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Common.Servers
{
    public class TcpSocketServer : IServer
    {
        private readonly TcpListener _tcpListener;

        public TcpSocketServer()
        {
            _tcpListener = new TcpListener();
        }

        public void Start(Action<IClientListener, IClientWriter> onAcceptedClient)
        {
            _tcpListener.Start();
            Task.Run(() =>
            {
                while (true)
                {
                    var acceptedTcpClient = _tcpListener.AcceptTcpClient();
                    var clientListener = new TcpSocketClientListener(acceptedTcpClient);
                    var clientWriter = new TcpSocketClientWriter(acceptedTcpClient);
                    onAcceptedClient(clientListener, clientWriter);
                }
            });
        }

        public void Stop()
        {
            _tcpListener.Stop();
        }
    }
}
