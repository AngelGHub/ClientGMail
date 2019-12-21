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
        private readonly string _pathFileHeaderMailLetter;

        public string PathFileHeaderMailLetter
        {
            get { return _pathFileHeaderMailLetter; }
        }


        public ControlFileGMail(string pathFileHeaderMailLetter)
        {
            _pathFileHeaderMailLetter = pathFileHeaderMailLetter;
        }

        public void SaveHeaderMailLetter(List<string> headerMailLetter)
        {

            using (FileStream fileStream = new FileStream(_pathFileHeaderMailLetter, FileMode.OpenOrCreate))
            {
                foreach (string header in headerMailLetter)
                {
                    byte[] array = Encoding.UTF8.GetBytes(header + Environment.NewLine);

                    int offset = 0;
                    int count = 1024;
                    int countData = array.Length;
                    while (offset < countData)
                    {
                        int value = countData - offset;
                        if (value < count)
                            count = value;

                        fileStream.Write(array, offset, count);

                        offset += count;
                    }
                }

            }

        }
    }
}
