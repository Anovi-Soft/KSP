using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSPCam
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    class DockingModuleCam : MonoBehaviour
    {
        private List<CameraForDocking> camList = new List<CameraForDocking>();
        private bool a = false;

        private Vessel vessel 
        {
            get {return FlightGlobals.ActiveVessel;}
        }
        private List<string> allDockingParts 
        {
            get
            {
                return GameDatabase.Instance.GetConfigNodes("PART")
                .Where(part => part.HasNode("MODULE") &&
                    part.GetNodes("MODULE")
                    .Where(subPart =>
                        subPart.GetValue("name") == "ModuleDockingNode")
                    .Any())
                    .Select(part => part.name)
                    .ToList();
            }
        }
            
            
        protected void Update()
        {
            if (!a)
            {
                //Поиск part содержащих в нодах модуль с именем ModuleDockingNode
                var dockingParts = vessel.parts
                    .Where(part => part.partInfo.partConfig.HasNode("MODULE") &&
                    part.partInfo.partConfig.GetNodes("MODULE")
                    .Where(node => node.GetValue("name") == "ModuleDockingNode").Any());

                //camList = dockingParts
                //    .Select(part => new CameraForDocking(part, false, false))
                //    .ToList();
                dockingParts.ToList().ForEach(cam => cam.AddModule("DickingFlag"));

                a = true;
            }
            //else
            //{
            //    camList.ForEach(cam => cam.UpdateTransform());
            //}

        }


    }
}
