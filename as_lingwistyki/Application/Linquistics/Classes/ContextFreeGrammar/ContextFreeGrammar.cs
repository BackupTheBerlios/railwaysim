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

        /// <summary>
        /// Copying constructor
        /// </summary>
        /// <param name="parGrammar">Grammar to be copied.</param>
        public ContextFreeGrammar(ContextFreeGrammar parGrammar)
        {
            //copy terminals
            terminals = new ArrayList();
            for (int i = 0; i < parGrammar.terminals.Count; i++)
                terminals.Add(parGrammar.terminals[i]);

            //copy nonTerminals
            nonTerminals = new ArrayList();
            for (int i = 0; i < parGrammar.nonTerminals.Count; i++)
                nonTerminals.Add(parGrammar.nonTerminals[i]);
            
            //copy startSymbol
            startSymbol = parGrammar.startSymbol;

            //copy productions
            productions = new Hashtable();
            for (int i = 0; i < parGrammar.productions.Count; i++)
            {
                ArrayList pom = new ArrayList();
               // IDictionaryEnumerator enumerator =  parGrammar.productions.GetEnumerator();
               //....
            }

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

       

        /// <summary>
        /// Transforms grammar to Chomsky form.
        /// Zakl¹damy, ¿e gramatyka jest ju¿ bez wycieralnych i jednostkowych.
        /// </summary>
        public void transformToChomsky()
        {
            if (isInChomskyForm()) return;

            //1.zast¹pienie symboli terminalnych z prawych stron produkcji nowymi symbolami nieterminalnymi
            //dodanie produkcji z nowych symboli nieterminalnych w terminale

            IDictionaryEnumerator en = productions.GetEnumerator();
            while (en.MoveNext())
            {
                ArrayList productionsSet = (ArrayList)en.Value;
                for (int i = 0; i < productionsSet.Count; i++)//przegl¹dam arrayList
                {
                    if (hasSmallLetter(productionsSet[i].ToString()) && productionsSet[i].ToString().Length > 1)//to zamieñ ma³e na nowe nieterm i dodaj produkcjê
                    {
                        ArrayList doZamiany = new ArrayList();
                        for (int j = 0; j < productionsSet[i].ToString().Length; j++)
                        {
                            if (isSmallLetter(productionsSet[i].ToString()[j]))//zamieñ na nieterm i dodaj produkcjê
                            {
                                doZamiany.Add(productionsSet[i].ToString()[j]);
                            }
                        }//for(int j
                        changeSymbolsAndAddProductions(productionsSet[i].ToString(), doZamiany);

                    }//if (hasSmallLetter)
                }//for
            }///while

            //2.jeœli prawa strona zawiera wiecej niz 2 nieterminale to dodajemy produkcjê z
            //nowego nieterminala w dwa stare

            en = productions.GetEnumerator();//od nowa przegl¹damy
            while (en.MoveNext())
            {
                 ArrayList productionsSet = (ArrayList)en.Value;
                 for (int i = 0; i < productionsSet.Count; i++)//przegl¹dam arrayList
                 {
                     while (productionsSet[i].ToString().Length > 2)//wiecej tylko nieterminali
                     {
                         string newNonTerminal = generateNewCNonTerminal();
                         productionsSet[i] = newNonTerminal + productionsSet[i].ToString().Substring(2);
                     }
                 }//for
            }//while
        }

        private bool isInChomskyForm()
        {
            IDictionaryEnumerator en = productions.GetEnumerator();

            while (en.MoveNext())
            {
                ArrayList productionsSet = (ArrayList)en.Value;
                for (int i = 0; i < productionsSet.Count; i++)
                {
                    //jeœli w stringu s¹ zarówno du¿e jak i ma³e litery 
                    //lub d³ugoœæ stringa wiêksza ni¿ 2 lub równa 0 to nie jest chomsky
                    if (productionsSet[i] == null || productionsSet[i].ToString().CompareTo("") == 0)
                        return false;

                    else if (productionsSet[i].ToString().Length > 2)
                        return false;

                    else if (productionsSet[i].ToString().Length == 2 && hasSmallLetter(productionsSet[i].ToString()))
                        return false;

                    else if (productionsSet[i].ToString().Length == 1 && !hasSmallLetter(productionsSet[i].ToString()))
                        return false;
                }//for
            }//while

            return true;
        }


        #region Pomocnicze

        private bool hasSmallLetter(string parWord)
        {
            string pomWord = parWord.ToUpper();

            if (pomWord.CompareTo(parWord) != 0)
                return true;

            return false;
        }

        private bool isSmallLetter(char c)
        {
            if (c.ToString().ToUpper().CompareTo(c.ToString()) != 0 ) return true;
            return false;
        }

        private int lastNonTermIndex = 0;
        private string generateNewCNonTerminal()
        {
            string newNonTerminal = "C" + lastNonTermIndex.ToString();
            lastNonTermIndex++;
            return newNonTerminal;
        }

        private void changeSymbolsAndAddProductions(string toTransformWord, ArrayList toTransformSymbols)///!!!!!
        {
            for (int i = 0; i < toTransformSymbols.Count; i++)
            {
                string[] tab = toTransformWord.Split((char)toTransformSymbols[i]);
                string newNonTerm = generateNewCNonTerminal();

                toTransformWord = "";
                for (int k = 0; k < tab.Length; k++)
                {
                    toTransformWord += tab[k];
                    if (k < tab.Length - 1) toTransformWord += newNonTerm;
                }

                //dodaj produkcjê
                if (tab.Length > 1)//tzn jeszcze nie wymieniony ten terminal to dodaj produkcjê
                {
                    ArrayList ar = new ArrayList();
                    ar.Add(toTransformSymbols[i].ToString());
                    productions.Add(newNonTerm, ar);
                }
            }
        }


        #endregion

    }
}
