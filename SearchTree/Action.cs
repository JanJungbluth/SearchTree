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
    public class Action
    {
        private String NAME;
        private int HAPPINESS;
        private int[] PRE_DIMENSIONS; // Dimensions with preconditions
        private int[] PRECONDITIONS; // Preconditions for that dimension
        private int[] TRA_DIMENSIONS; // Dimensions with transmisions
        private int[] TRANSITIONS; // transition of the coresponding dimension

        public Action(string Name, int Happiness, int[] PreDimension, int[] Preconditions, int[] TraDimensions, int[] Transitions)
        {
            
            this.NAME = Name;
            this.PRE_DIMENSIONS = PreDimension;
            this.PRECONDITIONS = Preconditions;
            this.TRA_DIMENSIONS = TraDimensions;
            this.TRANSITIONS = Transitions;
            this.HAPPINESS = Happiness; // #TODO use the costs!!!   
        }
        public int Happiness
        {
            get { return this.HAPPINESS; }
            set { this.HAPPINESS = value; }
        }
        public bool CheckPrecondition(StateSpace MyState)
        {
            //there are no preconditions if the precondition dimension is zero
            if(this.PRE_DIMENSIONS.Length == 0)
            {
                return true;
            }
            // if it is not zero there are constrained dimensions
            bool Check = false;
            int i = 0;
            // go thru each constrained dimension and check the Precondition
            foreach( int PreDim in this.PRE_DIMENSIONS)
            {

                
                if(MyState.StateVec[PreDim] == this.PRECONDITIONS[i++])
                {
                    Check = true;
                }
                else
                {
                    Check = false;
                    break;
                }                
            }
            return Check;
        }
        public StateSpace ExecuteAction(StateSpace MyState)
        {
            // create a new state with the depth of the parent plus 1 
            StateSpace NewState = new StateSpace(MyState.Depth + 1, MyState.getId, MyState.DimensionSize, MyState.StatesSizeVec, MyState.StateVec);
            // add the happiness gained by this action to the state
            NewState.Happiness = MyState.Happiness + this.HAPPINESS;
            // An action is executed by this function an it will change the state
            int i = 0;
            //Go thru all transmission dimension 
            foreach(int TraDim in this.TRA_DIMENSIONS)
            {
                // for each ellement in Tra_Dimension there is one transition
                NewState.setStateValue(TraDim, this.TRANSITIONS[i++]);                                
            }
            
            return NewState;
        }
        public String Name
        {
            get { return this.NAME; }
            set { this.NAME = value; }
        }
        public String printAction()
        {
            StringBuilder MySB = new StringBuilder(Environment.NewLine + "Action: " + this.NAME + Environment.NewLine);

            // Print the Preconditions
            MySB.Append("Preconditions: [");

            int i = 0;
            foreach (int PreDim in this.PRE_DIMENSIONS)
            {
                MySB.Append(String.Format(" ( {0} {1} )", PreDim, this.PRECONDITIONS[i++]));
            }               
            MySB.AppendLine(" ]");
            
            MySB.Append(Environment.NewLine);

            // Print the Transmitions
            MySB.AppendLine("Transition: [");
            i = 0;
            foreach (int TraDim in this.TRA_DIMENSIONS)
            {
                MySB.Append(String.Format(" ( {0} {1} )", TraDim, this.TRANSITIONS[i++]));
            }
            MySB.AppendLine(" ]");

            return MySB.ToString();
        }

    }
}
