using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Net.Sockets;
using Tobii.Interaction;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Net;
using System.Net.Sockets;


namespace WpfApp1
{

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


    public partial class MainWindow : Window
    {

        private GazePointDataStream gazePointDataStream;

        //private UdpClient udpClient;
        //private Timer timer0;
        //private int cnt = 0;
        private Byte[] data = { 0, 1, 2, 3 };
        private int[] gazeDataDummy = { 200, 200 };
        private Random rnd;

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                //this.udpClient = new UdpClient(8888);
                //this.udpClient.Connect("127.0.0.1", 8888);
                //this.udpClient.Send(data, data.Length);

               
                var host = new Host();
                //this.label.Content = host;

                //host.InitializeWpfAgent();

                gazePointDataStream = host.Streams.CreateGazePointDataStream(Tobii.Interaction.Framework.GazePointDataMode.Unfiltered);
                gazePointDataStream.GazePoint((x, y, ts) =>
                {
                    this.label.Content = "x=" + x + " y=" + y;

                    String str = x.ToString() + "\n" + y.ToString();
                    this.label.Content = str;
                    var sender = UdpUser.ConnectTo("127.0.0.1", 8888);
                    sender.Send(str);
                    this.label.Content = str;
                });

                this.rnd = new Random();
                this.button0.Click += new RoutedEventHandler(this.onClick);
                /*
                var currentWindowHandle = Process.GetCurrentProcess().MainWindowHandle;
                var interactorAgent = host.InitializeVirtualInteractorAgent(currentWindowHandle, "xxx");
                Rectangle rect = new Rectangle(0,0,1000,1000);

                interactorAgent
                    .AddInteractorFor(rect)
                    .WithGazeAware()
                    .HasGaze(() => this.label.Content = "gaze")
                    .LostGaze(() => this.label.Content = "no gaze");
                */
                this.label.Content = "OK";
            }
            catch (Exception e)
            {
                this.label.Content = e;
            }
        }

        private void onClick(Object Sender, EventArgs e)
        {
            this.gazeDataDummy[0] = this.rnd.Next(100, 800);
            this.gazeDataDummy[1] = this.rnd.Next(100, 800);
            String str = this.gazeDataDummy[0].ToString() + "\n" + this.gazeDataDummy[1].ToString();
            //this.udpClient.Send(Encoding.ASCII.GetBytes(str), str.Length);
                
            var sender = UdpUser.ConnectTo("127.0.0.1", 8888);
            sender.Send(str);
            this.label.Content = str;
        }


        /*
        private void Update(Object state)
        {
            lock(this)
            {
                //this.label.Content = this.cnt;
                //this.cnt++;
                try
                {
                    Random rnd = new Random();
                    this.gazeDataDummy[0] = rnd.Next(100, 800);
                    this.gazeDataDummy[1] = rnd.Next(100, 800);
                    String str = this.gazeDataDummy[0].ToString() + "\n" + this.gazeDataDummy[1].ToString();
                    //this.udpClient.Send(Encoding.ASCII.GetBytes(str), str.Length); // data.Length);
                    //Console.Write("ok");
                    //this.label.Content = "ok";
                }
                catch (Exception e)
                {
                    this.label.Content = e;
                    //Console.Write(e);
                }
            }
        }
        */
    }
}

/*
 * using System;
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
}
*/

