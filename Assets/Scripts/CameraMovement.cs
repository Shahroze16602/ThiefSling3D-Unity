using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    private Vector3 originalPosition;
    private Coroutine currentCoroutine;

    public void MoveRight(float distance, float duration)
    {
        StopCurrentCoroutine();
        currentCoroutine = StartCoroutine(MoveCamera(Vector3.right * distance, duration));
    }

    public void MoveBack(Vector3 targetPosition, float duration)
    {
        StopCurrentCoroutine();
        currentCoroutine = StartCoroutine(MoveCamera(targetPosition, duration));
    }

    private IEnumerator MoveCamera(Vector3 targetOffset, float duration)
    {
        Vector3 startPosition = transform.localPosition;
        Vector3 targetPosition = originalPosition + targetOffset;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPosition;
    }

    private void StopCurrentCoroutine()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
    }

    public void ResetPosition()
    {
        transform.localPosition = originalPosition;
    }

    private void Awake()
    {
        originalPosition = transform.localPosition;
    }
}
