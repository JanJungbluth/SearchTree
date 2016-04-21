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
        private int DepthCounter = 1;
        //create a new form for the viewer
        System.Windows.Forms.Form Graphform = new System.Windows.Forms.Form();
        //create a viewer object 
        Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
        //create a graph object 
        Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
        //create a list with all states which have to expand in the next step
        private List<StateSpace> StatesWaitingToExpand = new List<StateSpace>();
        //create a list with all possible actions
        private List<Action> FullActionSet = new List<Action>();
        //create a list for all applicable actions in a given state
        private List<Action> ApplicableActionSet = new List<Action>();

        //Test Test 1 2 3 4


        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Show a new iteration step in the textbox
            this.textBox1.AppendText(Environment.NewLine
                + String.Format("-------- Stepnumber {0} ------- ",this.DepthCounter++ )
                + Environment.NewLine);
            List<StateSpace> Temp = new List<StateSpace>(this.StatesWaitingToExpand);

            if( this.DepthCounter % 5 == 0)
            {
                List<StateSpace> SadStateList = Helper.KillSadStates(this.StatesWaitingToExpand, this.DepthCounter);
                foreach(StateSpace CurSadState in SadStateList)
                {
                    this.StatesWaitingToExpand.Remove(CurSadState);
                    Microsoft.Msagl.Drawing.Node CurSadNode = graph.FindNode(CurSadState.printEnumState());
                    graph.RemoveNode(CurSadNode);
                }


                // Show that the sad states was killed in the textbox
                this.textBox1.AppendText(Environment.NewLine
                    + String.Format("Sad states was killed at step {0} ", this.DepthCounter++)
                    + Environment.NewLine);
                
            }
            
            // Now we have to expand each state in the StatesWaitToExpand List and expand them
            foreach (StateSpace CurState in Temp)
            {
                // create the new applicable action list ...
                this.ApplicableActionSet = Helper.PossibleActionSet(this.FullActionSet, CurState);
                // ..and print them to the textbox
                this.textBox1.AppendText(Environment.NewLine + "Applicable Action List" + Environment.NewLine);
                foreach (Action CurAction in this.ApplicableActionSet)
                    this.textBox1.AppendText(CurAction.Name + Environment.NewLine);

                foreach (Action CurAction in this.ApplicableActionSet)
                {
                    //Create the new state
                    StateSpace NewState = CurAction.ExecuteAction(CurState);
                    // save the new state in the StatesWaitToExpand list
                    this.StatesWaitingToExpand.Add(new StateSpace(NewState));
                    //Get the CurState Node
                    Microsoft.Msagl.Drawing.Node CurNode = graph.FindNode(CurState.printEnumState());
                    // Add a new node to the graph
                    Microsoft.Msagl.Drawing.Node NewNode = new Microsoft.Msagl.Drawing.Node(NewState.printEnumState());
                    this.graph.AddNode(NewNode);
                    // Add a edge ( and the nodes ) to the graph
                    this.graph.AddEdge(CurNode.Id, CurAction.Name, NewNode.Id);
                    //Remove the old state
                    this.StatesWaitingToExpand.Remove(CurState);
                }
                
            }

            // freeze the form logic
            Graphform.SuspendLayout();
            this.tabControl1.SuspendLayout();
            //bind the graph to the viewer 
            viewer.Graph = graph;
            // add the Form control to the tabcontroler
            this.tabControl1.TabPages[0].Controls.Add(viewer);
            // doch the viewer with max size to the tabpage
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            // de-freeze the form logic
            Graphform.ResumeLayout();
            this.tabControl1.TabPages[0].ResumeLayout();
            // refresh all visual things
            this.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // The first step is to generate the target and start state
            StateSpace StartState = Helper.createStartState();
            StateSpace TargetState = Helper.createTargetState();
            // We print them both to the textbox
            this.textBox1.AppendText("StartState: " + StartState.printEnumState()+Environment.NewLine);
            this.textBox1.AppendText("TargetState: " + TargetState.printEnumState()+Environment.NewLine);
            
            // Then we create the all robot actions ...
            this.FullActionSet = Helper.createRobotActionSet();
            // and then we print them in the textbox
            this.textBox1.AppendText("Full Action List" + Environment.NewLine);
            foreach (Action CurAction in FullActionSet)
                this.textBox1.AppendText(CurAction.Name + Environment.NewLine);
            
            // We create the applicable action list
            this.ApplicableActionSet = Helper.PossibleActionSet(FullActionSet, StartState);
            // ..and print them to the textbox
            this.textBox1.AppendText(Environment.NewLine + "Applicable Action List" + Environment.NewLine);
            foreach (Action CurAction in this.ApplicableActionSet)
                this.textBox1.AppendText(CurAction.Name + Environment.NewLine);

            //create the root node for the graph
            Microsoft.Msagl.Drawing.Node StartNode = new Microsoft.Msagl.Drawing.Node(StartState.printEnumState());
            
            this.graph.AddNode(StartNode);
            // Next we execute the applicable actions to the startstate and collect the new states in
            //the StatesWaitToExpand list
            foreach (Action CurAction in this.ApplicableActionSet)
            {
                //Create the new state
                StateSpace CurState = CurAction.ExecuteAction(StartState);
                CurState.Depth = this.DepthCounter;
                // save the new state in the StatesWaitToExpand list
                this.StatesWaitingToExpand.Add(new StateSpace (CurState));
                // Add a new node to the graph
                Microsoft.Msagl.Drawing.Node CurNode = new Microsoft.Msagl.Drawing.Node(CurState.printEnumState());
                this.graph.AddNode(CurNode);
                // Add a edge ( and the nodes ) to the graph
                this.graph.AddEdge(StartNode.Id, CurAction.Name, CurNode.Id);

            }

            //Check if there is a new state in the stateswaittoexpandd list which meets the target state
            List<StateSpace> Targets = Helper.IsTargetState(this.StatesWaitingToExpand, TargetState);

            if(Targets.Count < 0)
                foreach(StateSpace CurTarget in Targets)
                {
                    Microsoft.Msagl.Drawing.Node c = graph.FindNode(CurTarget.printEnumState());
                    c.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;
                }
               
            // freeze the form logic
            Graphform.SuspendLayout();
            this.tabControl1.SuspendLayout();
            //bind the graph to the viewer 
            viewer.Graph = graph;
            // add the Form control to the tabcontroler
            this.tabControl1.TabPages[0].Controls.Add(viewer);
            // doch the viewer with max size to the tabpage
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            // de-freeze the form logic
            Graphform.ResumeLayout();
            this.tabControl1.TabPages[0].ResumeLayout();
            // refresh all visual things
            this.Refresh();
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}


