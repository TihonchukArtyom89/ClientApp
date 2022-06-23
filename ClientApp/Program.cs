using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
namespace ClientApp
{
    class Program
    {
        static void Communicate(string hostname,int port)
        {
            //Буфер для входящих данных
            byte[] bytes = new byte[1024];
            //Соединяемся с удалённым сервером
            //Устанавливаем удалённую конечную точку(сервер) для сокета
            IPHostEntry ipHost = Dns.GetHostEntry(hostname);//подобнее о хост энтри
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);//подобнее о конечной точке сокета
            Socket sock = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //Подключаемся к серверу
            sock.Connect(ipEndPoint);
            Console.Write("Введите сообщение: ");
            string message = Console.ReadLine();
            Console.WriteLine("Подключаемся к порту {0}", sock.RemoteEndPoint.ToString());
            byte[] data = Encoding.UTF8.GetBytes(message);
            //ПОлучаем количество отправленных байтов
            int bytesSent = sock.Send(data);
            //получаем ответ от сервера , bytesRec количество принятых байтов
            int bytesRec = sock.Receive(bytes);
            Console.WriteLine("\n Ответ от сервера: {0}\n\n", Encoding.UTF8.GetString(bytes, 0, bytesRec));
            //Вызываем Communicate() ещё
            if(message.IndexOf("<TheEnd>")==-1)
            {
                Communicate(hostname, port);
            }
            sock.Shutdown(SocketShutdown.Both);
            sock.Close();
        }
        static void Main(string[] args)
        {
            try
            {
                Communicate("localhost",8888);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникло необработанное исключение!\n" + ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}