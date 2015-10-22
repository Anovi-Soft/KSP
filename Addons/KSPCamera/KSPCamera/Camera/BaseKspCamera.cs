using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSPCamera
{
    public class BaseKspCamera
    {
        protected static int windowCount = 0;
        protected RenderTexture renderTexture;
        protected Rect windowPosition;
        protected int windowId;
        protected GameObject cameraGameObject;
        protected GameObject partGameObject;
        protected Camera camera;
        protected string windowLabel;

        protected int rotateX = 0;
        protected int rotateY = 0;
        protected int rotateZ = 0;

        protected float minZoom = 10;
        protected float maxZoom = 80;

        protected bool isActivate = false;

        public BaseKspCamera(Part part, string windowLabel = "Camera")
        {
            this.windowLabel = windowLabel;
            partGameObject = part.gameObject;
            InitWindow();
            InitTexture();
            InitCamera();
        }
        protected virtual void InitWindow()
        {
            windowId = UnityEngine.Random.Range(1000, 10000);
            windowPosition = GUIUtil.ScreenCenteredRect(260f, 270f);
            windowPosition = new Rect(windowPosition.xMin + 10f * windowCount,
                windowPosition.yMin + 10f * windowCount,
                windowPosition.width,
                windowPosition.height);
            windowCount++;
        }
        protected virtual void InitTexture()
        {
            renderTexture = new RenderTexture(256, 256, 24, RenderTextureFormat.RGB565);
            RenderTexture.active = renderTexture;
            renderTexture.Create();
        }
        protected virtual void InitCamera()
        {
            cameraGameObject = new GameObject();
            camera = cameraGameObject.AddComponent<Camera>();
            camera.CopyFrom(Camera.main);
            camera.cullingMask = 557059;
            camera.farClipPlane = 1000000f;
            camera.fieldOfView = 40f;
            camera.targetTexture = renderTexture;
            camera.depth = 3;
            camera.enabled = false;
            camera.clearFlags = CameraClearFlags.Skybox;
        }
        public virtual void Activate()
        {
            if (isActivate) return;
            RenderingManager.AddToPostDrawQueue(0, CamGui);
            isActivate = true;
        }
        public virtual void Deavtivate()
        {
            if (!isActivate) return;
            RenderingManager.RemoveFromPostDrawQueue(0, CamGui);
            isActivate = false;
        }
        private void CamGui()
        {
            windowPosition = GUI.Window(windowId, windowPosition, DrawWindow, windowLabel);
        }

        private void DrawWindow(int id)
        {
            ExtendedDrawWindowL1();
            Graphics.DrawTexture(new Rect(2f, 15f, 256f, 248f), Render());
            ExtendedDrawWindowL2();

            ExtendedDrawWindowL3();
            GUI.DragWindow();
        }

        protected virtual void ExtendedDrawWindowL1()
        {

        }
        protected virtual void ExtendedDrawWindowL2()
        {

        }
        protected virtual void ExtendedDrawWindowL3()
        {

        }

        protected virtual RenderTexture Render()
        {
            camera.Render();
            return renderTexture;
        }

        public virtual void Update()
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
