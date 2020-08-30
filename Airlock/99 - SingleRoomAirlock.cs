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

        //This script is supposed to facilitate a single room airlock with buttons for entry and exit.
        //each airlock requires 2 buttons, 2 doors, a vent, a sensor set to look in the airlock room and optionally LCD's for outside, airlock and inside.

        static string AirlockGroupName = "Airlocks";

        List<Airlock> Triggerlist;
        IMyBlockGroup AllAirlockBlocks;

        void Main(string argument, UpdateType updateType)
        {
            if (Triggerlist == null)
            {
                Triggerlist = new List<Airlock>();
            }

            if (AllAirlockBlocks == null)
            {
                AllAirlockBlocks = GridTerminalSystem.GetBlockGroupWithName(AirlockGroupName);
            }
            



            if (UpdateConditionUserOrBlock())
            #region User-triggered behaviour
            {
                bool ValidAirlock = Int32.TryParse(argument.Split('-')[0], out int AirlockNumber);

                //do nothing if bad input given from button
                if (!ValidAirlock)
                {
                    throw new Exception("Action must be <AirlockNumber>-<AirlockAction> with valid actions being 'entry' and 'exit'");
                }

                string AirlockAction = argument.Split('-')[1].ToLower();

                //we are working with entry and exit. no other cases are handled.
                if (AirlockAction != "entry" || AirlockAction != "exit")
                {
                    throw new Exception("Action must be <AirlockNumber>-<AirlockAction> with valid actions being 'entry' and 'exit'");
                }

                Airlock ThisAirlock = new Airlock(AirlockAction, AirlockNumber);
                Triggerlist.Add(ThisAirlock);

                ThisAirlock.DoAction();
            }
            #endregion

            AllAirlockBlocks.GetBlocksOfType<IMyTextPanel>(new List<IMyTextPanel>());

            if (UpdateConditionRecurrence100())
            #region ReoccuringBehaviour
            {
                bool UserFound = false;
                //do stuff in cycle


                List<IMySensorBlock> AllSensorBlocks = new List<IMySensorBlock>();
                AllAirlockBlocks.GetBlocksOfType<IMySensorBlock>(AllSensorBlocks);

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

            #endregion



            #region MainHelperMethods
            bool UpdateConditionUserOrBlock()
            {

                if ((updateType & (UpdateType.Trigger | UpdateType.Terminal)) != 0)
                {
                    return true;
                }
                return false;
            }

            bool UpdateConditionRecurrence100()
            {

                if ((updateType & (UpdateType.Update100)) != 0)
                {
                    return true;
                }
                return false;
            }


            //bool UpdateConditionTemplate()
            //{

            //    if ((updateType & (UpdateType.Update10)) != 0)
            //    {
            //        return true;
            //    }
            //    return false;
            //}

            #endregion

        }

        public class Airlock
        {
            string AirlockAction;
            int AirlockNumber;
            bool UserEntered;
            bool UserLeft;
            DateTime LastUpdated;

            public Airlock(string _airlockAction, int _airlockNumber)
            {
                AirlockAction = _airlockAction;
                AirlockNumber = _airlockNumber;
                UserEntered = false;
                UserLeft = false;
                LastUpdated = DateTime.Now;
            }

            public void DoAction()
            {
                if (AirlockAction == "entry")
                {
                    LastUpdated = DateTime.Now;
                    StartEntry(this);
                }
                else if (AirlockAction == "exit")
                {
                    LastUpdated = DateTime.Now;
                    StartExit(this);
                }
            }


        }


        //=======================================================================================
        //////////////////////////END//////////////////////////////////////////
        //=======================================================================================
    }
}
