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
        private const string ApplicationName = "ClientGMail";
        private const string UserAuth = "User";
        private const string TokenPath = "token.json";
        private const string CredentialsPath = "credentials.json";
        private const string AuthUserMail = "me";
        private const string HeaderMessage = "Subject";

        private readonly string[] _scopesService = { GmailService.Scope.GmailReadonly };

        private GmailService _gmailService;


        public ControlRequestGMail()
        {
            ConnectGMailService();
        }

        public void ConnectGMailService()
        {
            if (!File.Exists(CredentialsPath))
                return;

            UserCredential userCredential;
            using (FileStream credentialsData = new FileStream(CredentialsPath, FileMode.Open, FileAccess.Read))
            {
                userCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(credentialsData).Secrets,
                    _scopesService,
                    UserAuth,
                    CancellationToken.None,
                    new FileDataStore(TokenPath, true)).Result;
            }

            _gmailService = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = userCredential,
                ApplicationName = ApplicationName,
            });
        }


        public int GetCountLetter()
        {
            if (_gmailService == null)
                return -1;

            UsersResource.MessagesResource.ListRequest dataUserMessages = _gmailService.Users.Messages.List(AuthUserMail);
            return dataUserMessages.Execute().Messages.Count;
        }

        public string GetHeaderLetter(int idx)
        {
            if (_gmailService == null)
                return "";

            UsersResource.MessagesResource.ListRequest dataUserMessages = _gmailService.Users.Messages.List(AuthUserMail);
            var msgItem = dataUserMessages.Execute().Messages[idx];
            var msgReq = _gmailService.Users.Messages.Get(AuthUserMail, msgItem.Id).Execute();

            var headers = msgReq.Payload.Headers;   // Список элементов сообщения
            foreach (MessagePartHeader headItm in headers)
            {
                if (headItm.Name == HeaderMessage)
                {
                    return headItm.Value;
                }
            }

            return "";
        }
    }
}
