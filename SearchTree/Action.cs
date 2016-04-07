using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchTree
{
    class Action
    {
        private String NAME;
        private int DIMENSION;
        private int[][] PRECONDITIONS;
        private int[][,] TRANSITIONS;

        public Action(string Name, int Dimension, int[][] Preconditions, int[][,] Transitions)
        {
            this.NAME = Name;
            this.DIMENSION = Dimension;
            this.PRECONDITIONS = Preconditions;
            this.TRANSITIONS = Transitions;            
        }
        
        public bool CheckPrecondition(StateSpace MyState)
        {
            // first check thtat the StateVector and the Transitions has the same Dimension
            if(MyState.DimensionSize != this.DIMENSION)
            {
                throw new Exception("State Dimension != Action Dimension");
            }
            // dont be a pessimist about the preconditions
            bool Check = true;
            // check every StateDimension if at least on precondition is matched
            for( int i = 0; i < this.DIMENSION && Check; i++)
            {
                // take the a Precondition vector for a dimension
                int[] Value = this.PRECONDITIONS[i];
                //reset the check
                Check = false;
                // got thru this vector and check if no precondition is statisfied
                for (int j = 0; j < Value.Length; j++)
                {
                    // if one precondition matches the statevector than go further
                    if (MyState.StateVec[i] == Value[j])
                    {
                        Check = true;
                        break;
                    }                        
                }              
            }
            return Check;
        }
        public StateSpace ExecuteAction(StateSpace MyState)
        {
            bool breaktotop = false;
            for(int i = 0; i < MyState.DimensionSize; i++)
            {
                // Get the first dimension from the StateSpace
                
                foreach (int[,] Pair in this.TRANSITIONS)
                {
                    // Get the Rang (number of columns) of the Matrix
                    int Rang = Pair.Rank;
                    // Get the number of elements in the Matrix
                    int Laenge = Pair.Length;
                    // Calc the number of rows in the Matrix
                    int Rows = Laenge / Rang;

                    for (int j = 0; j < Rows; j++)
                    {
                        // if there is a transition ...
                        if ( MyState.StateVec[i] == Pair[j,0])
                        {
                            // .. then change the state and go to the next dimension
                            MyState.StateVec[i] = Pair[j, 1];
                            breaktotop = true;
                            break;
                        }
                        
                    }
                    if (breaktotop)
                    {
                        breaktotop = false;
                        break;
                    }
                                           
                }
            }
            
            return MyState;
        }
        public String printAction()
        {
            StringBuilder MySB = new StringBuilder(Environment.NewLine + "Action: " + this.NAME + Environment.NewLine);

            // Print the Preconditions
            String temp = "( ";
            MySB.AppendLine("Preconditions: ");
            foreach (int[] Row in this.PRECONDITIONS)
            {
                foreach(int Value in Row)
                {
                    temp += Value.ToString() + " ";
                }
                MySB.AppendLine(temp + " )" );
                temp = "(";
            }
            MySB.Append(Environment.NewLine);
            // Print the Transmitions
            MySB.AppendLine("Transmition: ");
            foreach (int[,] Pair in this.TRANSITIONS)
            {
                // Get the Rang (number of columns) of the Matrix
                int Rang = Pair.Rank;
                // Get the number of elements in the Matrix
                int Laenge = Pair.Length;
                // Calc the number of rows in the Matrix
                int Rows = Laenge / Rang;

                for ( int i = 0; i < Rows; i++)
                {
                    MySB.Append(String.Format("({0},{1}) ", Pair.GetValue(new int[] { i, 0 }), Pair.GetValue(new int[] { i, 1 })));
                }
                MySB.Append(Environment.NewLine);

            }

            MySB.Append(Environment.NewLine);                  

            return MySB.ToString();
        }

    }
}
