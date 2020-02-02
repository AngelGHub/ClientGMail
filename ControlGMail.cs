using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGMail
{
    class ControlGMail
    {
        const string AUTH_USER_MAIL = "me";

        private readonly GmailService _gmailService;

        public RequestGMail RequestGMail { get; private set; }


        public ControlGMail(UserCredential userCredential, string applicationName)
        {
            _gmailService = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = userCredential,
                ApplicationName = applicationName,
            });

            RequestGMail = new RequestGMail(_gmailService, AUTH_USER_MAIL);
        }

    }
}
