using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
//Программа создана студентом 156н группы Мирненко Максимом Дмитриевиичем 30.04.20
namespace RemoteProcessesInformation
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceFunctionality" in both code and config file together.

    // В этом класе осуществляется реализация интерфейса IServiceFunctionality
    // все клиенты подключающиеся к серверу работают в одном канале, это реализовано с помощью атрибута ServiceBehavior
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceFunctionality : IServiceFunctionality
    {
        // список пользователей
        List<ServerUser> users = new List<ServerUser>();
        //поле для генерации Id пользователей
        int nextId = 1;
        //private Process[] _processes = Process.GetProcesses();

        public int Connect(string name)
        {
            // создание пользователя
            ServerUser user = new ServerUser()
            {
                ID = nextId,
                Name = name,
                operationContext = OperationContext.Current
            };
            nextId++;

            SendMsg(": " + user.Name + " подключился к чату!", 0);

            // добавление пользователя в список
            users.Add(user);
            return user.ID;
        }

        public void Disconnect(int id)
        {
            // удаление пользователя
            var user = users.FirstOrDefault(i => i.ID == id);
            if (user != null)
            {
                users.Remove(user);
                SendMsg(": " + user.Name + " покинул чат!", 0);
            }
        }

        // формирование сообщения сервера пользователям, о подключении / отключении пользователя
        public void SendMsg(string msg, int id)
        {
            foreach (var item in users)
            {
                string answer = DateTime.Now.ToShortTimeString();

                var user = users.FirstOrDefault(i => i.ID == id);
                if (user != null)
                {
                    answer += ": " + user.Name + " ";
                }
                answer += msg;
                item.operationContext.GetCallbackChannel<IServerFunctionalityCallback>().MsgCallback(answer);
            }
        }
    }
}
