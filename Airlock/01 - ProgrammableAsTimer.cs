using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using VRageMath;
using VRage.Game;
using Sandbox.ModAPI.Interfaces;
using Sandbox.ModAPI.Ingame;
using Sandbox.Game.EntityComponents;
using VRage.Game.Components;
using VRage.Collections;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game.ModAPI.Ingame;
using SpaceEngineers.Game.ModAPI.Ingame;

namespace ScriptingClass
{
    class ProgrammableAsTimer
    {
        IMyGridTerminalSystem GridTerminalSystem = null;
        IMyGridProgramRuntimeInfo Runtime = null;
        Action<string> Echo = null;
        IMyTerminalBlock Me = null;
        //=======================================================================================
        //////////////////////////START//////////////////////////////////////////
        //=======================================================================================


        static string AirlockGroupName = "Airlock 1";

        IMyBlockGroup AllAirlockBlocks;

        void Main(string RuntimeArguments, UpdateType RuntimeUpdateType)
        {

            //if (AllAirlockBlocks == null)
            //{
                AllAirlockBlocks = GridTerminalSystem.GetBlockGroupWithName(AirlockGroupName);
            //}


            //if (UpdateConditionButtonPress(RuntimeUpdateType))
            //{
                IMyTerminalBlock InnerDoor;

                List<IMyTerminalBlock> DoorGroup = new List<IMyTerminalBlock>(); ;
            
                AllAirlockBlocks.GetBlocksOfType<IMyTerminalBlock>(DoorGroup);

                for (int i = 0; i < DoorGroup.Count-1; i++)
                {
                    //if (DoorGroup[i].CustomName.ToLower().Contains("inner door"))
                    //{
                        InnerDoor = DoorGroup[i];
                        var test = InnerDoor.GetActionWithName("ToggleDoor");
                test.Apply(InnerDoor);
                    //}
                }

            //}

        }

        #region Helper Methods
        public bool UpdateConditionButtonPress(UpdateType RuntimeUpdateType)
        {
            //the UpdateType has some special syntax for checking update type. saves some characters.
            //add accepted updatetypes in the paranthesis with "|" between them, like so:
            if ((RuntimeUpdateType & (UpdateType.Terminal | UpdateType.Trigger)) != 0)
            {
                return true;
            }
            return false;

        }
        #endregion

        //=======================================================================================
        //////////////////////////END//////////////////////////////////////////
        //=======================================================================================
    }
}
