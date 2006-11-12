using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Linquistics
{
    /// <summary>
    /// GrammarManager class integrates instance of ContextFreeGrammar class
    /// with GrammarForm
    /// </summary>
    class GrammarManager
    {

        GrammarForm             form;
        ContextFreeGrammar      grammar;
        ContextFreeGrammar      copyOfGrammar;

        public GrammarManager(GrammarForm parGrammarForm)
        {
            this.form = parGrammarForm;
        }

        public void createNewContextFreeGrammarManager()
        {
            string  allTerminals = "";
            string  allNonTerminals = "";
            string  startSymbol = "";
            int     productionsNum = 0;
            bool    isEmptyWord = false;
            string  emptyWordSymbol = "";

            ArrayList terminals = new ArrayList();
            ArrayList nonTerminals = new ArrayList();

            //get data from form
            try
            {
                allTerminals = form.terminalsTextBox.Text;
                allNonTerminals = form.nonTerminalsTextBox.Text;
                startSymbol = form.startSymbolComboBox.SelectedText;
                isEmptyWord = form.hasEmptyWord.Checked;
                emptyWordSymbol = form.emptyWordSymbolTextBox.Text;

                try
                {
                    productionsNum = Int32.Parse(form.productionsNumTextBox.Text);
                }
                catch (FormatException formEx)
                {
                    MessageBox.Show("Z³y format danych podanych jako liczba produkcji! " + formEx.Message);
                    return;
                }
            }
            catch (ArgumentNullException nullEx)
            {
                MessageBox.Show("Nie podane wszystkie wymagane argumenty! "+nullEx.Message);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("B³¹d przy zczytywaniu danych z panela definicji gramatyki. Typ wyj¹ztku: "+ ex.GetType());
                return;
            }

            //validate data
            string[] tab1 = allTerminals.Split(' ');
            for (int i = 0; i < tab1.Length; i++)
            {
                terminals.Add(tab1[i]);//usun¹æ powtórzenia
            }
            string[] tab2 = allNonTerminals.Split(' ');
            for (int i = 0; i < tab2.Length; i++)
            {
                nonTerminals.Add(tab2[i]);//usun¹æ powtórzenia
            }
            removeRepetitions(terminals);
            removeRepetitions(nonTerminals);
            //check if terminals are different from nonTerminals
            if (!differs(terminals, nonTerminals))
            {
                MessageBox.Show("Istniej¹ identyczne terminale i nieterminale! Popraw to i raz jeszcze zatwierdŸ gramatykê");
                return;
            }
            //check if emptyWordWymbol is unique
            if (isEmptyWord && !isUniqueEmptyWordSymbol(emptyWordSymbol, terminals, nonTerminals))
            {
                MessageBox.Show("Symbol s³owa pustego musi byæ unikalny!(Inny ni¿ terminale i nieterminale.");
                return;
            }
            //get productions and verify them
            //...


            //create grammar
            //grammar = new ContextFreeGrammar(terminals, nonTerminals,startSymbol, isEmptyWord, emptyWordSymbol, productions);

            //show information about result of creating
        }

        public void transformToGreibachManager()
        {
            //check if you can do it
            grammar.transformToGreibach();
            //show result on form
        }

        public void deleteWycieralneManager()
        {
            //check if you can do it
            grammar.deleteWycieralne();
            //show result on form
        }

        public void deleteUselessManager()
        {
            //check if you can do it
            grammar.deleteUseless();
            //show result on form
        }

        public void getFirstManager()
        {
            //fields
            string    parNonTerminal;
            ArrayList firstSymbols;

            //initialization
            parNonTerminal = "";
            firstSymbols = new ArrayList();

            //get data from form
            //validate data (also check if the specified word is composed frok
            //terminals defined in grammar)
            firstSymbols = grammar.getFirst(parNonTerminal);

            //show result on form
        }

        public void getFollowManager()
        {
            //fields
            string    parNonTerminal;
            ArrayList followSymbols;

            //initialization
            parNonTerminal = "";
            followSymbols = new ArrayList();

            //get data from form
            //validate data (also check if the specified word is composed frok
            //terminals defined in grammar)
            followSymbols = grammar.getFollow(parNonTerminal);

            //show result on form
        }

        public void checkMembershipManager()
        {
            //fields
            string parWord;
            bool   isMembership;

            //initialization
            parWord = "";
            isMembership = false;

            //get data from form
            //validate data (also check if the specified word is composed frok
            //terminals defined in grammar)

            isMembership = grammar.checkMembership(parWord);

            //show result on form
        }


        public void simulateCheckingMembershipManager()
        {
        }


        #region Pomocnicze
        private void removeRepetitions(ArrayList parSymbols)
        {
            int num = parSymbols.Count;

            for (int i = 0; i < num; i++)
            {
                string pom = parSymbols[i].ToString();
                for (int j = i + 1; j < num; j++)
                {
                    if (pom.CompareTo(parSymbols[j]) == 0)
                    {
                        parSymbols.RemoveAt(j);
                        num--;
                    }
                }
            }
        }

        private bool differs(ArrayList parList1, ArrayList parList2)
        {
            for (int i = 0; i < parList1.Count; i++)
            {
                for (int j = 0; j < parList2.Count; j++)
                {
                    if (parList1[i].ToString().CompareTo(parList2[j].ToString()) == 0)
                        return false;
                }
            }
            return true;
        }

        private bool isUniqueEmptyWordSymbol(string parEmptyWordSymbol, ArrayList parList1, ArrayList parList2)
        {
            for (int i = 0; i < parList1.Count; i++)
            {
                if (parEmptyWordSymbol.CompareTo(parList1[i].ToString()) == 0)
                    return false;
            }
            for (int j = 0; j < parList2.Count; j++)
            {
                if (parEmptyWordSymbol.CompareTo(parList2[j].ToString()) == 0)
                    return false;
            }
            return true;
        }


        #endregion
    }
}
