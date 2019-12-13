using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.IO;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace ClientGMail
{
    class ControlRequestGMail
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/gmail-dotnet-quickstart.json
        static string[] Scopes = { GmailService.Scope.GmailReadonly };
        static string ApplicationName = "ClientGMail";

        private GmailService _service;
        private UsersResource.MessagesResource.ListRequest _request;


        public void Init()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Gmail API service.
            _service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            _request = _service.Users.Messages.List("me");
        }


        public int GetCountMsg()
        {
            return _request.Execute().Messages.Count;
        }

        public string GetMsg(int idx)
        {
            var msgItem = _request.Execute().Messages[idx];
            var msgReq = _service.Users.Messages.Get("me", msgItem.Id).Execute();

            var headers = msgReq.Payload.Headers;   // Список элементов сообщения
            foreach (var headItm in headers)
            {
                if (headItm.Name == "Subject")
                {
                    return headItm.Value;
                }
            }

            return "";
        }
    }
}
