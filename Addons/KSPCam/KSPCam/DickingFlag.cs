using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSPCam
{
    class DickingFlag:PartModule
    {
        [KSPField(guiActive = true, guiActiveEditor = true, guiName = "Camera", isPersistant = true)]
        [UI_Toggle(controlEnabled = true, enabledText = "On", disabledText = "Off", scene = UI_Scene.All)]
        public bool IsEnabled;

        private bool isInit = false;
        private CameraForDocking camera;

        public override void OnStart(StartState state)
        {
            if (isInit || state == StartState.Editor)
                return;
            camera = new CameraForDocking(this.part, false, false);
            isInit = true;
        }

        public override void OnUpdate()
        {
            if (camera == null && IsEnabled)
            {
                camera = new CameraForDocking(this.part, false, false);
                isInit = true;
            }
            if (camera == null) return ;
            camera.UpdateTransform();
            if (IsEnabled)
                camera.Activate();
            else
                camera.Deavtivate();
        }
    }
}
