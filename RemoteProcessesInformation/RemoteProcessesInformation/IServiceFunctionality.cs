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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceFunctionality" in both code and config file together.

    // Создана служба WCF - её представляет 2 интерфейса IServiceFunctionality и IServerFunctionalityCallback
    //атрибут ServiceContract значит, что интерфейс является как таковым правирлом при взаимодействии сервиса и клиента
    //параметр атрибута CallbackContract = typeof(IServerFunctionalityCallback) для обмена данными между интерфейсами
    [ServiceContract(CallbackContract = typeof(IServerFunctionalityCallback))]
    public interface IServiceFunctionality
    {
        // метод для подключения к серверу, параметром метода является имя пользователя
        [OperationContract]
        int Connect(string name);

        // метод для отключения от сервера, параметром метода является порядковый номер пользователя
        [OperationContract]
        void Disconnect(int id);

        //метод для отправки сообщений сервису, создан в перспектииве для дальнейшего усовершенствования программы
        // параметрами метода являются текст и номер пользователя отправителя  
        [OperationContract(IsOneWay = true)]
        void SendMsg(string msg, int id);
    }

    // реализация интерфейса для получения сообщений пользователем
    // параметром метода является текстовое сообщение
    public interface IServerFunctionalityCallback
    {
        [OperationContract(IsOneWay = true)]
        void MsgCallback(string msg);
    }
}
