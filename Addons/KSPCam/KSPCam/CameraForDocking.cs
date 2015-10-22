using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSPCam
{
    public class CameraForDocking
    {
        private RenderTexture texture;
        private Rect position;
        private GameObject cameraGameObject;
        private GameObject partGameObject;
        private Camera camera;

        private bool controlButtons;
        private bool information;
        private bool greyScael;


        private bool isActivate = false;
        private int windowId;
        private int windowIdForName;
        private static int windowCount = 0;

        private int rotateX = 0;
        private int rotateY = 0;
        private int rotateZ = 0;

        private float minZoom = 10;
        private float maxZoom = 80;

        public CameraForDocking(Part part, bool controlButtons = false, bool information = false, bool greyScael = false)
        {
            GameObject gameObject = part.gameObject;

            this.controlButtons = controlButtons;
            this.information = information;
            this.greyScael = greyScael;
            this.partGameObject = gameObject;

            windowId = UnityEngine.Random.Range(1000, 10000);

            position = GUIUtil.ScreenCenteredRect(260f, 270f);
            position = new Rect(position.xMin + 10f * windowCount,
                position.yMin + 10f * windowCount,
                position.width,
                position.height);

            windowCount++;
            windowIdForName = windowCount;
            texture = new RenderTexture(256, 256, 24, RenderTextureFormat.RGB565);
            RenderTexture.active = texture;
            texture.Create();

            cameraGameObject = new GameObject();
            camera = cameraGameObject.AddComponent<Camera>();
            camera.CopyFrom(Camera.main);
            camera.cullingMask = 557059;
            camera.farClipPlane = 1000000f;
            camera.fieldOfView = 40f;
            camera.targetTexture = texture;
            camera.depth = 3;
            camera.enabled = false;
            camera.clearFlags = CameraClearFlags.Skybox;
            
            UpdateTransform();
            Activate();
        }


        public void Activate()
        {
            if (isActivate) return;

            camera.enabled = true;
            RenderingManager.AddToPostDrawQueue(0, CamGui);
            isActivate = true;
        }
        public void Deavtivate()
        {
            if (!isActivate) return;

            camera.enabled = false;
            RenderingManager.RemoveFromPostDrawQueue(0, CamGui);
            isActivate = false;
        }
        private void CamGui()
        {
            position = GUI.Window(windowId, position, DrawWindow, "Camera " + windowIdForName);
        }
        

        private void DrawWindow(int id)
        {
            if (camera != null)
                camera.Render();
            Graphics.DrawTexture(new Rect(2f, 15f, 256f, 248f), texture);

            if (controlButtons && GUI.RepeatButton(new Rect(5, 20, 25, 25), " "))
            {
                rotateZ += 1;
            }
            if (controlButtons && GUI.RepeatButton(new Rect(55, 20, 25, 25), " "))
            {
                rotateZ -= 1;
            }
            if (controlButtons && GUI.RepeatButton(new Rect(30, 20, 25, 25), "↑"))
            {
                rotateX -= 1;
            }
            if (controlButtons && GUI.RepeatButton(new Rect(30, 45, 25, 25), "↓"))
            {
                rotateX += 1;
            }
            if (controlButtons && GUI.RepeatButton(new Rect(5, 45, 25, 25), "←"))
            {
                rotateY -= 1;
            }
            if (controlButtons && GUI.RepeatButton(new Rect(55, 45, 25, 25), "→"))
            {
                rotateY += 1;

            }
            if (controlButtons && GUI.RepeatButton(new Rect(5, 70, 25, 25), "+"))
            {
                camera.fieldOfView -= 0.5f;
                if (camera.fieldOfView < minZoom)
                {
                    camera.fieldOfView = minZoom;
                }
            }
            if (controlButtons && GUI.RepeatButton(new Rect(30, 70, 25, 25), "-"))
            {
                camera.fieldOfView += 0.5f;
                if (camera.fieldOfView > maxZoom)
                {
                    camera.fieldOfView = maxZoom;
                }
            }
            if (controlButtons && GUI.RepeatButton(new Rect(55, 70, 25, 25), "0"))
            {
                camera.fieldOfView = 40f;
                rotateX = 0;
                rotateY = 0;
                rotateZ = 0;
            }
            //////////////////////////////////////////////////////////////////
            if (GUI.RepeatButton(new Rect(55, 20, 25, 25), " "))
            {
                camera.cullingMask = tmpShift = 0;
            }
            if (GUI.RepeatButton(new Rect(30, 20, 25, 25), "↑"))
            {
                camera.cullingMask = (1 << ++tmpShift);
            }
            if (GUI.RepeatButton(new Rect(30, 45, 25, 25), "↓"))
            {
                camera.cullingMask = (1 << --tmpShift);
            }
            ////////////////////////////////////////////////////////////////////
            GUI.DragWindow();
        }
        int tmpShift=0;
        public void UpdateTransform()
        {
            cameraGameObject.transform.position = partGameObject.transform.position;
            cameraGameObject.transform.rotation = partGameObject.transform.rotation;
            cameraGameObject.transform.Rotate(new Vector3(-1f, 0, 0), 90);

            cameraGameObject.transform.Rotate(new Vector3(0, 1f, 0), rotateY);
            cameraGameObject.transform.Rotate(new Vector3(1f, 0, 0), rotateX);
            cameraGameObject.transform.Rotate(new Vector3(0, 0, 1f), rotateZ);
        }
    }
}
