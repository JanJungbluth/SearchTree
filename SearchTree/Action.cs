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
        private int[][,] TRANSITIONS;

        public Action(string Name, int Dimension, int[][,] Transitions)
        {
            this.NAME = Name;
            this.DIMENSION = Dimension;
            TRANSITIONS = Transitions;            
        }
        public bool CheckPrecondition(StateSpace MyState)
        {
            bool Check = false;

            foreach(int[,] Pair in this.TRANSITIONS)
            {
                // #TODO
            }
            return Check;
        }
        public String printAction()
        {
            

            StringBuilder MySB = new StringBuilder(" Transitions of " + this.NAME + Environment.NewLine);
         
            foreach(int[,] Pair in this.TRANSITIONS)
            {
                int Rang = Pair.Rank;
                int Laenge = Pair.Length;
                int Rows = Laenge / Rang;

                //MySB.Append(" Transitions of " + this.NAME + Environment.NewLine);

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
