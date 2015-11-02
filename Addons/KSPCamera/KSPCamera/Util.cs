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
        /// Generate rectangle
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Texture2D MonoColorRectTexture(Color color)
        {
            return MonoColorTexture(color, 4, 4);
        }
        /// <summary>
        /// Generate vertical line
        /// </summary>
        /// <param name="color"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Texture2D MonoColorVerticalLineTexture(Color color, int size)
        {
            return MonoColorTexture(color, 1, size);
        }
        /// <summary>
        /// Generate horizontal line
        /// </summary>
        /// <param name="color"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Texture2D MonoColorHorizontalLineTexture(Color color, int size)
        {
            return MonoColorTexture(color, size, 1);
        }

        /// <summary>
        /// Texture generating a specific color
        /// </summary>
        /// <param name="color">Color of texture</param>
        /// <returns></returns>
        public static Texture2D MonoColorTexture(Color color, int width, int height)
        {
            var texture = new Texture2D(width, height);
            for (var i = 0; i < width; i++)
                for (var j = 0; j < height; j++)
                    texture.SetPixel(i, j, color);
            texture.Apply();
            return texture;
        }
    }
}
