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
        private GameObject self;
        private Part selfPart;
        public float DX;
        public float DY;
        public float DZ;
        public float SpeedX;
        public float SpeedY;
        public float SpeedZ;
        public float AngleX;
        public float AngleY;
        public float AngleZ;

        /// <param name="from">Object of comparison</param>
        public TargetHelper(Part from)
        {
            selfPart = from;
            self = selfPart.gameObject;
            Update();
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
        public void Update()
        {
            DX = targetTransform.position.x - self.transform.position.x;
            DY = targetTransform.position.y - self.transform.position.y;
            DZ = targetTransform.position.z - self.transform.position.z;

            var velocity = Target.GetObtVelocity() - selfPart.vessel.GetObtVelocity();
            SpeedX = (float)velocity.x;
            SpeedY = (float)velocity.y;
            SpeedZ = (float)velocity.z;

            Destenetion = (float)Math.Sqrt(Math.Pow(DX, 2) + Math.Pow(DY, 2) + Math.Pow(DZ, 2));

            AngleX = AngleAroundNormal(targetTransform.forward, self.transform.forward, self.transform.up);
            AngleY = AngleAroundNormal(targetTransform.forward, self.transform.forward, -self.transform.right);
            AngleZ = AngleAroundNormal(-targetTransform.up, self.transform.up, self.transform.forward);
            //var AngleYInverted = false;
            if (Mathf.Abs(AngleX) > 90)
                AngleY = invert(AngleY);
            //AngleYInverted = true;
            if (Mathf.Abs(AngleY) > 90)
            {
                AngleX = invert(AngleX);
                AngleZ = invert(AngleZ);
            }
            //if (AngleYInverted)
            //    AngleY = invert(AngleY);
        }

        private static float invert(float angle)
        {
            var sign = angle > 0 ? 1 : -1;
            return angle - sign * 180;
        }
        public float Destenetion;

        private static float AngleAroundNormal(Vector3 a, Vector3 b, Vector3 up)
        {
            var v1 = Vector3.Cross(up, a);
            var v2 = Vector3.Cross(up, b);
            if (Vector3.Dot(Vector3.Cross(v1, v2), up) < 0)
                return -Vector3.Angle(v1, v2);
            return Vector3.Angle(v1, v2);
        }
    }
}
