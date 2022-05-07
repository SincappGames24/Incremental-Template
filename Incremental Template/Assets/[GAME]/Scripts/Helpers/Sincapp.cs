using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SincappStudio
{
    public static class Sincapp
    {
        public static void LookAtSmoothly(this Transform transform, GameObject target, float smoothness)
        {
            Vector3 direction = target.transform.position - transform.position;
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), smoothness);
        }

        public static void SetX_Pos(this Transform transform, float value, Space spaceSelforWorld = Space.World)
        {
            if (spaceSelforWorld == Space.World)
            {
                var position = transform.position;
                position = new Vector3(value, position.y, position.z);
                transform.position = position;
            }
            else
            {
                var position = transform.localPosition;
                position = new Vector3(value, position.y, position.z);
                transform.localPosition = position;
            }
        }

        public static void SetY_Pos(this Transform transform, float value, Space spaceSelforWorld = Space.World)
        {
            if (spaceSelforWorld == Space.World)
            {
                var position = transform.position;
                position = new Vector3(position.x, value, position.z);
                transform.position = position;
            }
            else
            {
                var position = transform.localPosition;
                position = new Vector3(position.x, value, position.z);
                transform.localPosition = position;
            }
        }

        public static void SetZ_Pos(this Transform transform, float value, Space spaceSelforWorld = Space.World)
        {
            if (spaceSelforWorld == Space.World)
            {
                var position = transform.position;
                position = new Vector3(position.x, position.y, value);
                transform.position = position;
            }
            else
            {
                var position = transform.localPosition;
                position = new Vector3(position.x, position.y, value);
                transform.localPosition = position;
            }
        }
        public static IEnumerator WaitAndAction(float delayTime, UnityAction action)
        {
            yield return new WaitForSeconds(delayTime);
            action?.Invoke();
        }
    }
}