using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Linquistics
{
    public class Alphabet
    {
        private List<char> characters = new List<char>();

        public bool AddToAlphabet(char c)
        {
            if (!characters.Contains(c)) { characters.Add(c); return true; }
            else return false;
        }
        public void InitFromString(string s)
        {
            characters.Clear();
            string[] tab = s.Split(',');
            for (int i = 0; i < tab.Length; i++)
            {
                tab[i]=tab[i].Trim();
                if (tab[i].Length == 1)
                {
                    characters.Add(tab[i][0]);
                }
            }
        }
        public bool IsInAlphabet(char c)
        {
            if (characters.Contains(c)) return true;
            return false;
        }
        public bool DeleteChar(Char c)
        {
            return this.characters.Remove(c);
        }
        public void WriteAlphabetToListView(ListView lv)
        {
            for (int i = 0; i < characters.Count; i++)
            {
                lv.Items.Add(characters[i].ToString());
            }
        }
    }
}
