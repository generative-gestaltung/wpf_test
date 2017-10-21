using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace WpfApp1
{

    /*
    public struct Received
    {
        public IPEndPoint Sender;
        public string Message;
    }


    abstract class UdpBase
    {
        protected UdpClient Client;

        protected UdpBase()
        {
            Client = new UdpClient();
        }

        public async Task<Received> Receive()
        {
            var result = await Client.ReceiveAsync();
            return new Received()
            {
                Message = Encoding.ASCII.GetString(result.Buffer, 0, result.Buffer.Length),
                Sender = result.RemoteEndPoint
            };
        }
    }

    //Server
    class UdpListener : UdpBase
    {
        private IPEndPoint _listenOn;

        public UdpListener(int port) : this(new IPEndPoint(IPAddress.Any, port))
        {

        }

        public UdpListener(IPEndPoint endpoint)
        {
            _listenOn = endpoint;
            Client = new UdpClient(_listenOn);
        }
    }

    //Client
    class UdpUser : UdpBase
    {
        private UdpUser() { }

        public static UdpUser ConnectTo(string hostname, int port)
        {
            var connection = new UdpUser();
            connection.Client.Connect(hostname, port);
            return connection;
        }

        public void Send(string message)
        {
            var datagram = Encoding.ASCII.GetBytes(message);
            Client.Send(datagram, datagram.Length);
        }
    }

}



/*

class Program
{
    static int MODE_SEND = 0;
    static int MODE_RECV = 1;

    static void Main(string[] args)
    {
        int mode = MODE_SEND;

        if (mode == MODE_RECV)
        {
            //create a new server
            var server = new UdpListener(8888);

            //start listening for messages and copy the messages back to the client
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    var received = await server.Receive();
                    Console.WriteLine ("recv: "+received.Message);
                }
            });
            while (true) { }
        }


        if (mode == MODE_SEND)
        {
            Random rand = new Random();
            var client = UdpUser.ConnectTo("127.0.0.1", 8888);

            while (true)
            {
                int x = rand.Next(0, 1000);
                int y = rand.Next(0, 1000);

                client.Send (x.ToString()+"\n"+y.ToString());
                Thread.Sleep(100);
                //Console.WriteLine("sent");
            }
        }
    }
    */
}