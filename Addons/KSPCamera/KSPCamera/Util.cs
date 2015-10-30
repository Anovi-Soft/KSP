using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSPCamera
{
    /// <summary>
    /// Static class of utilities
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Standard path to the folder with the textures
        /// </summary>
        static string dataTexturePath = "OLDD/DockingCam/";

        /// <summary>
        /// Load Texture2D from standard folder
        /// </summary>
        /// <param name="name">Texture name without extension</param>
        /// <returns></returns>
        public static Texture2D LoadTexture(string name)
        {
            return GameDatabase.Instance.GetTexture(dataTexturePath + name, false);
        }

        /// <summary>
        /// Texture generating a specific color
        /// </summary>
        /// <param name="c">Color of texture</param>
        /// <returns></returns>
        public static Texture2D GetTexture(Color c)
        {
            Texture2D texture = new Texture2D(2, 2);
            Color[] colors = new Color[4];
            for (int i = 0; i < 4; i++)
                colors[i] = c;
            texture.SetPixels(colors);
            texture.Apply();
            return texture;
        }
    }
}
