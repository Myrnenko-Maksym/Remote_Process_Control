using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RemoteProcessesInformationClient.ServiceFunctionality;
//Программа создана студентом 156н группы Мирненко Максимом Дмитриевиичем 30.04.20
namespace RemoteProcessesInformationClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IServiceFunctionalityCallback
    {
        //триггер для проверки подключен ли пользователь
        bool isConnected = false;
        //создание объекта для клиента
        ServiceFunctionalityClient client;
        // ID пользователя
        int ID;
        //создание экземпляра класса для просмотров активных процессов
        private Process[] _processes = Process.GetProcesses();
        // запуск приложения начинается с этого метода (он по умолчанию)
        public MainWindow()
        {
            InitializeComponent();
        }
        // выделение памяти когда пользователь загружен
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            client = new ServiceFunctionalityClient(new System.ServiceModel.InstanceContext(this));
        }
        // метод подключения пользователя к сети если пользователь не подключен и меняется подпись кнопки
        void ConnectUser()
        {
            if (!isConnected)
            {
                ID = client.Connect(tbUserName.Text);
                tbUserName.IsEnabled = false;
                buttonConnection.Content = "Disconnect";
                isConnected = true;
            }
        }
        // метод отключения пользователя к сети если пользователь подключен и меняется подпись кнопки
        void DisconnectUser()
        {
            if (isConnected)
            {
                client.Disconnect(ID);
                tbUserName.IsEnabled = true;
                buttonConnection.Content = "Connect";
                isConnected = false;
            }
        }
        // прописываются действия по нажатию кнопки Connect/Disconnect
        private void ButtonConnection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isConnected)
                {
                    DisconnectUser();
                    lbInfo.Items.Clear();
                }
                else
                {
                    if (tbUserName.Text != "Input user Name!" && tbUserName.Text != "" && tbUserName.Text != " ")
                    {
                        ConnectUser();
                    }
                    
                }
            }
            catch
            {
                MessageBox.Show("Oops, something is going wrong! Go to Help tab to read some features or read manual, part 3, please.");
            }
        }
        // отображение процессов другого пользователя
        public void MsgCallback(string msg)
        {
            
            lbInfo.Items.Clear();
            if (isConnected)
            {
                foreach (Process process in _processes)
                {
                    lbInfo.Items.Add(process.ProcessName);
                }
            }
            
            lbInfo.Items.Add(msg);

            //foreach (Process process in _processes)
            //{
            //    lbInfo.Items.Add(process.ProcessName);
            //}
        }
        // действие прилоджения по нажатию на крестик слева сверху
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }
        
        // действие прилоджения по нажатию на кнопку Close
        private void CloseTab_Click(object sender, RoutedEventArgs e)
        {
            DisconnectUser();
            Close();
        }
        // действие прилоджения по нажатию на кнопку About
        private void AboutTab_Click(object sender, RoutedEventArgs e)
        {
            Window aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }
        // действие прилоджения по нажатию на кнопку Help
        private void HelpTab_Click(object sender, RoutedEventArgs e)
        {
            Window helpWindow = new HelpWindow();
            helpWindow.ShowDialog();
        }

        //private void ButtonInfo_Click( object sender, RoutedEventArgs e)
        //{
        //    //lbInfo.Items.Clear();
        //    //if (isConnected)
        //    //{
        //    //    foreach (Process process in _processes)
        //    //    {
        //    //        lbInfo.Items.Add(process.ProcessName);
        //    //    }
        //    //}
        //}
    }
}
