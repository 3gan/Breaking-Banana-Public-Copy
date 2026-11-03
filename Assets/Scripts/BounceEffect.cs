using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceEffect : MonoBehaviour
{
    public float bounceHeight = 0.3f;
    public float bounceDuration = 0.4f;
    public int bounceCount = 2;

    public void StartBounce()
    {
        StartCoroutine(BounceHandler());
    }

    private IEnumerator BounceHandler()
    {
        Vector3 startPosition = transform.position;
        float localHeigth = bounceHeight;
        float localDuration = bounceDuration;

        for (int i = 0; i < bounceCount; i++)
        {
            yield return Bounce(transform, startPosition, localHeigth, localDuration/2);
            localHeigth *= 0.5f;
            localDuration *= 0.8f;
        }

        transform.position = startPosition;

    }

    private IEnumerator Bounce(Transform objectTransformaton, Vector3 start, float height, float duration)
    {
        Vector3 peak = start + Vector3.up * height;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            objectTransformaton.position = Vector3.Lerp(peak, start, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;

        while (elapsed < duration)
        {
            objectTransformaton.position = Vector3.Lerp(peak, start, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
