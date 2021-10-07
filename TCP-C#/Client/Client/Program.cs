using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        const int port = 8888;
        const string address = "127.0.0.1";
        static void Main(string[] args)
        {     
            TcpClient client = null;
            try
            {
                while (true)
                {
                    client = new TcpClient(address, port);
                    NetworkStream stream = client.GetStream();
                    Console.WriteLine("Введите число");
                    string message = Console.ReadLine();
                    message = String.Format("{0}", message);  
                    byte[] data = Encoding.Unicode.GetBytes(message);                 
                    stream.Write(data, 0, data.Length);

                    data = new byte[64];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);


                    message = builder.ToString();
                    Console.WriteLine("Сервер: {0}", message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Close();
            }
        }
    }
}
