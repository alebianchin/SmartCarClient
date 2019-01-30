using System;
using System.Threading;
using Newtonsoft.Json;

namespace SmartCarClient
{
    class Program
    {
        static void Main(string[] args)
        {

            MessageQueuing queue = new MessageQueuing();
            ThreadStart threadDelegate = new ThreadStart(AMQPSender.SendData);
            Thread t = new Thread(threadDelegate);
            CarReading carReading = new CarReading();
            t.Start();

            while (true)
            {
                string msg = JsonConvert.SerializeObject(carReading.GetReading());
                queue.addMessage(msg);

                Console.WriteLine("Added to queue : \n {0}", msg);

                Thread.Sleep(1000);
            }
        }
    }

}



