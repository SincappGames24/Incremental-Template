using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


    public class CameraShake : MonoBehaviour
    {
        private void OnEnable()
        {
            EventManager.CameraShake += DoShake;
        }

        private void OnDisable()
        {
            EventManager.CameraShake -= DoShake;
        }

        private void DoShake(float shakeAmount) => StartCoroutine(Shake(.15f, .3f));

        public IEnumerator Shake(float duration, float magnitude)
        {
            Vector3 originalPos = transform.localPosition;
            float elapsed = 0;

            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                transform.localPosition = new Vector3(x, y, originalPos.z);

                elapsed += Time.deltaTime;

                yield return null;
            }

            transform.localPosition = originalPos;
        }
    }
