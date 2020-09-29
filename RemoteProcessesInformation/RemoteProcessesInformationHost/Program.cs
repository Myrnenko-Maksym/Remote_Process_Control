using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
//Программа создана студентом 156н группы Мирненко Максимом Дмитриевиичем 30.04.20
namespace RemoteProcessesInformationHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // используется конструкция using для автоматического освобождения всех ресурсов при помощи Dispose() при окончании работы программы
            // создание Host подключая интерфейс ServiceFunctionality
            using (var host = new ServiceHost(typeof(RemoteProcessesInformation.ServiceFunctionality)))
            {
                host.Open();
                Console.WriteLine("Host has been started!");
                Console.ReadLine();
            }
        }
    }
}
