using FixMath.NET;
using UnityEngine;

namespace ConversionHelper
{
    /// <summary>
    /// Helps convert between XNA math types and the BEPUphysics replacement math types.
    /// A version of this converter could be created for other platforms to ease the integration of the engine.
    /// </summary>
    public static class MathConverter
    {
        //Vector2
        public static Vector2 Convert(BEPUutilities.Vector2 bepuVector)
        {
            Vector2 toReturn;
            toReturn.x = (float)bepuVector.X;
            toReturn.y = (float)bepuVector.Y;
            return toReturn;
        }

        public static void Convert(ref BEPUutilities.Vector2 bepuVector, out Vector2 xnaVector)
        {
            xnaVector.x = (float)bepuVector.X;
            xnaVector.y = (float)bepuVector.Y;
        }

        public static BEPUutilities.Vector2 Convert(Vector2 xnaVector)
        {
            BEPUutilities.Vector2 toReturn;
            toReturn.X = (Fix64)xnaVector.x;
            toReturn.Y = (Fix64)xnaVector.y;
            return toReturn;
        }

        public static void Convert(ref Vector2 xnaVector, out BEPUutilities.Vector2 bepuVector)
        {
            bepuVector.X = (Fix64)xnaVector.x;
            bepuVector.Y = (Fix64)xnaVector.y;
        }

        //Vector3
        public static Vector3 Convert(BEPUutilities.Vector3 bepuVector)
        {
            Vector3 toReturn;
            toReturn.x = (float)bepuVector.X;
            toReturn.y = (float)bepuVector.Y;
            toReturn.z = (float)bepuVector.Z;
            return toReturn;
        }

        public static void Convert(ref BEPUutilities.Vector3 bepuVector, out Vector3 xnaVector)
        {
            xnaVector.x = (float)bepuVector.X;
            xnaVector.y = (float)bepuVector.Y;
            xnaVector.z = (float)bepuVector.Z;
        }

        public static BEPUutilities.Vector3 Convert(Vector3 xnaVector)
        {
            BEPUutilities.Vector3 toReturn;
            toReturn.X = (Fix64)xnaVector.x;
            toReturn.Y = (Fix64)xnaVector.y;
            toReturn.Z = (Fix64)xnaVector.z;
            return toReturn;
        }

        public static void Convert(ref Vector3 xnaVector, out BEPUutilities.Vector3 bepuVector)
        {
            bepuVector.X = (Fix64)xnaVector.x;
            bepuVector.Y = (Fix64)xnaVector.y;
            bepuVector.Z = (Fix64)xnaVector.z;
        }

        public static Vector3[] Convert(BEPUutilities.Vector3[] bepuVectors)
        {
            Vector3[] xnaVectors = new Vector3[bepuVectors.Length];
            for (int i = 0; i < bepuVectors.Length; i++)
            {
                Convert(ref bepuVectors[i], out xnaVectors[i]);
            }
            return xnaVectors;

        }

        public static BEPUutilities.Vector3[] Convert(Vector3[] xnaVectors)
        {
            var bepuVectors = new BEPUutilities.Vector3[xnaVectors.Length];
            for (int i = 0; i < xnaVectors.Length; i++)
            {
                Convert(ref xnaVectors[i], out bepuVectors[i]);
            }
            return bepuVectors;

        }
    }
}
