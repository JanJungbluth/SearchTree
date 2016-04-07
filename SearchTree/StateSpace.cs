using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchTree
{
    class StateSpace
    {
        private int STATE_SPACE_DIMENSION; // Get the Dimension of the statespace
        private int[] STATES_SIZE_VEC; // Get for each Dimension the number of possible states
        private int[] STATE_VEC; // This ist the state vector

        // Constructor
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
            set { this.STATE_VEC = value;}
        }
        public void setStateValue(int Dimension, int StateValue)
        {
            this.STATE_VEC[Dimension] = StateValue;
        }
        public String printState()
        {
            StringBuilder MySB = new StringBuilder("StateVec: [ ");

            foreach (int Value in this.STATE_VEC)
                MySB.Append(String.Format(" {0} ", Value));

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
                
            MySB.Append(String.Format(" ] possible states: {0} " , StateSpaceSize) + Environment.NewLine);

            return MySB.ToString();

        }
    }
}
