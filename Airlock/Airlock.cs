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
using System.Runtime.InteropServices;
using Sandbox.Game.Gui;
//using Sandbox.ModAPI;

namespace Airlock
{
    class MyScript
    {
        IMyGridTerminalSystem GridTerminalSystem = null;
        IMyGridProgramRuntimeInfo Runtime = null;
        Action<string> Echo = null;
        IMyTerminalBlock Me = null;
        //=======================================================================================
        //////////////////////////START//////////////////////////////////////////
        //=======================================================================================

        void Main(string argument, UpdateType updateType)
        {

            

            if ((updateType == UpdateType.Trigger || updateType == UpdateType.Terminal) && Runtime.UpdateFrequency == UpdateFrequency.None)
            {
                Runtime.UpdateFrequency = UpdateFrequency.Update100;
            }
            else if (Runtime.UpdateFrequency == UpdateFrequency.Update100)
            {
                bool UserFound = false;
                //do stuff in cycle

                var AllAirlockBlocks = GridTerminalSystem.GetBlockGroupWithName("Airlocks");

                List<IMySensorBlock> AllSensorBlocks = new List<IMySensorBlock>();
                AllAirlockBlocks.GetBlocksOfType(AllSensorBlocks);
                

                //if all sensors report no users, close all doors, depressurise airlocks and stop script from running again:
                if (!UserFound)
                {

                    List<IMyAirVent> AllAirVents = new List<IMyAirVent>();
                    AllAirlockBlocks.GetBlocksOfType<IMyAirVent>(AllAirVents);
                    

                    foreach (var item in AllAirVents)
                    {
                        item.Depressurize = true;
                    }

                    Runtime.UpdateFrequency = UpdateFrequency.None;
                }
                
            }






            if ((updateType & (UpdateType.Update1 | UpdateType.Update100 | UpdateType.Update10)) != 0)
            {
                // The code you want to run when the given update types are called should be placed here
            }



            var outerair = GridTerminalSystem.GetBlockGroupWithName("OuterAirtightDoors") as IMyAirtightSlideDoor;
            outerair.OpenDoor();

            var sensors = GridTerminalSystem.GetBlockGroupWithName("AirlockSensor") as IMySensorBlock;

            if (sensors.IsActive)
            {
                outerair.CloseDoor();

            }


            
        }

        //=======================================================================================
        //////////////////////////END//////////////////////////////////////////
        //=======================================================================================
    }
}
