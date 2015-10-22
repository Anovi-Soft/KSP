using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSPCam
{
    class CameraModule:PartModule
    {
        [KSPField(guiActive = true, guiActiveEditor = true, guiName = "Camera", isPersistant = true)]
        [UI_Toggle(controlEnabled = true, enabledText = "On", disabledText = "Off", scene = UI_Scene.Flight)]
        public bool IsEnabled=true;

        private bool isInit = false;
        private CameraForDocking camera;

        public override void OnStart(StartState state)
        {
            if (isInit || state == StartState.Editor)
                return;
            camera = new CameraForDocking(this.part, true, false);
            isInit = true;
        }

        public override void OnUpdate()
        {
            if (camera == null) return;

            camera.UpdateTransform();
            if (IsEnabled)
                camera.Activate();
            else
                camera.Deavtivate();
        }
    }
}
