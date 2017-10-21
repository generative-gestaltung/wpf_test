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
//using System.Windows.Shapes;
using System.Net.Sockets;
using Tobii.Interaction;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private GazePointDataStream gazePointDataStream;

        private UdpClient udpClient;
        private Timer timer0;
        //private int cnt = 0;
        private Byte[] data = { 0, 1, 2, 3 };
        private int[] gazeDataDummy = { 200, 200 };
        private Random rnd;

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                this.udpClient = new UdpClient(8888);
                this.udpClient.Connect("127.0.0.1", 8888);
                //this.udpClient.Send(data, data.Length);

                var host = new Host();
                gazePointDataStream = host.Streams.CreateGazePointDataStream(Tobii.Interaction.Framework.GazePointDataMode.Unfiltered);
                gazePointDataStream.GazePoint((x, y, ts) =>
                {
                    this.label.Content = "x=" + x + " y=" + y;

                    String str = x.ToString() + "\n" + y.ToString();
                    this.label.Content = str;
                    this.udpClient.Send(Encoding.ASCII.GetBytes(str), str.Length);
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
            this.label.Content = str;
            this.udpClient.Send(Encoding.ASCII.GetBytes(str), str.Length);
        }

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
                    this.udpClient.Send(Encoding.ASCII.GetBytes(str), str.Length); // data.Length);
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
    }
}
