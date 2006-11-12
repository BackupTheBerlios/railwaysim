using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;


namespace Linquistics
{
    class ContextFreeGrammar
    {
        //Fields
        /// <summary>
        /// Terminals - each terminal is of type string
        /// </summary>
        ArrayList   terminals;
        /// <summary>
        /// NonTerminals - each nonterminal is of type string
        /// </summary>
        ArrayList   nonTerminals;
        /// <summary>
        /// One of nonterminals - start symbol of grammar - string
        /// </summary>
        string     startSymbol;
        /// <summary>
        /// Has grammar got empty word?
        /// </summary>
        bool hasEmptyWord;
        /// <summary>
        /// Single symbol - symbol of empty word - string
        /// </summary>
        string      emptyWordSymbol;
        /// <summary>
        /// Table of pairs key -> arrayList of results. Each result is of type Word
        /// </summary>
        Hashtable productions;



        public ContextFreeGrammar(ArrayList parTerminals, ArrayList parNonTerminals, string parStartSymbol,
            bool parHasEmptyWord, string parEmptyWordSymbol, Hashtable parProductions)
        {
            terminals = parTerminals;
            nonTerminals = parNonTerminals;
            startSymbol = parStartSymbol;
            productions = parProductions;
            hasEmptyWord = parHasEmptyWord;
            emptyWordSymbol = parEmptyWordSymbol;

            //InitContextFreeGrammar();
        }

