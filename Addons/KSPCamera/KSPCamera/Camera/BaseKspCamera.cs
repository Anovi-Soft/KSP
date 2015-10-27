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
        protected Rect texturePosition;
        protected int windowId;
        protected GameObject partGameObject;
        protected string windowLabel;
        protected Texture bg;

        protected int rotateX = 0;
        protected int rotateY = 0;
        protected int rotateZ = 0;

        protected float minZoom = 10;
        protected float maxZoom = 80;
        protected float currentZoom = 40;

        protected float windowSize = 260f;
        protected float windowSizeCoef = 1f;

        public bool IsActivate = false;

        protected List<Camera> allCameras = new List<Camera>();
        protected List<GameObject> allCamerasGameObject = new List<GameObject>();
        protected List<string> cameraNames = new List<string>{"GalaxyCamera", "Camera ScaledSpace", "Camera 01", "Camera 00" };

        public BaseKspCamera(Part part, string windowLabel = "Camera")
        {
            this.windowLabel = windowLabel;
            bg = Util.GetTexture(new Color(0.45f, 0.45f, 0.45f, 1));
            partGameObject = part.gameObject;
            InitWindow();
            InitTexture();
            windowCount++;
        }
        /// <summary>
        /// Initializes window
        /// </summary>
        protected virtual void InitWindow()
        {
            windowId = UnityEngine.Random.Range(1000, 10000);
            if (windowPosition == null)
            {
                windowPosition = GUIUtil.ScreenCenteredRect(windowSize, windowSize + 10f);
                windowPosition = new Rect(windowPosition.xMin + 10f * windowCount,
                    windowPosition.yMin + 10f * windowCount,
                    windowPosition.width,
                    windowPosition.height);
            }
            else
            {
                windowPosition.width = windowSize * windowSizeCoef;
                windowPosition.height = windowSize * windowSizeCoef + 10f;
            }
        }
        /// <summary>
        /// Initializes texture
        /// </summary>
        protected virtual void InitTexture()
        {

            texturePosition = new Rect(7f, 15f, windowPosition.width - 14f, windowPosition.height - 24);
            renderTexture = new RenderTexture(512, 512, 24, RenderTextureFormat.RGB565);
            RenderTexture.active = renderTexture;
            renderTexture.Create();
        }
        /// <summary>
        /// Initializes camera
        /// </summary>
        protected virtual void InitCameras()
        {
            allCamerasGameObject = cameraNames.Select(a => new GameObject()).ToList();
            allCameras = allCamerasGameObject.Select((go, i) =>
                {
                    var camera = go.AddComponent<Camera>();
                    var cameraExample = Camera.allCameras.FirstOrDefault(cam => cam.name == cameraNames[i]);
                    if (cameraExample != null)
                    {
                        camera.CopyFrom(cameraExample);
                        camera.name = string.Format("{1} copy of {0}", cameraNames[i], windowCount);
                        camera.targetTexture = renderTexture;
                    }
                    return camera;
                }).ToList();
        }
        /// <summary>
        /// Destroy Cameras
        /// </summary>
        protected virtual void DestroyCameras()
        {
            allCameras.ForEach(Camera.Destroy);
            allCameras = new List<Camera>();
        }
        /// <summary>
        /// Create and activate cameras
        /// </summary>
        public virtual void Activate()
        {
            if (IsActivate) return;
            InitCameras();
            RenderingManager.AddToPostDrawQueue(0, CamGui);
            IsActivate = true;
        }
        /// <summary>
        /// Destroy  cameras
        /// </summary>
        public virtual void Deavtivate()
        {
            if (!IsActivate) return;
            DestroyCameras();
            RenderingManager.RemoveFromPostDrawQueue(0, CamGui);
            IsActivate = false;
        }
        private void CamGui()
        {
            windowPosition = GUI.Window(windowId, windowPosition, DrawWindow, windowLabel);
        }
        /// <summary>
        /// drawing method
        /// </summary>
        private void DrawWindow(int id)
        {
            Graphics.DrawTexture(texturePosition, bg);
            ExtendedDrawWindowL1();
            ExtendedDrawWindowL2();
            ExtendedDrawWindowL3();
            GUI.DragWindow();
        }

        /// <summary>
        /// drawing method, first layer
        /// </summary>
        protected virtual void ExtendedDrawWindowL1()
        {
            Graphics.DrawTexture(texturePosition, Render());
        }

        /// <summary>
        /// drawing method, second layer
        /// </summary>
        protected virtual void ExtendedDrawWindowL2()
        {

        }

        /// <summary>
        /// drawing method, third layer
        /// </summary>
        protected virtual void ExtendedDrawWindowL3()
        {
            if (GUI.RepeatButton(new Rect(texturePosition.xMax - 10, texturePosition.yMax - 10, 9, 9), " "))
            {
                windowSizeCoef = windowSizeCoef == 1f ? 2f : 1f;
                Deavtivate();
                InitWindow();
                InitTexture();
                Activate();

            }
        }
        /// <summary>
        /// render texture camera
        /// </summary>
        protected virtual RenderTexture Render()
        {
            allCameras.ForEach(a => a.Render());
            return renderTexture;
        }

        /// <summary>
        /// Update position and rotation of the cameras
        /// </summary>
        public virtual void Update()
        {
            allCamerasGameObject.Last().transform.position = partGameObject.transform.position;
            allCamerasGameObject.Last().transform.rotation = partGameObject.transform.rotation;
            allCamerasGameObject.Last().transform.Rotate(new Vector3(-1f, 0, 0), 90);

            allCamerasGameObject.Last().transform.Rotate(new Vector3(0, 1f, 0), rotateY);
            allCamerasGameObject.Last().transform.Rotate(new Vector3(1f, 0, 0), rotateX);
            allCamerasGameObject.Last().transform.Rotate(new Vector3(0, 0, 1f), rotateZ);

            allCamerasGameObject[0].transform.rotation = allCamerasGameObject.Last().transform.rotation;
            allCamerasGameObject[1].transform.rotation = allCamerasGameObject.Last().transform.rotation;
            allCamerasGameObject[2].transform.rotation = allCamerasGameObject.Last().transform.rotation;
            allCamerasGameObject[2].transform.position = allCamerasGameObject.Last().transform.position;
            allCameras.ForEach(cam => cam.fieldOfView = currentZoom);
        }
    }
    
}
