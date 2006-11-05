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
            string allTerminals = "";
            string allNonTerminals = "";
            string startSymbol = "";
            int productionsNum = 0;

            //get data from form
            try
            {
                allTerminals = form.terminalsTextBox.Text;
                allNonTerminals = form.nonTerminalsTextBox.Text;
                startSymbol = form.startSymbolComboBox.SelectedText;
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


            //create grammar
            grammar = new ContextFreeGrammar();

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

    }
}
