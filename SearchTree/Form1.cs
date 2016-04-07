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
            StateSpace State1 = new StateSpace(3, new int[] { 3, 5, 4 });
            State1.StateVec = new int[]{ 1, 3, 2};
            this.textBox1.AppendText(State1.printState());
            this.textBox1.AppendText(State1.printStateSpace());

            int[][,] Transitions1 = new int[3][,]{   new int[,] { {1,3}, {2,3} }, new int[,] { {0,2}, {3,2}, {5,4} }, new int[,] { {1,2}, {2,3}, {3,4} }  };
            int[][] Preconditions1 = new int[3][] { new int[] { 0, 1, 2, }, new int[] { 0, 3, 5 }, new int[] { 1, 2, 3 } };
            Action Action1 = new Action("Take", 3, Preconditions1,Transitions1);
            this.textBox1.AppendText(Action1.printAction());
            
            bool Worked = Action1.CheckPrecondition(State1);


            this.Refresh();
        }
    }
}


