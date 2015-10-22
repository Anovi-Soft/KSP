using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSPCamera
{
    class PersonalCamera:BaseKspCamera
    {
        public PersonalCamera(Part part, string windowLabel = "Camera")
            : base(part, windowLabel)
        {
        }
        protected override void ExtendedDrawWindowL3()
        {
            base.ExtendedDrawWindowL3();
            if (GUI.RepeatButton(new Rect(5, 20, 25, 25), " "))
            {
                rotateZ += 1;
            }
            if (GUI.RepeatButton(new Rect(55, 20, 25, 25), " "))
            {
                rotateZ -= 1;
            }
            if (GUI.RepeatButton(new Rect(30, 20, 25, 25), "↑"))
            {
                rotateX -= 1;
            }
            if (GUI.RepeatButton(new Rect(30, 45, 25, 25), "↓"))
            {
                rotateX += 1;
            }
            if (GUI.RepeatButton(new Rect(5, 45, 25, 25), "←"))
            {
                rotateY -= 1;
            }
            if (GUI.RepeatButton(new Rect(55, 45, 25, 25), "→"))
            {
                rotateY += 1;

            }
            if (GUI.RepeatButton(new Rect(5, 70, 25, 25), "+"))
            {
                camera.fieldOfView -= 0.5f;
                if (camera.fieldOfView < minZoom)
                {
                    camera.fieldOfView = minZoom;
                }
            }
            if (GUI.RepeatButton(new Rect(30, 70, 25, 25), "-"))
            {
                camera.fieldOfView += 0.5f;
                if (camera.fieldOfView > maxZoom)
                {
                    camera.fieldOfView = maxZoom;
                }
            }
            if (GUI.RepeatButton(new Rect(55, 70, 25, 25), "0"))
            {
                camera.fieldOfView = 40f;
                rotateX = 0;
                rotateY = 0;
                rotateZ = 0;
            }
        }
    }
}
