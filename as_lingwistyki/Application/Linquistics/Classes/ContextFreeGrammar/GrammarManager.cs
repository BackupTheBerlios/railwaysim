using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

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

        public GrammarManager(GrammarForm parGrammarForm)
        {
            this.form = parGrammarForm;
        }

        public void createNewContextFreeGrammarManager()
        {
            //get data from form

            //validate data
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
