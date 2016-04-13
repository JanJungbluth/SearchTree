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

            StateSpace StartState = Helper.createStartState();
            StateSpace TargetState = Helper.createTargetState();
            this.textBox1.AppendText(StartState.printState());
            this.textBox1.AppendText(TargetState.printState());
            this.Refresh();
            this.textBox1.AppendText("Full Action List" + Environment.NewLine);
            List<Action> MyRobotActionList = Helper.createRobotActionSet();
            foreach(Action CurAction in MyRobotActionList)
            {
                this.textBox1.AppendText(CurAction.Name + Environment.NewLine);
            }
            this.Refresh();
           
            List<Action> MyRobotFeasibleActionList = Helper.PossibleActionSet(MyRobotActionList, StartState);
            this.textBox1.AppendText("Constarined Action List" + Environment.NewLine);
            foreach (Action CurAction in MyRobotFeasibleActionList)
            {
                this.textBox1.AppendText(CurAction.Name + Environment.NewLine);
            }
            this.Refresh();


            
        }
    }
}


