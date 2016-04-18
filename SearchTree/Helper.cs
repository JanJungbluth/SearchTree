using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchTree
{
    public static class Helper
    {
        #region Enumeration

        public enum TagsDim : int
        {
            RobotEffector = 0, RobotPosition, HumanTask, HumanScrew, HumanTool, HumanAssistance,
            LeftHand, LeftHandPosition, RightHand, RightHandPosition, Screw, ScrewPosition, ToolPosition, BoxPosition
        }

        public enum TagsRobot : int
        {
            EMPTY = 0, HAS_GRIPPER, HAS_SCRWEDRIVER, HAS_OBJECT,
            AT_HOME_POS = 0, AT_LEFT_HAND_POS, AT_RIGHT_HAND_POS, AT_GRIPPER_MAGAZINE_POS, AT_SCREWDRIVER_MAGAZIN_POS, AT_SCREW_POS,
            AT_FULLY_LOOSE_SCREW_POS, AT_TOOL_MAGAZINE_POS, AT_BOX_MAGAZIN_POS
        };

        public enum TagsHuman
        {
            DONT_KNOW_TASK = 0, KNOW_TASK,
            DONT_KNOW_SCREW = 0, KNOW_SCREW,
            DONT_KNOW_TOOL = 0, KNOW_TOOL,
            DONT_KNOW_ASSISTANCE = 0, KNOW_ASSISTANCE,
            EMPTY = 0, HAS_OBJECT,
            AT_BODY_POS = 0, AT_LEFT_HAND_POS, AT_RIGHT_HAND_POS, AT_SCREW_POS, AT_FULLY_LOOSE_SCREW_POS, AT_TOOL_MAGAZINE_POS, AT_BOX_MAGAZINE_POS
        };
        public enum TagsScrew
        {
            TIGHTENED = 0, LOOSE, FULLY_LOOSE, REMOVED,
            AT_SCREW_POS = 0, AT_FULLY_LOOSE_SCREW_POS, AT_GRIPPER, AT_HUMAN_LEFT_HAND, AT_HUMAN_RIGHT_HAND, AT_BOX_MAGAZINE_POS
        };
        public enum TagsTool
        {
            AT_TOOL_MAGAZINE_POS = 0, AT_LEFT_HAND_POS, AT_RIGHT_HAND_POS, AT_GRIPPER, AT_HUMAN_LEFT_HAND, AT_HUMAN_RIGHT_HAND,
        };
        public enum TagsBox
        {
            AT_BOX_MAGAZINE_POS = 0, AT_LEFT_HAND_POS, AT_RIGHT_HAND_POS, AT_GRIPPER, AT_HUMAN_LEFT_HAND, AT_HUMAN_RIGHT_HAND,
        };
        #endregion

        public static StateSpace createStartState()
        {
            const int StateSpaceDim = 14;

            StateSpace StartState = new StateSpace(0,0,StateSpaceDim, new int[] { 4, 9, 2, 2, 2, 2, 2, 5, 2, 5, 4, 6, 6, 6 });

            StartState.StateVec = new int[]{ (int)TagsRobot.EMPTY, (int)TagsRobot.AT_HOME_POS,
                                             (int)TagsHuman.KNOW_TASK,(int)TagsHuman.KNOW_SCREW,(int)TagsHuman.KNOW_TOOL,(int)TagsHuman.KNOW_ASSISTANCE,
                                             (int)TagsHuman.EMPTY,(int)TagsHuman.AT_BODY_POS,(int)TagsHuman.EMPTY,(int)TagsHuman.AT_BODY_POS,
                                             (int)TagsScrew.TIGHTENED, (int)TagsScrew.AT_SCREW_POS, (int)TagsTool.AT_TOOL_MAGAZINE_POS,  (int)TagsBox.AT_BOX_MAGAZINE_POS };
            return StartState;
        }
        public static StateSpace createTargetState()
        {
            const int StateSpaceDim = 14;

            StateSpace TargetState = new StateSpace(0,0,StateSpaceDim, new int[] { 4, 9, 2, 2, 2, 2, 2, 5, 2, 5, 4, 6, 6, 6 });
            TargetState.StateVec = new int[]{(int)TagsRobot.EMPTY, (int)TagsRobot.AT_HOME_POS,
                                             (int)TagsHuman.KNOW_TASK,(int)TagsHuman.KNOW_SCREW,(int)TagsHuman.KNOW_TOOL,(int)TagsHuman.KNOW_ASSISTANCE,
                                             (int)TagsHuman.EMPTY,(int)TagsHuman.AT_BODY_POS,(int)TagsHuman.EMPTY,(int)TagsHuman.AT_BODY_POS,
                                             (int)TagsScrew.REMOVED, (int)TagsScrew.AT_BOX_MAGAZINE_POS, (int)TagsTool.AT_TOOL_MAGAZINE_POS,  (int)TagsBox.AT_BOX_MAGAZINE_POS };
            return TargetState;
        }
        public static List<Action> createRobotActionSet()
        {
            #region Define Robot Actions

            #region Goto Statements
            Action GOTO_HOME_POS = new Action("GOTO_HOME_POS", 0,
                new int[] { },
                new int[] { },
                new int[] { (int)TagsDim.RobotPosition },
                new int[] { (int)TagsRobot.AT_HOME_POS });
            Action GOTO_LEFT_HAND_POS = new Action("GOTO_LEFT_HAND_POS", 0,
                new int[] { },
                new int[] { },
                new int[] { (int)TagsDim.RobotPosition },
                new int[] { (int)TagsRobot.AT_LEFT_HAND_POS });
            Action GOTO_RIGHT_HAND_POS = new Action("GOTO_RIGHT_HAND_POS", 0,
                new int[] { },
                new int[] { },
                new int[] { (int)TagsDim.RobotPosition },
                new int[] { (int)TagsRobot.AT_RIGHT_HAND_POS });
            Action GOTO_GRIPPER_MAGAZIN_POS = new Action("GOTO_GRIPPER_MAGAZIN_POS", 0,
                new int[] { },
                new int[] { },
                new int[] { (int)TagsDim.RobotPosition },
                new int[] { (int)TagsRobot.AT_GRIPPER_MAGAZINE_POS });
            Action GOTO_SCREWDRIVER_MAGAZIN_POS = new Action("GOTO_SCREWDRIVER_MAGAZIN_POS", 0,
                new int[] { },
                new int[] { },
                new int[] { (int)TagsDim.RobotPosition },
                new int[] { (int)TagsRobot.AT_SCREWDRIVER_MAGAZIN_POS });
            Action GOTO_SCREW_POS = new Action("GOTO_SCREW_POS", 0,
                new int[] { },
                new int[] { },
                new int[] { (int)TagsDim.RobotPosition },
                new int[] { (int)TagsRobot.AT_SCREW_POS });
            Action GOTO_FULLY_LOOSE_SCREW_POS = new Action("GOTO_FULLY_LOOSE_SCREW_POS", 0,
                new int[] { },
                new int[] { },
                new int[] { (int)TagsDim.RobotPosition },
                new int[] { (int)TagsRobot.AT_FULLY_LOOSE_SCREW_POS });
            Action GOTO_TOOL_MAGAZIN_POS = new Action("GOTO_TOOL_MAGAZIN_POS", 0,
                new int[] { },
                new int[] { },
                new int[] { (int)TagsDim.RobotPosition },
                new int[] { (int)TagsRobot.AT_TOOL_MAGAZINE_POS });
            Action GOTO_BOX_MAGAZIN_POS = new Action("GOTO_BOX_MAGAZIN_POS", 0,
                new int[] { },
                new int[] { },
                new int[] { (int)TagsDim.RobotPosition },
                new int[] { (int)TagsRobot.AT_BOX_MAGAZIN_POS });
            #endregion

            #region  Take Statements
            Action TAKE_GRIPPER = new Action("TAKE_GRIPPER", 1,
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.RobotPosition },
                new int[] { (int)TagsRobot.EMPTY, (int)TagsRobot.AT_GRIPPER_MAGAZINE_POS },
                new int[] { (int)TagsDim.RobotEffector },
                new int[] { (int)TagsRobot.HAS_GRIPPER });
            Action TAKE_SCREWDRIVER = new Action("TAKE_SCREWDRIVER", 1,
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.RobotPosition },
                new int[] { (int)TagsRobot.EMPTY, (int)TagsRobot.AT_SCREWDRIVER_MAGAZIN_POS },
                new int[] { (int)TagsDim.RobotEffector },
                new int[] { (int)TagsRobot.HAS_SCRWEDRIVER });
            Action TAKE_TOOL = new Action("TAKE_TOOL", 1,
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.RobotPosition, (int)TagsDim.ToolPosition },
                new int[] { (int)TagsRobot.HAS_GRIPPER, (int)TagsRobot.AT_TOOL_MAGAZINE_POS, (int)TagsTool.AT_TOOL_MAGAZINE_POS },
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.ToolPosition },
                new int[] { (int)TagsRobot.HAS_OBJECT, (int)TagsTool.AT_GRIPPER });
            Action TAKE_BOX = new Action("TAKE_BOX", 1,
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.RobotPosition, (int)TagsDim.BoxPosition },
                new int[] { (int)TagsRobot.HAS_GRIPPER, (int)TagsRobot.AT_BOX_MAGAZIN_POS, (int)TagsBox.AT_BOX_MAGAZINE_POS },
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.BoxPosition },
                new int[] { (int)TagsRobot.HAS_OBJECT, (int)TagsBox.AT_GRIPPER });
            #endregion

            #region Put Back Statements
            Action PUT_BACK_GRIPPER = new Action("PUT_BACK_GRIPPER", 1,
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.RobotPosition },
                new int[] { (int)TagsRobot.HAS_GRIPPER, (int)TagsRobot.AT_GRIPPER_MAGAZINE_POS },
                new int[] { (int)TagsDim.RobotEffector },
                new int[] { (int)TagsRobot.EMPTY });
            Action PUT_BACK_SCREWDRIVER = new Action("PUT_BACK_SCREWDRIVER", 1,
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.RobotPosition },
                new int[] { (int)TagsRobot.HAS_SCRWEDRIVER, (int)TagsRobot.AT_SCREWDRIVER_MAGAZIN_POS },
                new int[] { (int)TagsDim.RobotEffector },
                new int[] { (int)TagsRobot.EMPTY });
            Action PUT_BACK_TOOL = new Action("PUT_BACK_TOOL", 1,
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.RobotPosition, (int)TagsDim.ToolPosition },
                new int[] { (int)TagsRobot.HAS_OBJECT, (int)TagsRobot.AT_TOOL_MAGAZINE_POS, (int)TagsTool.AT_GRIPPER },
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.ToolPosition },
                new int[] { (int)TagsRobot.HAS_GRIPPER, (int)TagsTool.AT_TOOL_MAGAZINE_POS });
            Action PUT_BACK_BOX = new Action("PUT_BACK_BOX", 1,
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.RobotPosition, (int)TagsDim.BoxPosition },
                new int[] { (int)TagsRobot.HAS_OBJECT, (int)TagsRobot.AT_BOX_MAGAZIN_POS, (int)TagsBox.AT_GRIPPER },
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.BoxPosition },
                new int[] { (int)TagsRobot.HAS_GRIPPER, (int)TagsBox.AT_BOX_MAGAZINE_POS });
            Action PUT_SCREW_IN_BOX = new Action("PUT_SCREW_IN_BOX", 1,
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.RobotPosition, (int)TagsDim.Screw, (int)TagsDim.ScrewPosition },
                new int[] { (int)TagsRobot.HAS_OBJECT, (int)TagsRobot.AT_BOX_MAGAZIN_POS, (int)TagsScrew.REMOVED, (int)TagsScrew.AT_GRIPPER },
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.ScrewPosition },
                new int[] { (int)TagsRobot.HAS_GRIPPER, (int)TagsScrew.AT_BOX_MAGAZINE_POS });
            #endregion

            #region HAND Statements
            Action HAND_TOOL_LEFT = new Action("HAND_TOOL_LEFT", 2,
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.RobotPosition, (int)TagsDim.LeftHand, (int)TagsDim.LeftHandPosition, (int)TagsDim.ToolPosition },
                new int[] { (int)TagsRobot.HAS_OBJECT, (int)TagsRobot.AT_LEFT_HAND_POS, (int)TagsHuman.EMPTY, (int)TagsHuman.AT_LEFT_HAND_POS, (int)TagsTool.AT_GRIPPER },
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.LeftHand, (int)TagsDim.ToolPosition },
                new int[] { (int)TagsRobot.HAS_GRIPPER, (int)TagsHuman.HAS_OBJECT, (int)TagsTool.AT_HUMAN_LEFT_HAND });
            Action HAND_TOOL_RIGHT = new Action("HAND_TOOL_RIGHT", 2,
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.RobotPosition, (int)TagsDim.RightHand, (int)TagsDim.RightHandPosition, (int)TagsDim.ToolPosition },
                new int[] { (int)TagsRobot.HAS_OBJECT, (int)TagsRobot.AT_RIGHT_HAND_POS, (int)TagsHuman.EMPTY, (int)TagsHuman.AT_RIGHT_HAND_POS, (int)TagsTool.AT_GRIPPER },
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.RightHand, (int)TagsDim.ToolPosition },
                new int[] { (int)TagsRobot.HAS_GRIPPER, (int)TagsHuman.HAS_OBJECT, (int)TagsTool.AT_HUMAN_RIGHT_HAND });
            Action HAND_BOX_LEFT = new Action("HAND_BOX_LEFT", 2,
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.RobotPosition, (int)TagsDim.LeftHand, (int)TagsDim.LeftHandPosition, (int)TagsDim.BoxPosition },
                new int[] { (int)TagsRobot.HAS_OBJECT, (int)TagsRobot.AT_LEFT_HAND_POS, (int)TagsHuman.EMPTY, (int)TagsHuman.AT_LEFT_HAND_POS, (int)TagsBox.AT_GRIPPER },
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.LeftHand, (int)TagsDim.BoxPosition },
                new int[] { (int)TagsRobot.HAS_GRIPPER, (int)TagsHuman.HAS_OBJECT, (int)TagsBox.AT_HUMAN_LEFT_HAND });
            Action HAND_BOX_RIGHT = new Action("HAND_BOX_RIGHT", 2,
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.RobotPosition, (int)TagsDim.RightHand, (int)TagsDim.RightHandPosition, (int)TagsDim.BoxPosition },
                new int[] { (int)TagsRobot.HAS_OBJECT, (int)TagsRobot.AT_RIGHT_HAND_POS, (int)TagsHuman.EMPTY, (int)TagsHuman.AT_RIGHT_HAND_POS, (int)TagsBox.AT_GRIPPER },
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.RightHand, (int)TagsDim.BoxPosition },
                new int[] { (int)TagsRobot.HAS_GRIPPER, (int)TagsHuman.HAS_OBJECT, (int)TagsBox.AT_HUMAN_RIGHT_HAND });
            #endregion

            #region Return Statements
            Action RETURN_TOOL_LEFT = new Action("RETURN_TOOL_LEFT", 2,
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.RobotPosition, (int)TagsDim.LeftHand, (int)TagsDim.LeftHandPosition, (int)TagsDim.ToolPosition },
                new int[] { (int)TagsRobot.HAS_GRIPPER, (int)TagsRobot.AT_LEFT_HAND_POS, (int)TagsHuman.HAS_OBJECT, (int)TagsHuman.AT_LEFT_HAND_POS, (int)TagsTool.AT_HUMAN_LEFT_HAND },
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.LeftHand, (int)TagsDim.ToolPosition },
                new int[] { (int)TagsRobot.HAS_OBJECT, (int)TagsHuman.EMPTY, (int)TagsTool.AT_GRIPPER });
            Action RETURN_TOOL_RIGHT = new Action("RETURN_TOOL_RIGHT", 2,
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.RobotPosition, (int)TagsDim.RightHand, (int)TagsDim.RightHandPosition, (int)TagsDim.ToolPosition },
                new int[] { (int)TagsRobot.HAS_GRIPPER, (int)TagsRobot.AT_RIGHT_HAND_POS, (int)TagsHuman.HAS_OBJECT, (int)TagsHuman.AT_RIGHT_HAND_POS, (int)TagsTool.AT_HUMAN_RIGHT_HAND },
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.RightHand, (int)TagsDim.ToolPosition },
                new int[] { (int)TagsRobot.HAS_OBJECT, (int)TagsHuman.EMPTY, (int)TagsTool.AT_GRIPPER });
            Action RETURN_BOX_LEFT = new Action("RETURN_BOX_LEFT", 2,
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.RobotPosition, (int)TagsDim.LeftHand, (int)TagsDim.LeftHandPosition, (int)TagsDim.BoxPosition },
                new int[] { (int)TagsRobot.HAS_GRIPPER, (int)TagsRobot.AT_LEFT_HAND_POS, (int)TagsHuman.HAS_OBJECT, (int)TagsHuman.AT_LEFT_HAND_POS, (int)TagsBox.AT_HUMAN_LEFT_HAND },
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.LeftHand, (int)TagsDim.BoxPosition },
                new int[] { (int)TagsRobot.HAS_OBJECT, (int)TagsHuman.EMPTY, (int)TagsBox.AT_GRIPPER });
            Action RETURN_BOX_RIGHT = new Action("RETURN_BOX_RIGHT", 2,
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.RobotPosition, (int)TagsDim.RightHand, (int)TagsDim.RightHandPosition, (int)TagsDim.BoxPosition },
                new int[] { (int)TagsRobot.HAS_GRIPPER, (int)TagsRobot.AT_RIGHT_HAND_POS, (int)TagsHuman.HAS_OBJECT, (int)TagsHuman.AT_RIGHT_HAND_POS, (int)TagsBox.AT_HUMAN_RIGHT_HAND },
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.RightHand, (int)TagsDim.BoxPosition },
                new int[] { (int)TagsRobot.HAS_OBJECT, (int)TagsHuman.EMPTY, (int)TagsBox.AT_GRIPPER });
            #endregion

            #region Loose Joint Statements
            Action LOOSE_SCREW = new Action("LOOSE_SCREW", 3,
                new int[] { (int)TagsDim.RobotEffector,     (int)TagsDim.RobotPosition, (int)TagsDim.Screw, (int)TagsDim.ScrewPosition },
                new int[] { (int)TagsRobot.HAS_SCRWEDRIVER, (int)TagsRobot.AT_SCREW_POS, (int)TagsScrew.TIGHTENED, (int)TagsScrew.AT_SCREW_POS },
                new int[] { (int)TagsDim.Screw },
                new int[] { (int)TagsScrew.LOOSE });
            Action LOOSE_SCREW_FULLY = new Action("LOOSE_SCREW_FULLY", 4,
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.RobotPosition, (int)TagsDim.Screw, (int)TagsDim.ScrewPosition },
                new int[] { (int)TagsRobot.HAS_SCRWEDRIVER, (int)TagsRobot.AT_SCREW_POS, (int)TagsScrew.LOOSE, (int)TagsScrew.AT_SCREW_POS },
                new int[] { (int)TagsDim.RobotPosition, (int)TagsDim.Screw, (int)TagsDim.ScrewPosition },
                new int[] { (int)TagsRobot.AT_FULLY_LOOSE_SCREW_POS, (int)TagsScrew.FULLY_LOOSE, (int)TagsScrew.AT_FULLY_LOOSE_SCREW_POS });
            Action REMOVE_SCREW = new Action("REMOVE_SCREW", 3,
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.RobotPosition, (int)TagsDim.Screw, (int)TagsDim.ScrewPosition },
                new int[] { (int)TagsRobot.HAS_GRIPPER, (int)TagsRobot.AT_FULLY_LOOSE_SCREW_POS, (int)TagsScrew.FULLY_LOOSE, (int)TagsHuman.AT_FULLY_LOOSE_SCREW_POS },
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.Screw, (int)TagsDim.ScrewPosition },
                new int[] { (int)TagsRobot.HAS_OBJECT, (int)TagsScrew.REMOVED, (int)TagsScrew.AT_GRIPPER });
            Action LOOSE_AND_REMOVE_SCREW = new Action("LOOSE_AND_REMOVE_SCREW", 5,
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.RobotPosition, (int)TagsDim.Screw, (int)TagsDim.ScrewPosition },
                new int[] { (int)TagsRobot.HAS_SCRWEDRIVER, (int)TagsRobot.AT_SCREW_POS, (int)TagsScrew.TIGHTENED, (int)TagsHuman.AT_SCREW_POS },
                new int[] { (int)TagsDim.RobotEffector, (int)TagsDim.Screw, (int)TagsDim.ScrewPosition },
                new int[] { (int)TagsRobot.HAS_OBJECT, (int)TagsScrew.REMOVED, (int)TagsScrew.AT_GRIPPER });
            #endregion

            #region Information Statements
            Action INFORM_TASK = new Action("INFORM_TASK", 2,
                new int[] { (int)TagsDim.HumanTask },
                new int[] { (int)TagsHuman.DONT_KNOW_TASK },
                new int[] { (int)TagsDim.HumanTask },
                new int[] { (int)TagsHuman.KNOW_TASK });
            Action INFORM_SCREW = new Action("INFORM_SCREW", 2,
                new int[] { (int)TagsDim.HumanScrew },
                new int[] { (int)TagsHuman.DONT_KNOW_SCREW },
                new int[] { (int)TagsDim.HumanScrew },
                new int[] { (int)TagsHuman.KNOW_SCREW });
            Action INFORM_TOOL = new Action("INFORM_TOLL", 2,
                new int[] { (int)TagsDim.HumanTool },
                new int[] { (int)TagsHuman.DONT_KNOW_TOOL },
                new int[] { (int)TagsDim.HumanTool },
                new int[] { (int)TagsHuman.KNOW_TOOL });
            Action INFORM_ASSISTANCE = new Action("INFORM_ASSISTANCE", 2,
                new int[] { (int)TagsDim.HumanAssistance },
                new int[] { (int)TagsHuman.DONT_KNOW_ASSISTANCE },
                new int[] { (int)TagsDim.HumanAssistance },
                new int[] { (int)TagsHuman.KNOW_ASSISTANCE });


            #endregion

            #endregion

            // Create new List of which contains all possible Actions
            List<Action> RobotActionSet = new List<Action>();

            #region Fill List with actions
            RobotActionSet.Add(GOTO_HOME_POS);
            RobotActionSet.Add(GOTO_LEFT_HAND_POS);
            RobotActionSet.Add(GOTO_RIGHT_HAND_POS);
            RobotActionSet.Add(GOTO_GRIPPER_MAGAZIN_POS);
            RobotActionSet.Add(GOTO_SCREWDRIVER_MAGAZIN_POS);
            RobotActionSet.Add(GOTO_TOOL_MAGAZIN_POS);
            RobotActionSet.Add(GOTO_SCREW_POS);
            RobotActionSet.Add(GOTO_FULLY_LOOSE_SCREW_POS);
            RobotActionSet.Add(GOTO_BOX_MAGAZIN_POS);
            RobotActionSet.Add(TAKE_GRIPPER);
            RobotActionSet.Add(TAKE_SCREWDRIVER);
            RobotActionSet.Add(TAKE_TOOL);
            RobotActionSet.Add(TAKE_BOX);
            RobotActionSet.Add(PUT_BACK_GRIPPER);
            RobotActionSet.Add(PUT_BACK_SCREWDRIVER);
            RobotActionSet.Add(PUT_BACK_TOOL);
            RobotActionSet.Add(PUT_BACK_BOX);
            RobotActionSet.Add(PUT_SCREW_IN_BOX);
            RobotActionSet.Add(HAND_TOOL_LEFT);
            RobotActionSet.Add(HAND_TOOL_RIGHT);
            RobotActionSet.Add(HAND_BOX_LEFT);
            RobotActionSet.Add(HAND_BOX_RIGHT);
            RobotActionSet.Add(RETURN_TOOL_LEFT);
            RobotActionSet.Add(RETURN_TOOL_RIGHT);
            RobotActionSet.Add(RETURN_BOX_LEFT);
            RobotActionSet.Add(RETURN_BOX_RIGHT);
            RobotActionSet.Add(LOOSE_SCREW);
            RobotActionSet.Add(LOOSE_SCREW_FULLY);
            RobotActionSet.Add(REMOVE_SCREW);
            RobotActionSet.Add(LOOSE_AND_REMOVE_SCREW);
            RobotActionSet.Add(INFORM_TASK);
            RobotActionSet.Add(INFORM_SCREW);
            RobotActionSet.Add(INFORM_TOOL);
            RobotActionSet.Add(INFORM_ASSISTANCE);
            #endregion

            return RobotActionSet;
        }
        public static List<Action> PossibleActionSet(List<Action> ActionList, StateSpace State)
        {
            // Create a new action set which contains only the feasible actions
            List<Action> PosActionSet = new List<Action>();
            // go thru the action list
            foreach (Action CurAction in ActionList)
            {
                // Check if the action is applieable on thaht state
                if (CurAction.CheckPrecondition(State))
                {
                    // if it is, then add the action to the possible action list
                    PosActionSet.Add(CurAction);
                }
            }
            return PosActionSet;
        }
        public static List<StateSpace> KillSadStates(List<StateSpace> StateList, int StepSize)
        {
            //define minimum happiness for that level
            double MinHappiness = 0.2 * StepSize;
            // Create a new reverse ordered state list
            List<StateSpace> ReverseOrderedSet = new List<StateSpace>();
            // sort  by depth of the nodes
            ReverseOrderedSet = StateList.OrderBy(o => o.Depth).ToList<StateSpace>();
            // reverse the list
            ReverseOrderedSet.Reverse();
            // Create a new state set which contains only the sad states
            List<StateSpace> SadSet = new List<StateSpace>();
            // go thru the states in the list
            foreach (StateSpace CurState in ReverseOrderedSet)
            {
                // Check if the state is happy in that level
                if (CurState.Happiness <= MinHappiness || CurState.DontKillMe)
                {
                    // if it is, then add the action to the sad state list
                    SadSet.Add(CurState);
                }
            }
            // return the sad states to kill them
            return SadSet;
        }
        public static List<StateSpace> IsTargetState(List<StateSpace> StatesWaitToExpand, StateSpace TargetState)
        {
            List<StateSpace> IsTargetState = new List<StateSpace>();
            foreach(StateSpace CurState in StatesWaitToExpand)
            {
                if(CurState == TargetState)
                {
                    IsTargetState.Add(CurState);
                }
            }
            return IsTargetState;
        }
        
    }
}
