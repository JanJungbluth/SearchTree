using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchTree
{


    public class StateSpace
    {
        private int STATE_SPACE_DIMENSION; // Get the Dimension of the statespace
        private int[] STATES_SIZE_VEC; // Get for each Dimension the number of possible states
        private int[] STATE_VEC; // This ist the state vector

        // Constructor teste
        public StateSpace(int Dimension, int[] StateSizeVec)
        {
            // Get the dimension
            this.STATE_SPACE_DIMENSION = Dimension;
            // Get the number of states for each dimension
            this.STATES_SIZE_VEC = StateSizeVec;
            // Create the state vector
            this.STATE_VEC = new int[this.STATE_SPACE_DIMENSION];
        }
        public int DimensionSize
        {
            get { return this.STATE_SPACE_DIMENSION; }
        }
        public int[] StatesSizeVec
        {
            get { return this.STATES_SIZE_VEC; }
        }
        public int StateSize(int Dimension)
        {
            return this.STATES_SIZE_VEC[Dimension];
        }
        public int[] StateVec
        {
            get { return this.STATE_VEC; }
            set { this.STATE_VEC = value; }
        }
        public void setStateValue(int Dimension, int StateValue)
        {
            this.STATE_VEC[Dimension] = StateValue;
        }
        public bool isTargetState(StateSpace CurState, StateSpace Target)
        {
            // This function checks if the current State is the target state
            bool IsTarget = true; // If true it is the target state

            int i = 0;// Indexer
            // Go thru alls dimensions and check current against target statevalue 
            foreach (int CurValue in CurState.StateVec)
            {
                if (CurValue != Target.StateVec[i++])
                {
                    // If one is current state value is wrong
                    //its not the target state
                    IsTarget = false;
                    break;
                }

            }
            return IsTarget;
        }
        public String printState()
        {
            StringBuilder MySB = new StringBuilder("StateVec: [");

            foreach (int Value in this.STATE_VEC)
                MySB.Append(String.Format(" {0}", Value));

            MySB.Append(" ]" + System.Environment.NewLine);

            return MySB.ToString();

        }
        public String printStateSpace()
        {
            int StateSpaceSize = 1;
            StringBuilder MySB = new StringBuilder("StateSpace: [ ");

            for (int i = 0; i < this.STATE_SPACE_DIMENSION; i++)
            {
                MySB.Append(String.Format(" {0} ", this.STATES_SIZE_VEC[i]));
                StateSpaceSize *= this.STATES_SIZE_VEC[i];
            }

            MySB.Append(String.Format(" ] possible states: {0} ", StateSpaceSize) + Environment.NewLine);

            return MySB.ToString();

        }
        public String printEnumState()
        {
            
            // #TODO USE THE ENUMERATION !!!!
            String[][] Dim = new String[14][]{ new String[]{ "EMPTY", "HAS_GRIPPER", "HAS_SCRWEDRIVER", "HAS_OBJECT" },
                                new String[]{"AT_HOME_POS", "AT_LEFT_HAND_POS", "AT_RIGHT_HAND_POS", "AT_GRIPPER_MAGAZINE_POS", "AT_SCREWDRIVER_MAGAZIN_POS",
                                 "AT_SCREW_POS", "AT_FULLY_LOOSE_SCREW_POS","AT_TOOL_MAGAZINE_POS", "AT_BOX_MAGAZIN_POS"},
                                new String[] { "DONT_KNOW_TASK", "KNOW_TASK" },
                                new String[] { "DONT_KNOW_SCREW", "KNOW_SCREW" },
                                new String[] { "DONT_KNOW_TOOL", "KNOW_TOOL" },
                               new String[] { "DONT_KNOW_ASSISTANCE", "KNOW_ASSISTANCE" },
                                new String[] { "EMPTY", "HAS_OBJECT" },
                               new String[] { "AT_BODY_POS", "AT_LEFT_HAND_POS", "AT_RIGHT_HAND_POS", "AT_SCREW_POS", "AT_FULLY_LOOSE_SCREW_POS",
                                  "AT_TOOL_MAGAZINE_POS", "AT_BOX_MAGAZINE_POS" },
                               new String[] { "EMPTY", "HAS_OBJECT" },
                               new String[] { "AT_BODY_POS", "AT_LEFT_HAND_POS", "AT_RIGHT_HAND_POS", "AT_SCREW_POS", "AT_FULLY_LOOSE_SCREW_POS",
                                  "AT_TOOL_MAGAZINE_POS", "AT_BOX_MAGAZINE_POS" },
                                new String[] { "TIGHTENED", "LOOSE", "FULLY_LOOSE", "REMOVED" },
                                new String[] { "AT_SCREW_POS", "AT_FULLY_LOOSE_SCREW_POS", "AT_GRIPPER", "AT_HUMAN_LEFT_HAND", "AT_HUMAN_RIGHT_HAND", "AT_BOX_MAGAZINE_POS" },
                                new String[] { "AT_TOOL_MAGAZINE_POS", "AT_LEFT_HAND_POS", "AT_RIGHT_HAND_POS", "AT_GRIPPER", "AT_HUMAN_LEFT_HAND", "AT_HUMAN_RIGHT_HAND" },
                                new String[] { "AT_BOX_MAGAZINE_POS", "AT_LEFT_HAND_POS", "AT_RIGHT_HAND_POS", "AT_GRIPPER", "AT_HUMAN_LEFT_HAND","AT_HUMAN_RIGHT_HAND" } };

            StringBuilder MySB = new StringBuilder("StateVec:" + Environment.NewLine);
            int i = 0;
            foreach (int Value in this.STATE_VEC)
                    MySB.AppendLine(Dim[i++][Value]);            

            return MySB.ToString();
        }

    }
}
