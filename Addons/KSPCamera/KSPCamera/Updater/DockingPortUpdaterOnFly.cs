using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSPCamera
{
    [KSPAddon(KSPAddon.Startup.Flight, true)]
    class DockingPortUpdaterOnFly : MonoBehaviour
    {
        bool isInit = false;
        protected void Awake()
        {
            GameEvents.onPartAttach.Add(PartAttach);
            GameEvents.onPartDestroyed.Add(PartCameraDeactivate);
            GameEvents.onVesselDestroy.Add(VesselDestroy);
            
        }
        protected void Start()
        {
            isInit = false;
        }
        private void PartAttach(GameEvents.HostTargetAction<Part, Part> gameEvent)
        {
            //DockingPortUpdaterOnEditor.AddDockingCamPart(gameEvent.host);
            isInit = false;
        }
        private void VesselDestroy(Vessel vessel)
        {
            foreach (var part in vessel.parts)
                PartCameraDeactivate(part);
        }
        private void PartCameraDeactivate(Part part)
        {
            foreach (var module in part.Modules)
            {
                var kspCamera = module as ICamPart;
                if (kspCamera != null)
                    kspCamera.Deavtivate();
            }
        }
        public static void AddDockingCamPart(Part part)
        {
            if (part.Modules.Contains("ModuleDockingNode") &&
                !part.Modules.Contains("DockingCamPart"))
                part.AddModule("DockingCamPart");
            
            foreach (var module in part.Modules)
            {
                var kspCamera = module as ICamPart;
                if (kspCamera != null)
                    kspCamera.Start();
            }
        }

        protected void Update()
        {
            if (isInit) return;
            foreach (Part part in FlightGlobals.ActiveVessel.parts)
            {
                AddDockingCamPart(part);
                
            }
            isInit = true;
        }
    }
}
