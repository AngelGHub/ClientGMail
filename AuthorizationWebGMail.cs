using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientGMail
{
    class AuthorizationWebGMail
    {    
        const int CANCEL_WAIT_AUTH = 60;

        private readonly string _userAuth;
        private readonly string _сredentialsPath;
        private readonly string _tokenPatch;
        private readonly string[] _scopesService;       

        public UserCredential UserCredential { get; private set; }
        public string ErrorMessage { get; private set; }


        public AuthorizationWebGMail(string userAuth, string сredentialsPath, string tokenPatch)
        {
            _userAuth = userAuth;
            _сredentialsPath = сredentialsPath;
            _tokenPatch = tokenPatch;

            string[] _scopesService = { GmailService.Scope.GmailReadonly };
        }

        public async Task<bool> AuthorizationAsync()
        {
            if (!File.Exists(_сredentialsPath)) {
                ErrorMessage = "Не найден файл учётных данных пользователя (credentials.json).";
                return false;
            }

            using (FileStream credentialsData = new FileStream(_сredentialsPath, FileMode.Open, FileAccess.Read))
            {
                CancellationTokenSource ctsWaitAuth = new CancellationTokenSource();
                try
                {
                    ctsWaitAuth.CancelAfter(TimeSpan.FromSeconds(CANCEL_WAIT_AUTH));

                    UserCredential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(credentialsData).Secrets,
                        _scopesService,
                        _userAuth,
                        ctsWaitAuth.Token,
                        new FileDataStore(_tokenPatch, true));

                    return true;
                }
                catch (OperationCanceledException)
                {
                    ErrorMessage = "Не удалось авторизоваться за отведённое время.";
                    return false;
                }
            }
        }
    }
}
