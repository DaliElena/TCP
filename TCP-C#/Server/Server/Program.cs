using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        const int port = 8888; 
        static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);
                server.Start();

                while (true)
                {
                    Console.WriteLine("Ожидание подключений... ");

                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Подключен клиент. Выполнение запроса...");

                    NetworkStream stream = client.GetStream();
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    byte[] dataClient = new byte[64];
                    do
                    {
                        bytes = stream.Read(dataClient, 0, dataClient.Length);
                        builder.Append(Encoding.Unicode.GetString(dataClient, 0, bytes));
                    }
                    while (stream.DataAvailable);
                    string message = builder.ToString();
                    Console.WriteLine(message);
                    // сообщение для отправки клиенту
                    string response;
                    if (int.TryParse(message, out int number))
                    {
                         response = LoremNET.Lorem.Words(Convert.ToInt32(message));
                    }
                    else
                    {
                        response = "Ошибка ввода. Введите число и повторите попытку";
                    }
                    
                    byte[] data = Encoding.Unicode.GetBytes(response);
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Отправлено сообщение: {0}", response);
                    stream.Close();
                    client.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (server != null)
                    server.Stop();
            }
        }
    }
}
