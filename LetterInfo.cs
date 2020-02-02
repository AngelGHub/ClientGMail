using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGMail
{
    class LetterData
    {
        public string Header { get; set; }
    }

    class Letter
    {
        public string ID { get; set; }
        public LetterData LetterData { get; set; }


        public Letter()
        {
            LetterData = new LetterData();
        }
    }

    class LetterInfo
    {
        private readonly List<Letter> _listLetter;


        public LetterInfo()
        {
            _listLetter = new List<Letter>();
        }

        public void Add(Letter letter)
        {
            _listLetter.Add(letter);
        }

        public void Delete(Letter letter)
        {
            _listLetter.Remove(letter);
        }

        public int Count()
        {
            return _listLetter.Count;
        }

        public Letter Item(int idx)
        {
            return _listLetter[idx];
        }
    }
}
