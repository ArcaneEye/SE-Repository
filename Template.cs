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
    class MyScript
    {
        IMyGridTerminalSystem GridTerminalSystem = null;
        IMyGridProgramRuntimeInfo Runtime = null;
        Action<string> Echo = null;
        IMyTerminalBlock Me = null;
        //=======================================================================================
        //////////////////////////START//////////////////////////////////////////
        //=======================================================================================

        void Main(string RuntimeArguments, UpdateType RuntimeUpdateType)
        {

            if (UpdateConditionTemplate())
            {
                //do stuff that fits the updateconditions here
            }





            #region Helper Methods
            bool UpdateConditionTemplate()
            {
                //the UpdateType has some special syntax for checking update type. saves some characters.
                //add accepted updatetypes in the paranthesis with "|" between them, like so:
                if ((RuntimeUpdateType & (UpdateType.Update1 | UpdateType.Update100 | UpdateType.Update10 | UpdateType.Terminal)) != 0)
                {
                    return true;
                }
                return false;

            }
            #endregion
        }

        //=======================================================================================
        //////////////////////////END//////////////////////////////////////////
        //=======================================================================================
    }
}
