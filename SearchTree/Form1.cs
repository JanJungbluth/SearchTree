using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;


namespace SearchTree
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //create a form 
            //System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            //create a viewer object 
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            //create a graph object 
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            //create the graph content 
            graph.AddEdge("A", "B");
            graph.AddEdge("B", "C");
            graph.AddEdge("A", "C").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
            graph.FindNode("A").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Magenta;
            graph.FindNode("B").Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
            Microsoft.Msagl.Drawing.Node c = graph.FindNode("C");
            c.Attr.FillColor = Microsoft.Msagl.Drawing.Color.PaleGreen;
            c.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;
            //bind the graph to the viewer 
            viewer.Graph = graph;
            //associate the viewer with the form 
            this.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Controls.Add(viewer);
            this.ResumeLayout();
            //show the form 
            this.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            const int StateSpaceDim = 14;

            StateSpace StartState = new StateSpace(StateSpaceDim, new int[] { 3, 9, 2, 2, 2, 2, 2, 5, 2, 5, 4, 6, 6, 6 });
            StartState.StateVec = new int[]{ (int)TagsRobot.EMPTY, (int)TagsRobot.AT_HOME_POS,
                                             (int)TagsHuman.KNOW_TASK,(int)TagsHuman.KNOW_SCREW,(int)TagsHuman.KNOW_TOOL,(int)TagsHuman.KNOW_ASSISTANCE,
                                             (int)TagsHuman.EMPTY,(int)TagsHuman.AT_BODY_POS,(int)TagsHuman.EMPTY,(int)TagsHuman.AT_BODY_POS,
                                             (int)TagsScrew.TIGHTENED, (int)TagsScrew.AT_SCREW_POS, (int)TagsTool.AT_TOOL_MAGAZINE_POS,  (int)TagsBox.AT_BOX_MAGAZINE_POS };

            StateSpace TargetState = new StateSpace(StateSpaceDim, new int[] { 3, 9, 2, 2, 2, 2, 2, 5, 2, 5, 4, 6, 6, 6 });
            TargetState.StateVec = new int[]{(int)TagsRobot.EMPTY, (int)TagsRobot.AT_HOME_POS,
                                             (int)TagsHuman.KNOW_TASK,(int)TagsHuman.KNOW_SCREW,(int)TagsHuman.KNOW_TOOL,(int)TagsHuman.KNOW_ASSISTANCE,
                                             (int)TagsHuman.EMPTY,(int)TagsHuman.AT_BODY_POS,(int)TagsHuman.EMPTY,(int)TagsHuman.AT_BODY_POS,
                                             (int)TagsScrew.REMOVED, (int)TagsScrew.AT_BOX_MAGAZINE_POS, (int)TagsTool.AT_TOOL_MAGAZINE_POS,  (int)TagsBox.AT_BOX_MAGAZINE_POS };

            this.textBox1.AppendText(StartState.printState());
            this.textBox1.AppendText(TargetState.printState());
            this.Refresh();



            Action GOTO_HOME_POS = new Action("GOTO_HOME_POS", new int[] { }, new int[] { }, new int[] { 2 }, new int[] { 0 });
            bool test = GOTO_HOME_POS.CheckPrecondition(StartState);
            //int[][,] Transitions1 = new int[StateSpaceDim][,]{   new int[,] { {1,3}, {2,3} }, new int[,] { {0,2}, {3,2}, {5,4} }, new int[,] { {1,2}, {2,3}, {3,4} }  };

            //Action Action1 = new Action("Take", 3, Preconditions1,Transitions1);
            //this.textBox1.AppendText(Action1.printAction());

            // bool Worked = Action1.CheckPrecondition(StartState);
            // if (Worked)
            //     Action1.ExecuteAction(StartState);
            // this.textBox1.AppendText(StartState.printState());

            //// this.textBox1.AppendText(TagsRobot.EMPTY.ToString()) ;
            // this.textBox1.AppendText(TagsRobot.AT_HOME_POS.ToString());

            // //Creat it worked


            this.Refresh();
        }
    }
}


