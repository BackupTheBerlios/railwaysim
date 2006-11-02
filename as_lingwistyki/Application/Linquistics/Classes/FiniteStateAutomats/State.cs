using System;
using System.Collections.Generic;
using System.Text;

namespace Linquistics
{
    public class State : IComparable
    {
        private String name;
        public State(string n)
        {
            name = n; 
        }
        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public int CompareTo(object obj)
        {
            if (obj is State)
            {
                State temp = (State)obj;

                return name.CompareTo(temp.name);
            }

            throw new ArgumentException("object is not a State");
        }

    }
}
