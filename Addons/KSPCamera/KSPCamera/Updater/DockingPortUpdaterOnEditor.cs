using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSPCamera
{
    [KSPAddon(KSPAddon.Startup.EditorAny, false)]
    class DockingPortUpdaterOnEditor : MonoBehaviour
    {
        protected void Awake()
        {
            GameEvents.onPartAttach.Add(PartAttach);
        }

        private void PartAttach(GameEvents.HostTargetAction<Part, Part> gameEvent)
        {
            AddDockingCamPart(gameEvent.host);
        }

        public static void AddDockingCamPart(Part part)
        {
            if (part.Modules.Contains("ModuleDockingNode") &&
                !part.Modules.Contains("DockingCamPart"))
                part.AddModule("DockingCamPart");

        }


        //private Vessel vessel
        //{
        //    get { return FlightGlobals.ActiveVessel; }
        //}
        //protected void Update()
        //{
        //    if (isInit) return;

        //    var dockingParts = vessel.parts
        //        .Where(part => part.partInfo.partConfig.HasNode("MODULE") &&
        //        part.partInfo.partConfig.GetNodes("MODULE")
        //        .Where(node => node.GetValue("name") == "ModuleDockingNode").Any());

        //    dockingParts.ToList().ForEach(cam => cam.AddModule("DickingFlag"));

        //    isInit = true;
        //}
    }
}
