using System;
using System.Net.Sockets;
using System.IO;
using System.Text;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class Constants
{
    public const string DISCONNECT_MSG = "DISCONNECT";
}

namespace Test_client
{
    internal class Program
    {                           // Client sided
        static void Main(string[] args)
        {
            connection:
            try
            {
                string ip_addr_server = "192.168.100.40";
                // thiết lập kết nối đến server
                TcpClient client = new TcpClient(ip_addr_server, 1302);
                Console.WriteLine("Waiting to connect to server...");
                

                // gửi data cho server
                string msg = "huhu, hello world";
                StreamWriter sw = new StreamWriter(client.GetStream());
                StreamReader sr = new StreamReader(client.GetStream());
                
                sw.WriteLine(Constants.DISCONNECT_MSG);
                sw.Flush();

                Console.WriteLine("Server respone: " + sr.ReadLine());


                Console.WriteLine("Closing client...");
                client.Close();
                sw.Close();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to connect, retrying...");
                Console.WriteLine(ex.Message);
                goto connection;
            }
            Console.WriteLine("Client closed...");
            Console.ReadKey();
        }
    }
}
