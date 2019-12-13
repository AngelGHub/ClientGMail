using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGMail
{
    class MngControl
    {

        static void Main(string[] args)
        {
            Control();
        }

        static void Control()
        {
            Console.WriteLine("Запуск.");

            List<string> _list = new List<string>();

            Console.WriteLine("Связь с сервисом GMail.");
            ControlRequestGMail controlRequestGMail = new ControlRequestGMail();
            controlRequestGMail.Init();

            Console.WriteLine("Получение списка писем.");
            int i = 0;
            int cnt = controlRequestGMail.GetCountMsg();
            while (i < cnt)
            {
                string txt = controlRequestGMail.GetMsg(i);
                _list.Add(txt);

                i++;
            }

            Console.WriteLine("Запись списка писем в файл.");
            ControlFileGMail controlFileGMail = new ControlFileGMail();
            controlFileGMail.SaveToFIle(_list);
        }
    }
}