        public void InitContextFreeGrammar()
        {
            //SprawdŸ czy s¹ ju¿ nieterminale postaci Cx -  x jest liczb¹
            int max = 0;
            for(int i = 0 ; i < nonTerminals.Count; i++)
            {
                if(nonTerminals[i].ToString()[0].CompareTo("C") == 0)
                {
                    try
                    {
                        max = Int32.Parse(nonTerminals[i].ToString().Substring(1));
                        if(max >= lastNonTermIndex)
                            lastNonTermIndex = max + 1;
                    }
                    catch(Exception ex)
                    {
                    }
                }
            }
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
            ///Each element in list is of type Word
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
            ///Each element in list is of type Word
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



        #region Chomsky

        /// <summary>
        /// Transforms grammar to Chomsky form.
        /// Zakl¹damy, ¿e gramatyka jest ju¿ bez wycieralnych i jednostkowych.
        /// </summary>
        public void transformToChomsky()
        {
            if (isInChomskyForm()) return;

            //1.zast¹pienie symboli terminalnych z prawych stron produkcji (o ile po prawej 
            //jest wiecej ni¿ jeden symbol) nowymi symbolami nieterminalnymi
            //dodanie produkcji z nowych symboli nieterminalnych w terminale

            IDictionaryEnumerator en = productions.GetEnumerator();
            //Terminale do zamiany na nowe nieterminale
            ArrayList doZamiany = new ArrayList();

            while (en.MoveNext())
            {
                //List of Words
                ArrayList productionsSet = (ArrayList)en.Value;
                for (int i = 0; i < productionsSet.Count; i++)//przegl¹dam arrayList Word'ów
                {
                    //jeœli wiêcej ni¿ jeden symbol po prawej stronie
                    //zamieñ terminale na nowe nieterminale i dodaj produkcje
                    //jeœli po prawej dok³adnie jeden symbol to jest to terminal (wynika z usuniêcia jednostkowych)
                    if (((Word)productionsSet[i]).word.Count > 1 && hasTerminal(((Word)productionsSet[i])))
                    {
                       
                        for (int j = 0; j < ((Word)productionsSet[i]).word.Count; j++)
                        {
                            //Jeœli jest terminalem zamieñ na nieterm i dodaj produkcjê
                            if (isTerminal( ((Word)productionsSet[i]).word[j].ToString() ) )
                            {
                                doZamiany.Add(((Word)productionsSet[i]).word[j]);
                            }
                        }//for
                    }//if
                }//for
            }//while
            //Usuñ powtarzaj¹ce siê terminale w liœcie doZamiany
            removeRepetitions(doZamiany);
            //Dodaj produkcje z nowych nieterminali w te terminale
            Hashtable newProductionsRev = addCProductions(doZamiany);
            //Zamieñ terminale w prawych stronach produkcji o d³ugoœci > 1 na nowe nieterminale
            en = productions.GetEnumerator();//od nowa przegl¹damy

            while (en.MoveNext())
            {
                //List of Words
                ArrayList productionsSet = (ArrayList)en.Value;
                for (int i = 0; i < productionsSet.Count; i++)//przegl¹dam arrayList Word'ów dla jednego klucza
                {
                    if (((Word)productionsSet[i]).word.Count > 1 && hasTerminal(((Word)productionsSet[i])))
                    {
                        //zamieñ terminale na nieterminale z newProductionsRev
                        changeTerminalsToNonterminals((Word)productionsSet[i], newProductionsRev);
                    }
                }
            }



            //2.jeœli prawa strona zawiera wiêcej ni¿ 2 nieterminale to dodajemy produkcjê z
            //nowego nieterminala w dwa stare

            en = productions.GetEnumerator();//od nowa przegl¹damy
            while (en.MoveNext())
            {
                 ArrayList productionsSet = (ArrayList)en.Value;
                 for (int i = 0; i < productionsSet.Count; i++)//przegl¹dam arrayList Word'ów dla jednego klucza
                 {
                     while (((Word)productionsSet[i]).word.Count > 2)//wiecej tylko nieterminali
                     {
                         string newNonTerminal = generateNewCNonTerminal();
                         changeFirstTwoIntoOne((Word)productionsSet[i], newNonTerminal);
                     }
                 }//if
            }//while
        }

        #endregion

        #region Pomocnicze

        private bool isInChomskyForm()
        {
            IDictionaryEnumerator en = productions.GetEnumerator();

            while (en.MoveNext())
            {
                //List of Words
                ArrayList productionsSet = (ArrayList)en.Value;
                for (int i = 0; i < productionsSet.Count; i++)
                {
                    //lub d³ugoœæ s³owa wiêksza ni¿ 2 lub równa 0 to nie jest Chomsky
                    if (productionsSet[i] == null || ((Word)productionsSet[i]).word.Count == 0 || ((Word)productionsSet[i]).word.Count > 2)
                        return false;

                    //jeœli jest d³ugosci 2 i ma terminale
                    if(((Word)productionsSet[i]).word.Count == 2 && hasTerminal(((Word)productionsSet[i])) )
                        return false;

                    //jeœli jest d³ugoœci 1 i nie ma nieterminala
                    if (((Word)productionsSet[i]).word.Count == 1 && !hasTerminal(((Word)productionsSet[i])))
                        return false;

                }//for
            }//while

            return true;
        }

        private bool hasTerminal(Word parWord)
        {
            ArrayList pom = parWord.word;

            for (int i = 0; i < pom.Count; i++)
            {
                for (int j = 0; j < terminals.Count; j++)
                {
                    if (pom[i].ToString().CompareTo(terminals[j].ToString()) == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool isTerminal(string parSymbol)
        {
            for (int i = 0; i < terminals.Count; i++)
            {
                if (parSymbol.CompareTo(terminals[i].ToString()) == 0)
                {
                    return true;
                }
            }
            return false;
        }

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


        private int lastNonTermIndex = 1;
        private string generateNewCNonTerminal()
        {
            string newNonTerminal = "C" + lastNonTermIndex.ToString();
            lastNonTermIndex++;
            return newNonTerminal;

        }


        private Hashtable addCProductions(ArrayList parTerminals)
        {
            Hashtable newProductionsRev = new Hashtable();

            for (int i = 0; i < parTerminals.Count; i++)
            {
                string newNonTerm = generateNewCNonTerminal();
                productions.Add(newNonTerm, parTerminals[i]);

                newProductionsRev.Add(parTerminals[i], newNonTerm);
                nonTerminals.Add(newNonTerm);
            }
            return newProductionsRev;
        }


        private void changeTerminalsToNonterminals(Word parWord, Hashtable changes)
        {
            //terminale s¹ inne od nieterminali wiec mogê przeszukiwaæ ca³oœæ
            for (int i = 0; i < parWord.word.Count; i++)
            {
                //kluczem changes s¹ terminale do wymiany, a wynikiem nowy nieterminal
                if (changes[parWord.word[i].ToString()] != null)
                {
                    parWord.word[i] = changes[parWord.word[i].ToString()];
                }
            }
        }


        private void changeFirstTwoIntoOne(Word parWord, string parSymbol)
        {
            parWord.word.RemoveAt(0);
            //teraz drugi jest pierwszy
            parWord.word[0] = parSymbol;
        }


        #endregion

    }
   



}
