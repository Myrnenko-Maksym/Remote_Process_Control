using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
//Программа создана студентом 156н группы Мирненко Максимом Дмитриевиичем 30.04.20
namespace RemoteProcessesInformation
{
    // контейнер для хранения информации о пользователе
    class ServerUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public OperationContext operationContext { get; set; }
    }
}
