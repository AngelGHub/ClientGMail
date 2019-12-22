using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGMail
{
    class MngControl
    {
        private static string _pathFileHeaderMailLetter = @".\HeaderMailLetter.txt";


        static void Main(string[] args)
        {
            Control();
        }

        static void Control()
        {
            Console.WriteLine("Запуск.");

            Console.WriteLine("Связь с сервисом GMail ...");
            ControlRequestGMail controlRequestGMail = new ControlRequestGMail();

            Console.WriteLine("Получение списка заголовков писем ...");
            List<string> headerMailLetter = new List<string>();
            GetHeaderLetter(controlRequestGMail, headerMailLetter);

            if (headerMailLetter.Count > 0)
            {
                Console.WriteLine("Запись списка писем в файл ...");
                ControlFileGMail controlFileGMail = new ControlFileGMail(_pathFileHeaderMailLetter);
                controlFileGMail.SaveHeaderMailLetter(headerMailLetter);
            }

            Console.WriteLine("Завершение.");
        }

        static void GetHeaderLetter(ControlRequestGMail controlRequestGMail, List<string> headerMailLetter)
        {
            int i = 0;
            int cnt = controlRequestGMail.GetCountLetter();
            while (i < cnt)
            {
                string header = controlRequestGMail.GetHeaderLetter(i);
                headerMailLetter.Add(header);

                i++;
            }
        }
    }
}
