using System;
using System.Collections.Generic;
using System.Text;

namespace Linquistics
{
    public class StateFiniteAutomata : State, IComparable
    {
        private Node node;
        public StateFiniteAutomata(string n)
            : base(n)
        {
        }
        public StateFiniteAutomata(Node nod)
            : base(nod.Name)
        {

        }
        public new int CompareTo(object obj)
        {
            if (obj is StateFiniteAutomata)
            {
                StateFiniteAutomata temp = (StateFiniteAutomata)obj;

                return base.Name.CompareTo(temp.Name);
            }

            throw new ArgumentException("object is not a StateFiniteAutomata");
        }

    }
    
}
