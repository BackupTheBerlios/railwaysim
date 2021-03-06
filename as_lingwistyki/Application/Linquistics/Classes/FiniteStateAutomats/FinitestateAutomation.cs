using System;
using System.Collections.Generic;
using System.Text;

namespace Linquistics
{

   ///  <summary>
   ///  </summary>
   public class FinitestateAutomation 
   {

     /// Attributes

     /// Attribute states
     private List<StateFiniteAutomata> states = new List<StateFiniteAutomata>();

     /// Attribute innerAlphabet
     private Alphabet innerAlphabet = new Alphabet();
       public Alphabet InnerAlphabet
       {
           get { return innerAlphabet; }
       }

     /// Attribute startState
     private StateFiniteAutomata startState = null;

     /// Attribute acceptStates
     private SortedDictionary<String,StateFiniteAutomata> acceptStates = new SortedDictionary<string,StateFiniteAutomata>();

     /// Attribute name
     private String name = "";

     /// Attribute operationFunction
     private OperationFunction operationFunction = new OperationFunction();


     
     /// Association End operationFunction
     private StateFiniteAutomata currentState = null;
     public StateFiniteAutomata CurrentState
     {
         get { return currentState; }
     }
     public OperationFunction OpFunction
     {
         get
         {
             return operationFunction;
         }
     }
       public void AddStartState(StateFiniteAutomata s)
       {
           this.startState = s;
           this.AddState(s);
       }
       public void AddAcceptState(StateFiniteAutomata s)
       {
           this.acceptStates.Add(s.Name,s);
           this.AddState(s);
       }
       public void AddState(StateFiniteAutomata s)
       {
           states.Add(s);
       }
       public void ActivateAutomate()
       {
           currentState = startState;
       }
       public StateFiniteAutomata NextOperation(char c)
       {
           StateFiniteAutomata nextSt = operationFunction.GetNextState(currentState,c);
           currentState = nextSt;
           return nextSt;
       }
       public bool IsInAcceptedState()
       {
          if(currentState!=null&&acceptStates.ContainsKey(currentState.Name))
           return true;
       return false;
       }
       public bool InnerSimulation(string nap)
       {
           this.ActivateAutomate();
           StateFiniteAutomata nextSt;
           StringBuilder sBuild=new StringBuilder(nap);
           char nextChar = 'a';
           while (sBuild.Length > 0)
           {
               nextChar = sBuild[0];
               sBuild.Remove(0, 1);
               nextSt = operationFunction.GetNextState(currentState, nextChar);
               if (nextSt == null) return false;
               currentState = nextSt;
           }
           if (this.IsInAcceptedState()) return true;
           return false;

       }
 } 
}
