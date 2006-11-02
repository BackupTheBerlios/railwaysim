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
 } 
}
