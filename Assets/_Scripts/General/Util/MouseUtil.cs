using System.Numerics;
using UnityEngine;
using Plane = UnityEngine.Plane;
using Vector3 = UnityEngine.Vector3;

namespace _Scripts.General.Util
{
    public class MouseUtil
    {
        private static Camera camera = Camera.main;

        public static Vector3 GetMousePositionInWorldSpace(float zValue = 0f)
        {
            Plane dragPlane = new(camera.transform.position, new Vector3(0, 0, zValue));
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (dragPlane.Raycast(ray, out float distance))
            {
                return ray.GetPoint(distance);
            }
            return Vector3.zero;
        }
    }
}