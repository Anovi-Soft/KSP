using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSPCamera
{
    /// <summary>
    /// Module adds an external camera and gives control over it
    /// </summary>
    class CameraModule : PartModule, ICamPart
    {
        [KSPField(guiActive = true, guiActiveEditor = true, guiName = "Camera", isPersistant = true)]
        [UI_Toggle(controlEnabled = true, enabledText = "On", disabledText = "Off", scene = UI_Scene.All)]
        public bool IsEnabled;

        PersonalCamera camera;
        
        public override void OnStart(StartState state = StartState.Flying)
        {
            if (state == StartState.Editor || camera != null)
                return;
            Start();
        }
        public void Start()
        {
            if (camera == null)
                camera = new PersonalCamera(this.part);
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
            camera.Activate();
        }
        public void Deavtivate()
        {
            camera.Deavtivate();
            
        }
        
    }
}
