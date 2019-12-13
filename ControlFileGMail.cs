using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGMail
{
    class ControlFileGMail
    {
        readonly string _path = @".\ListGmailMsg.txt";

        public void SaveToFIle(List<string> list)
        {

            using (FileStream fileStream = new FileStream(_path, FileMode.OpenOrCreate))
            {
                foreach (var txt in list)
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(txt + "\n");

                    fileStream.Write(array, 0, array.Length);
                }

            }

        }
    }
}
