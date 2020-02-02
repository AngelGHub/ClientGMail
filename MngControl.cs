using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGMail
{
    class MngControl
    {
        private const string PATH_FILE_HEADER = @".\HeaderMailLetter.txt";

        private const string APPLICATION_NAME = "ClientGMail";
        private const string PATH_CREDENTIALS = "credentials.json";
        private const string AUTHORIZED_USER = "User";
        private const string TOKEN_PATH = "token.json";        

        private static UserCredential _userCredential;
        private static LetterInfo _letterInfo;
        private static ControlGMail _controlGMail;


        static void Main(string[] args)
        {
            Console.WriteLine("Запуск.");

            //Ожидание авторизации
            AuthorizationWebAsync().Wait();
            if (_userCredential != null) {

                // Связь с сервисом GMail
                ConnectServiceGMail();

                _letterInfo = new LetterInfo();

                // Получение писем
                ReceivingLetters();

                // Сохранение писем
                SaveLetter();
            }

            Console.WriteLine("Завершение.");
        }

        private static async Task AuthorizationWebAsync()
        {         
            Console.WriteLine("Ожидание авторизации ...");
            AuthorizationWebGMail authorizationGMail = new AuthorizationWebGMail(AUTHORIZED_USER, PATH_CREDENTIALS, TOKEN_PATH);
            bool value = await authorizationGMail.AuthorizationAsync();
            if (value) {
                _userCredential = authorizationGMail.UserCredential;
            }
            else {
                Console.WriteLine(authorizationGMail.ErrorMessage);
            }         
        }

        private static void ConnectServiceGMail()
        {
            Console.WriteLine("Связь с сервисом GMail ...");
            _controlGMail = new ControlGMail(_userCredential, APPLICATION_NAME);
        }

        private static void ReceivingLetters()
        {
            RequestGMail requestGMail = _controlGMail.RequestGMail;

            Console.WriteLine("Получение списка заголовков писем ...");
            requestGMail.GetListLetters(_letterInfo);

            int idx = 0;
            int cnt = _letterInfo.Count();
            while (idx < cnt)
            {
                Letter letter = _letterInfo.Item(idx);
                requestGMail.GetLetter(letter);

                idx++;
            }
        }

        private static void SaveLetter()
        {
            Console.WriteLine("Запись списка заголовков писем в файл ...");
            SaveGMail saveGMail = new SaveGMail(PATH_FILE_HEADER);
            saveGMail.SaveHeader(_letterInfo);
        }
    }
}
