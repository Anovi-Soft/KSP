using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;


namespace KSPCamera
{
    class DockingCamera:BaseKspCamera
    {
        private HashSet<int> usedId = new HashSet<int>();
        private int ID;
        private Texture2D textureMask;
        public DockingCamera(Part part, string windowLabel = "Docking Camera")
            : base(part, windowLabel)
        {

            textureMask = Util.LoadTexture("dockingcam");
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
                var target = new TargetHelper(part);
                GUI.Label(new Rect(texturePosition.xMax - 70, 20 + i++ * 20, 70, 20), String.Format("dx:{0:f2}", target.DX));
                GUI.Label(new Rect(texturePosition.xMax - 70, 20 + i++ * 20, 70, 20), String.Format("dy:{0:f2}", target.DY));
                GUI.Label(new Rect(texturePosition.xMax - 70, 20 + i++ * 20, 70, 20), String.Format("dz:{0:f2}", target.DZ));
                i++;
                GUI.Label(new Rect(texturePosition.xMax - 70, 20 + i++ * 20, 70, 20), String.Format("°X:{0:f1}°", target.AngleX));
                GUI.Label(new Rect(texturePosition.xMax - 70, 20 + i++ * 20, 70, 20), String.Format("°Y:{0:f1}°", target.AngleY));
                GUI.Label(new Rect(texturePosition.xMax - 70, 20 + i++ * 20, 70, 20), String.Format("°Z:{0:f1}°", target.AngleZ));
                i++;
                GUI.Label(new Rect(texturePosition.xMax - 70, 20 + i++ * 20, 70, 20), String.Format("vX:{0:f1}", target.SpeedX));
                GUI.Label(new Rect(texturePosition.xMax - 70, 20 + i++ * 20, 70, 20), String.Format("vY:{0:f1}", target.SpeedY));
                GUI.Label(new Rect(texturePosition.xMax - 70, 20 + i++ * 20, 70, 20), String.Format("vZ:{0:f1}", target.SpeedZ));
                
            }
            base.ExtendedDrawWindowL3();
        }
        
        public static Material Grayscale()
        {

            //var reader = new StreamReader(KSPUtil.ApplicationRootPath.Replace(@"\", "/") + "/GameData/OLDD/DockingCam/NightVision.shader");
            //var shader = reader.ReadToEnd();
            //reader.Close();
            //var mat = new Material(shader);

            //mat.SetTexture("_Overlay1Tex", Util.LoadTexture("NVMesh"));
            //mat.SetTexture("_Overlay2Tex", Util.LoadTexture("Noise"));

            //mat.SetFloat("_Monochrome", 1);
            //mat.SetColor("_MonoColor", new Color(0, .5f, 0, 1));
            //mat.SetFloat("_Contrast", 4f);
            //mat.SetFloat("_Brightness", 0.41f);

            //mat.SetFloat("_Overlay1Amount", .86f);
            //mat.SetFloat("_Overlay2Amount", .86f);

            //return mat;
            var lst = Resources.FindObjectsOfTypeAll<Shader>();
            var shader = Shader.Find("Hidden/Grayscale Effect");
            var mat = new Material(shader);
            return mat;
        }

        public override void Activate()
        {

            if (IsActivate) return;
            SetFreeId();
            base.Activate();
        }

        public override void Deactivate()
        {
            if (!IsActivate) return;
            usedId.Remove(ID);
            base.Deactivate();
        }

        private void SetFreeId()
        {
            for (int i = 1; i < int.MaxValue; i++)
            {
                if (!usedId.Contains(i))
                {
                    ID = i;
                    windowLabel = subWindowLabel + " " + ID + " " + TargetHelper.Target.GetName();
                    usedId.Add(i);
                    return;
                }
            }
        }
    }
}
