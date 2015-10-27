using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSPCamera
{
    /// <summary>
    /// Extended information about the selected target
    /// </summary>
    public class TargetHelper
    {
        /// <summary>
        /// Object of comparison
        /// </summary>
        private GameObject from;
        
        /// <param name="from">Object of comparison</param>
        public TargetHelper(GameObject from)
        {
            this.from = from;
        }
        public static ITargetable Target
        {
            get
            {
                return FlightGlobals.fetch.VesselTarget;
            }
        }
        private Transform targetTransform
        {
            get
            {
                return Target.GetTransform();
            }
        }
        public static bool IsTargetSelect
        {
            get 
            {
                return Target != null;
            }
        }
        public float Destenetion
        {
            get
            {
                return (float)Math.Sqrt(Math.Pow(DX,2) + Math.Pow(DY,2) + Math.Pow(DZ,2));
            }
        }
        public float DX
        {
            get
            {
                return targetTransform.position.x - from.transform.position.x;
            }
        }
        public float DY
        {
            get
            {
                return targetTransform.position.y - from.transform.position.y;
            }
        }
        public float DZ
        {
            get
            {
                return targetTransform.position.z - from.transform.position.z;
            }
        }
        public float SpeedX
        {
            get
            {
                return 0;
            }
        }
        public float SpeedY
        {
            get
            {
                return 0;
            }
        }
        public float SpeedZ
        {
            get
            {
                return 0;
            }
        }
        public float AngleX
        {
            get
            {
                return targetTransform.rotation.x-from.transform.rotation.x;
            }
        }
        public float AngleY
        {
            get
            {
                return targetTransform.rotation.y - from.transform.rotation.y;
            }
        }
        public float AngleZ
        {
            get
            {
                return targetTransform.rotation.z - from.transform.rotation.z;
            }
        }
    }
}
