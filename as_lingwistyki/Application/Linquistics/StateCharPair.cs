using System;
using System.Collections.Generic;
using System.Text;

namespace Linquistics
{
    public class StateCharPair : IComparable
    {
     /// Attributes

     /// Attribute singleChar
        private Char singleChar;

     /// Attribute currentState
        private StateFiniteAutomata currentState;


        public StateCharPair(StateFiniteAutomata state, Char c)
        {
            singleChar = c;
            currentState = state;
        }
        public int CompareTo(object obj)
        {
            if (obj is StateCharPair)
            {
                StateCharPair temp = (StateCharPair)obj;
                if (currentState == null || temp.currentState == null) return -1;
                int stcomp=currentState.CompareTo(temp.currentState);
                if (stcomp == 0) return singleChar.CompareTo(temp.singleChar);
                else return stcomp;
            }

            throw new ArgumentException("object is not a StateCharPair");
        }
        public string StateName
        {
            get 
            {
                if (currentState != null)
                    return currentState.Name;
                else return "";
            }
        }
        public char Character
        {
            get { return this.singleChar; }
        }
    }
}
