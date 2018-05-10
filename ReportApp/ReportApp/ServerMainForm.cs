using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportApp
{
    public partial class ServerMainForm : Form
    {
        private const int ConnectPort = 8888;
        private readonly TcpListener _listener;
        private int _connectedClients;

        public ServerMainForm()
        {
            InitializeComponent();
            _listener = new TcpListener(IPAddress.Any, ConnectPort);
            _listener.Start();
            serverInfoArea.Text += $"Server started on {ConnectPort} port{Environment.NewLine}";

            new Thread(() =>
            {
                while (true)
                {
                    new ClientThread(_listener.AcceptTcpClient());
                    serverInfoArea.Text += $"{_connectedClients++} client connected{Environment.NewLine}";
                }
            });
        }

        public void AddText(string text) => serverInfoArea.Text += text;

        ~ServerMainForm() => _listener?.Stop();
    }



}
