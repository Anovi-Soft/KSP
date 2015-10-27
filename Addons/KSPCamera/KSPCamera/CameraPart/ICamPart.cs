using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSPCamera
{
    interface ICamPart
    {
        /// <summary>
        /// Activate camera
        /// </summary>
        void Activate();
        /// <summary>
        /// Deactivate camera
        /// </summary>
        void Deavtivate();
        /// <summary>
        /// Adding a camera
        /// </summary>
        void Start();
    }
}
