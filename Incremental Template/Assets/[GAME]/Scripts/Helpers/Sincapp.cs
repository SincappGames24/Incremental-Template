using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        
        public static class AbbrevationUtility
        {
            private static readonly SortedDictionary<float, string> abbrevations = new SortedDictionary<float, string>
            {
                {1000, "K"},
                {1000000, "M"},
                {1000000000, "B"},
                {1000000000000, "T"}
            };

            public static string AbbreviateNumber(float number)
            {
                for (int i = abbrevations.Count - 1; i >= 0; i--)
                {
                    KeyValuePair<float, string> pair = abbrevations.ElementAt(i);

                    if (Mathf.Abs(number) >= pair.Key)
                    {
                        float roundedNumber = number / pair.Key;
                        return roundedNumber.ToString("F1") + pair.Value;
                    }
                }

                return number.ToString("F0");
            }
        }
    }
}