using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGMail
{
    class RequestGMail
    {
        private const string HEADER_LETTER = "Subject";

        private GmailService _gmailService;
        private readonly string _authUserMail;


        public RequestGMail(GmailService gmailService, string authUserMail)
        {
            _gmailService = gmailService;
            _authUserMail = authUserMail;
        }

        public void GetListLetters(LetterInfo letterInfo)
        {
            UsersResource.MessagesResource.ListRequest listRequest = _gmailService.Users.Messages.List(_authUserMail);
            foreach (var msgItm in listRequest.Execute().Messages)
            {
                Letter letter = new Letter();
                letter.ID = msgItm.Id;

                letterInfo.Add(letter);
            }
        }

        public void GetLetter(Letter letter)
        {
            Message message = _gmailService.Users.Messages.Get(_authUserMail, letter.ID).Execute();

            IList<MessagePartHeader> headers = message.Payload.Headers;
            foreach (MessagePartHeader partHeader in headers)
            {
                if (partHeader.Name == HEADER_LETTER)
                {
                    letter.LetterData.Header = partHeader.Value;
                    return;
                }
            }
        }
    }
}
