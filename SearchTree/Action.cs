using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchTree
{
    #region "Enumeration"

    public enum ActionTagsHumans
    {
        BE_IDLE = 0, TAKE_TOOL, HOLD_PART, LOOSE_SCREW, LOOSE_SCREW_FULLY, REMOVE_SCREW, LOOSE_AND_REMOVE_SCREW,
        RETURN_TOOL, RELEASE_PART, PLACE_SCREW_IN_BOX
    };

    public enum ActionTagsRobot
    {
        BE_IDLE = 0, HAND_TOOL, CHANGE_EFFECTOR, HOLD_PART, LOOSE_SCREW, LOOSE_SCREW_FULLY, REMOVE_SCREW, LOOSE_AND_REMOVE_SCREW,
        HAND_SCREW_BOX, RETURN_TOOL, RELEASE_PART, PLACE_SCREW_IN_BOX, RETURN_SCREW_BOX
    };


    #endregion
    class Action
    {
        private String NAME;
        private int[] PRE_DIMENSIONS; // Dimensions with preconditions
        private int[] PRECONDITIONS; // Preconditions for that dimension
        private int[] TRA_DIMENSIONS; // Dimensions with transmisions
        private int[] TRANSITIONS; // transition of the coresponding dimension

        public Action(string Name, int[] PreDimension, int[] Preconditions, int[] TraDimensions, int[] Transitions)
        {
            
            this.NAME = Name;
            this.PRE_DIMENSIONS = PreDimension;
            this.PRECONDITIONS = Preconditions;
            this.TRA_DIMENSIONS = TraDimensions;
            this.TRANSITIONS = Transitions;            
        }
        
        public bool CheckPrecondition(StateSpace MyState)
        {
            //there are no preconditions if the precondition dimension is zero
            if(this.PRE_DIMENSIONS.Length == 0)
            {
                return true;
            }
            // if it is not zero there are constrained dimensions
            bool Check = true;
            int i = 0;
            // go thru each constrained dimension and check the Precondition
            foreach( int PreDim in this.PRE_DIMENSIONS)
            {
                Check = false;
                if(MyState.StateVec[PreDim] == this.PRECONDITIONS[i++])
                {
                    Check = true;
                }
                
            }

            return Check;
        }
        public StateSpace ExecuteAction(StateSpace MyState)
        {
            // A action is executed by this function an it will change the state
           // bool breaktotop = false;
            // Go thru all dimension of the state space
            //for(int i = 0; i < MyState.DimensionSize; i++)
           // {
            //    // Get one dimension form the transitionsspace and check it against the statespace
            //    foreach (int[,] Pair in this.TRANSITIONS)
            //    {
            //        // Get the Rang (number of columns) of the Matrix
            //        int Rang = Pair.Rank;
            //        // Get the number of elements in the Matrix
            //        int Laenge = Pair.Length;
            //        // Calc the number of rows in the Matrix
            //        int Rows = Laenge / Rang;

            //        for (int j = 0; j < Rows; j++)
            //        {
            //            // if there is a transition for the state...
            //            if ( MyState.StateVec[i] == Pair[j,0])
            //            {
            //                // .. then change the state and go to the next dimension
            //                MyState.StateVec[i] = Pair[j, 1];
            //                breaktotop = true;
            //                break;
            //            }
                        
            //        }
            //        if (breaktotop)
            //        {   // to jump to the next dimension break this loop
            //            breaktotop = false;
            //            break;
            //        }
                                           
            //    }
            //}
            
            return MyState;
        }
        public String printAction()
        {
            //StringBuilder MySB = new StringBuilder(Environment.NewLine + "Action: " + this.NAME + Environment.NewLine);

            //// Print the Preconditions
            //String temp = "( ";
            //MySB.AppendLine("Preconditions: ");
            //foreach (int[] Row in this.PRECONDITIONS)
            //{
            //    foreach(int Value in Row)
            //    {
            //        temp += Value.ToString() + " ";
            //    }
            //    MySB.AppendLine(temp + " )" );
            //    temp = "(";
            //}
            //MySB.Append(Environment.NewLine);
            //// Print the Transmitions
            //MySB.AppendLine("Transition: ");
            //foreach (int[,] Pair in this.TRANSITIONS)
            //{
            //    // Get the Rang (number of columns) of the Matrix
            //    int Rang = Pair.Rank;
            //    // Get the number of elements in the Matrix
            //    int Laenge = Pair.Length;
            //    // Calc the number of rows in the Matrix
            //    int Rows = Laenge / Rang;

            //    for ( int i = 0; i < Rows; i++)
            //    {
            //        MySB.Append(String.Format("({0},{1}) ", Pair.GetValue(new int[] { i, 0 }), Pair.GetValue(new int[] { i, 1 })));
            //    }
            //    MySB.Append(Environment.NewLine);

            //}

            //MySB.Append(Environment.NewLine);                  

            return "";//MySB.ToString();
        }

    }
}
