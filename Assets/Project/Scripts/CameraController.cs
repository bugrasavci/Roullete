using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform target;

    [Header("Animation Settings")]
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private AnimationCurve easingCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private Coroutine currentAnimation;

    private void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void GoToTarget()
    {
        StartCameraTransition(target.position, target.rotation);
    }

    public void GoToOrigin()
    {
        StartCameraTransition(initialPosition, initialRotation, delay: 0.45f);
    }

    private void StartCameraTransition(Vector3 destination, Quaternion targetRotation, float delay = 0f)
    {
        if (currentAnimation != null)
            StopCoroutine(currentAnimation);

        currentAnimation = StartCoroutine(AnimateCamera(destination, targetRotation, delay));
    }

    private IEnumerator AnimateCamera(Vector3 destination, Quaternion targetRotation, float delay)
    {
        if (delay > 0f)
            yield return new WaitForSeconds(delay);

        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            float t = easingCurve.Evaluate(elapsed / animationDuration);

            transform.position = Vector3.Lerp(startPosition, destination, t);
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t); // smoother rotation

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = destination;
        transform.rotation = targetRotation;

        currentAnimation = null;
    }
}
