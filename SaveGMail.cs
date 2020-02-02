using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGMail
{
    class SaveGMail
    {
        private readonly string _pathFileHeader;


        public SaveGMail(string pathFileHeader)
        {
            _pathFileHeader = pathFileHeader;
        }

        public void SaveHeader(LetterInfo letterInfo)
        {

            using (FileStream fileStream = new FileStream(_pathFileHeader, FileMode.OpenOrCreate))
            {
                int idx = 0;
                int cnt = letterInfo.Count();
                while (idx < cnt)
                {
                    Letter letter = letterInfo.Item(idx);

                    byte[] array = Encoding.UTF8.GetBytes(letter.LetterData.Header + Environment.NewLine);

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

                    idx++;
                }

            }

        }
    }
}
