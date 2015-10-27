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
        private Material grayscale;
        protected override void InitTexture()
        {
 	        base.InitTexture();
            grayscale = Grayscale();
        }
        protected override void ExtendedDrawWindowL1()
        {
            Graphics.DrawTexture(texturePosition, Render(), grayscale);
        }
        protected override void ExtendedDrawWindowL2()
        {
            base.ExtendedDrawWindowL2();
            GUI.DrawTexture(texturePosition, textureMask);
        }
        protected override void ExtendedDrawWindowL3()
        {

            if (GUI.RepeatButton(new Rect(7, 15, 20, 20), "+"))
            {
                currentZoom -= 0.5f;
                if (currentZoom < minZoom)
                    currentZoom = minZoom;
            }
            if (GUI.RepeatButton(new Rect(28, 15, 20, 20), "-"))
            {
                currentZoom += 0.5f;
                if (currentZoom > maxZoom)
                    currentZoom = maxZoom;
            }
            if (TargetHelper.IsTargetSelect)
            {
                int i = 0;
                var target = new TargetHelper(partGameObject);
                GUI.Label(new Rect(texturePosition.xMax - 60, 20 + i++ * 20, 60, 20), String.Format("dx:{0:f2}", target.DX));
                GUI.Label(new Rect(texturePosition.xMax - 60, 20 + i++ * 20, 60, 20), String.Format("dy:{0:f2}", target.DY));
                GUI.Label(new Rect(texturePosition.xMax - 60, 20 + i++ * 20, 60, 20), String.Format("dz:{0:f2}", target.DZ));
                i++;
                //GUI.Label(new Rect(texturePosition.xMax - 60, 20 + i++ * 20, 60, 20), String.Format("°X:{0:f1}", target.AngleX));
                //GUI.Label(new Rect(texturePosition.xMax - 60, 20 + i++ * 20, 60, 20), String.Format("°Y:{0:f1}", target.AngleY));
                //GUI.Label(new Rect(texturePosition.xMax - 60, 20 + i++ * 20, 60, 20), String.Format("°Z:{0:f1}", target.AngleZ));
                
            }
            base.ExtendedDrawWindowL3();
        }
        public DockingCamera(Part part, string windowLabel = "Camera") : base(part, windowLabel)
        {
            textureMask = Util.LoadTexture("dockingcam");
        }

        public static Material Grayscale()
        {
                var mat = new Material(Shader.Find("Hidden/Grayscale Effect"));
                return mat;
        }
    }
}
