using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Linquistics
{
    public class OperationFunction 
    {

     /// Attributes

     /// Attribute functionData
     private SortedDictionary<StateCharPair,List<StateFiniteAutomata>> functionData = new SortedDictionary<StateCharPair,List<StateFiniteAutomata>>();

        public void AddElement(StateCharPair k, StateFiniteAutomata v)
        {
            List<StateFiniteAutomata> s = null;
            if (functionData.Count > 0)
            {

                if(functionData.ContainsKey(k) )
                {
                    s = functionData[k];
                    s.Add(v);
                }
                else
                {
                    s = new List<StateFiniteAutomata>();
                    s.Add(v);
                    functionData.Add(k, s);
                }
            }
            else
            {
                 s = new List<StateFiniteAutomata>();
                 s.Add(v);
                 functionData.Add(k, s);
                
            }
        }
        public bool DeleteRow(StateCharPair sp, StateFiniteAutomata s)
        {
            if (functionData.Count > 0)
            {
                List<StateFiniteAutomata> nStates = functionData[sp];
                if (nStates != null) { return nStates.Remove(s); }
            }
            return false;
        }
        public bool SetDirectState(StateCharPair sp, StateFiniteAutomata s)
        {
            if (functionData.Count > 0)
            {
                List<StateFiniteAutomata> nStates = null;
                if(functionData.ContainsKey(sp))
                {
                    nStates=functionData[sp];
                
                    if (!nStates.Contains(s)) { nStates.Add(s); return true; }
                }
                else
                {
                    nStates = new List<StateFiniteAutomata>();
                    nStates.Add(s);
                    functionData.Add(sp, nStates);
                    return true;
                }
                

            }
            return false;
        }
        public void WriteDataToListView(ListView lv)
        {
            IEnumerator<StateCharPair> enumSC=functionData.Keys.GetEnumerator();
            IEnumerator<List<StateFiniteAutomata>> enumSt = functionData.Values.GetEnumerator();
            int i=0;
            while (enumSC.MoveNext() && enumSt.MoveNext())
            {

                
                List<StateFiniteAutomata> ls=enumSt.Current;
                StateCharPair fs = enumSC.Current;
                for(i=0;i<ls.Count;i++)
                {
                    string[] tab = new string[3];
                    tab[0] = fs.StateName;
                    tab[1] = fs.Character.ToString();

                    tab[2] = ls[i].Name;
                    ListViewItem item = new ListViewItem(tab);
                    lv.Items.Add(item);
                }
            }
        }
    }
}
