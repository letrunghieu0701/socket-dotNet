using System;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Constants
{
    public const string DISCONNECT_MSG = "DISCONNECT";
}

namespace Test_server
{
    internal class Program
    {                                   // Server sided
        static void Main(string[] args)
        {
            // Mở kết nối và listen client
            TcpListener server = new TcpListener(System.Net.IPAddress.Any, 1302);
            server.Start();

            while(true)
            {
                Console.WriteLine("Waiting for connection from a client...");

                // Accecpt và lấy ra request từ client
                try
                {
                    TcpClient client = server.AcceptTcpClient();

                    StreamReader sr = new StreamReader(client.GetStream());
                    StreamWriter sw = new StreamWriter(client.GetStream());

                    string request = sr.ReadLine();
                    Console.WriteLine("Client say: " + request);
                    

                    
                    sw.WriteLine("Hi, I'm the server");
                    sw.Flush();
                    //Console.ReadKey();

                    if (request == Constants.DISCONNECT_MSG)
                    {
                        Console.WriteLine("Closing server...");
                        sr.Close();
                        sw.Close();
                        client.Close();
                        server.Stop();
                        break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong");
                    Console.WriteLine(ex.Message);
                }
            }
            Console.WriteLine("Server closed...");
            Console.ReadKey();
        }
    }
}
