using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;


namespace Linquistics
{
    class ContextFreeGrammar
    {
        //Fields
        ArrayList   terminals;
        ArrayList   nonTerminals;
        String      startSymbol;

        /// <summary>
        /// Table of pairs key -> set of results
        /// </summary>
        Hashtable productions;

        public ContextFreeGrammar()
        {
            terminals = new ArrayList();
            nonTerminals = new ArrayList();
            startSymbol = "";
            productions = new Hashtable();
        }

        //Transfors grammar's productions to Greibach normal form
        public void transformToGreibach()
        {
        }

        /// <summary>
        /// Deletes wycieralne nonterminals and productions in context-free grammar
        /// </summary>
        public void deleteWycieralne()
        {
        }

        /// <summary>
        /// Deletes useless nonterminals and productions in context-free grammar
        /// </summary>
        public void deleteUseless()
        {
        }

        /// <summary>
        /// Returns list of 'First'- symbols (set of terminals)
        /// </summary>
        /// <param name="parNonTerminal">Nonterminal for which 'First'- symbols are obtained</param>
        /// <returns>List of 'First'-symbols for specified nonterminal</returns>
        public ArrayList getFirst(string parNonTerminal)
        {
            ArrayList firstSymbols;
            firstSymbols = new ArrayList();

            return firstSymbols;
        }

        /// <summary>
        /// Returns list of 'Follow'- symbols (set of terminals)
        /// </summary>
        /// <param name="parNonTerminal">Nonterminal for which 'Follow'- symbols are obtained</param>
        /// <returns>List of 'Follow'-symbols for specified nonterminal</returns>
        public ArrayList getFollow(string parNonTerminal)
        {
            ArrayList followSymbols;
            followSymbols = new ArrayList();

            return followSymbols;
        }


        /// <summary>
        /// Checks if word belongs to grammar - could be obtained from
        /// start symbol using grammar's productions.
        /// </summary>
        /// <param name="parWord"></param>
        /// <returns></returns>
        public bool checkMembership(string parWord)
        {
            bool isMembership;
            isMembership = false;


            return isMembership;
        }

        public void oneStepInCheckingMembership(string parCurrWord)
        {
        }
    }
}
