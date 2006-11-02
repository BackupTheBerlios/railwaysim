using System;
using System.Collections.Generic;
using System.Text;

namespace Linquistics
{
    public class StateFiniteAutomata : State, IComparable
    {
        private Node node=null;
        public StateFiniteAutomata(string n)
            : base(n)
        {
        }
        public StateFiniteAutomata(Node nod)
            : base(nod.Name)
        {
            node = nod;
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
        public Node AssociatedNode
        {
            get { return node; }
        }

    }
   
    
}
