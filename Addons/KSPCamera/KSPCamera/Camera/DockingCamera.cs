using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace KSPCamera
{
    class DockingCamera:BaseKspCamera
    {
        private Texture2D textureMask;

        protected override void ExtendedDrawWindowL2()
        {
            base.ExtendedDrawWindowL2();
            GUI.DrawTexture(new Rect(2f, 15f, 256f, 248f), textureMask);
        }
        public DockingCamera(Part part, string windowLabel = "Camera") : base(part, windowLabel)
        {
            textureMask = GameDatabase.Instance.GetTexture("L0dom/textures/dockingcam", false);
        }
    }
}
