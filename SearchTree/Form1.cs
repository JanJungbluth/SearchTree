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
        #region Global Definitions
        //this is the number of expandsteps
        private int DepthCounter = 0;
        private StateSpace Target;
        private StateSpace TargetFound;
        //create a new form for the viewer
        System.Windows.Forms.Form Graphform = new System.Windows.Forms.Form();
        //create a viewer object 
        Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
        //create a graph object 
        Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
        //create a list with all states 
        private List<StateSpace> AllStates = new List<StateSpace>();
        //create a list with all states which have to expand in the next step
        private List<StateSpace> StatesWaitingToExpand = new List<StateSpace>();
        //create a list with all possible actions
        private List<Action> FullActionSet = new List<Action>();
        //create a list for all applicable actions in a given state
        private List<Action> ApplicableActionSet = new List<Action>();
        
        #endregion

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            // Show a new iteration step in the textbox1
            this.textBox1.AppendText(Environment.NewLine
                + String.Format("-------- Stepnumber {0} ------- ",this.DepthCounter++ )
                + Environment.NewLine);
        
            //copy the StateWaitToExpand list to manipulate the list not in the foreach loop
            List<StateSpace> Temp = new List<StateSpace>(this.StatesWaitingToExpand);
            // Now we have to expand each state in the StatesWaitToExpand List and expand them
            foreach (StateSpace CurState in Temp)
            {
                // create the new applicable action list ...
                this.ApplicableActionSet = Helper.PossibleActionSet(this.FullActionSet, CurState);

                foreach (Action CurAction in this.ApplicableActionSet)
                {
                    //Create the new state
                    StateSpace NewState = CurAction.ExecuteAction(CurState);
                    // check if the action returns a state that is not in the Allstates list 
                    if (Helper.IsTargetState(this.AllStates,NewState).Count == 0)
                    {
                        // when there is not that state in the list, then create a new state and add them to the list
                        // give out the new state
                        // save the new state in the StatesWaitToExpand list
                        this.StatesWaitingToExpand.Add(new StateSpace(NewState));
                        // save the new state in the AllStates list
                        this.AllStates.Add(new StateSpace(CurState));
                        //Get the CurState Node
                        Microsoft.Msagl.Drawing.Node CurNode = graph.FindNode(CurState.printEnumState());
                        // Add a new node to the graph
                        Microsoft.Msagl.Drawing.Node NewNode = new Microsoft.Msagl.Drawing.Node(NewState.printEnumState());
                        //NewNode.Attr.FillColor = new Microsoft.Msagl.Drawing.Color((byte)(NewState.Depth*10), (byte)(NewState.Depth * 10), (byte)(NewState.Depth * 10));
                        this.graph.AddNode(NewNode);
                        // Add a edge to the graph
                        this.graph.AddEdge(CurNode.Id, CurAction.Name, NewNode.Id);
                        //Remove the curent state from the StatewaitToExpand list
                        this.StatesWaitingToExpand.Remove(CurState);
                    }
                    // Check if the arc between CurState and NewState exists in the graph
                    else
                    {
                        //get the state form the Allstates list 
                        StateSpace OldState = Helper.GetEqualState(this.AllStates, NewState);
                        //Get the CurState Node
                        Microsoft.Msagl.Drawing.Node CurNode = graph.FindNode(CurState.printEnumState());
                        //Get the NewState Node
                        Microsoft.Msagl.Drawing.Node NewNode = graph.FindNode(OldState.printEnumState());
                        //Create a temporary edge to check if it exist already
                        Microsoft.Msagl.Drawing.Edge TempEdge = new Microsoft.Msagl.Drawing.Edge(CurNode.Id, CurAction.Name, NewNode.Id);
                        if (!this.graph.Edges.Contains<Microsoft.Msagl.Drawing.Edge>(TempEdge))
                        {
                            //Add the arc between them
                            this.graph.AddEdge(CurNode.Id, CurAction.Name, NewNode.Id);
                        }
                        
                    }
                    
                }
                
            }
            //Check if there is a new state in the stateswaittoexpandd list which meets the target state
            List<StateSpace> Targets = Helper.IsTargetState(this.AllStates, this.Target);
            if (Targets.Count < 0)
                foreach (StateSpace CurTarget in Targets)
                {
                    this.TargetFound = new StateSpace( CurTarget);
                    Microsoft.Msagl.Drawing.Node c = graph.FindNode(CurTarget.printEnumState());
                    c.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;
                }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // count the Depthcounter + 1
            this.DepthCounter++;
            // The first step is to generate the target and start state
            StateSpace StartState = Helper.createStartState();
            //Add the first state to the AllStates list
            this.AllStates.Add(StartState);
            //Generate the target
            StateSpace TargetState = Helper.createTargetState();
            // and save it in this variable
            this.Target = new StateSpace(TargetState);
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
                // check if the action returns the same state as the tempstate
                if(!CurState.isTargetState(StartState))
                {
                    // save the new state in the StatesWaitToExpand list
                    this.StatesWaitingToExpand.Add(new StateSpace(CurState));
                    // save the new state in the AllStates list
                    this.AllStates.Add(new StateSpace(CurState));
                    // Add a new node to the graph
                    Microsoft.Msagl.Drawing.Node CurNode = new Microsoft.Msagl.Drawing.Node(CurState.printEnumState());
                    this.graph.AddNode(CurNode);
                    // Add a edge ( and the nodes ) to the graph
                    this.graph.AddEdge(StartNode.Id, CurAction.Name, CurNode.Id);
                }
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

        private void button3_Click(object sender, EventArgs e)
        {
            // Show the iteration setp in the textbox 2
            this.textBox2.Text = this.DepthCounter.ToString();
            this.textBox2.Refresh();
            // Show the number of nodes that exists
            this.textBox3.Text = this.AllStates.Count.ToString();
            this.textBox3.Refresh();
            //Show the number of nodes which are waiting to expan
            this.textBox4.Text = this.StatesWaitingToExpand.Count.ToString();
            this.textBox4.Refresh();
            //Show the number of node in the graph
            this.textBox5.Text = this.graph.NodeCount.ToString();
            this.textBox5.Refresh();
            //Show the number of edges in the graph
            this.textBox6.Text = this.graph.EdgeCount.ToString();
            this.textBox6.Refresh();
            //Show the number of goals
            if(this.TargetFound != null)
            {
                this.textBox7.Text = this.TargetFound.getId.ToString();
                this.textBox7.Refresh();
            }
            this.textBox7.Text = "No found";
            this.textBox7.Refresh();
            this.Refresh();
           

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // freeze the form logic
            Graphform.SuspendLayout();
            this.tabControl1.SuspendLayout();
            // change the graph layout method
            this.graph.LayoutAlgorithmSettings = new Microsoft.Msagl.Layout.MDS.MdsLayoutSettings();
            //bind the graph to the viewer 
            viewer.CurrentLayoutMethod = Microsoft.Msagl.GraphViewerGdi.LayoutMethod.UseSettingsOfTheGraph;
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
    }
}


