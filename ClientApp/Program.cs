using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
namespace ClientApp
{
    class Program
    {
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
/*
namespace OneThreadServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Однопоточный сервер запущен!");
            //Подготавливаем конечную точку сокета
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");//подобнее о хост энтри
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint iPEndPoint = new IPEndPoint(ipAddr, 8888);//подобнее о конечной точке сокета
            //Создаём потоковый сокет по протоколу TCP/IP
            Socket sock = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {//связываем сокет с конечной точкой
                sock.Bind(iPEndPoint);
                //начинаем прослушку сокета
                sock.Listen(10);
                while(true)
                {//Начинаем слушать соединения в бесконечном цикле
                    Console.WriteLine("Слушаем, порт {0}",iPEndPoint);
                    //Программа приостанавливается, ожидая входящее соединение
                    //сокет для обмена данными с клиентом
                    Socket s = sock.Accept();
                    //сюда будут записаны данные от клиента
                    string data = null;
                    //Клиент есть, начинаем читать его запрос
                    //массив полученных данных
                    byte[] bytes = new byte[1024];
                    //Длина полученных данных 
                    int bytesCount = s.Receive(bytes);
                    //Декодируем данные
                    data += Encoding.UTF8.GetString(bytes, 0, bytesCount);
                    //Показываем обработанные данные
                    Console.WriteLine("Данные от клиента: " + data + "\n\n");
                    //Генерируем ответ клиенту(размер в байтах его запроса)
                    string reply = "Query size: " + data.Length.ToString() + " chars!!!";
                    //Кодируем ответ сервера
                    byte[] msg = Encoding.UTF8.GetBytes(reply);
                    //Отправляем ответ сервера клиенту
                    s.Send(msg);
                    if(data.IndexOf("<TheEnd>")>-1)
                    {
                        Console.WriteLine("Соединение закрыто.");
                        break;
                    }
                    s.Shutdown(SocketShutdown.Both);
                    s.Close();
                }
            }
            catch(Exception ex)
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
*/