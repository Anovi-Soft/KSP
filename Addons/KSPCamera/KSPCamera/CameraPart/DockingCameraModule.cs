using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSPCamera
{
    class DockingCameraModule : PartModule, ICamPart
    {
        /// <summary>
        /// Module adds an external camera and gives control over it
        /// </summary>
        [KSPField(guiActive = true, guiActiveEditor = true, guiName = "Camera", isPersistant = true)]
        [UI_Toggle(controlEnabled = true, enabledText = "On", disabledText = "Off", scene = UI_Scene.All)]
        public bool IsEnabled;
        BaseKspCamera camera;

        public override void OnStart(PartModule.StartState state = StartState.Flying)
        {
            if (state == StartState.Editor || camera != null)
                return;
            Start();
        }
        public void Start()
        {
            if (camera == null)
                camera = new DockingCamera(this.part);
        }
        public override void OnUpdate()
        {
            if (camera == null) 
                return;
            if (camera.IsActivate)
                camera.Update();
            if (IsEnabled)
                Activate();
            else
                Deavtivate();
        }

        public void Activate()
        {
            if (TargetHelper.IsTargetSelect && new TargetHelper(part).Destenetion <= 1000)
                camera.Activate();
            else
            {
                ScreenMessages.PostScreenMessage("You need to set target and be closer than 1000 meters from target", 5f, ScreenMessageStyle.UPPER_CENTER);
                IsEnabled = false;
            }
        }
        public void Deavtivate()
        {
            camera.Deactivate();
        }
    }
}
