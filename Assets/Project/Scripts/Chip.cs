using UnityEngine;
using System.Collections;

public class Chip : MonoBehaviour
{
    [SerializeField] private float value;
    [SerializeField] private GameObject ring;

    private Coroutine scaleRoutine;
    private Vector3 normalScale;
    private Vector3 hoverScale = Vector3.one * 1.2f;

    public float GetValue => value;

    private void Awake()
    {
        normalScale = transform.localScale;

        if (ring != null)
            ring.SetActive(false);
    }

    public void OnClick()
    {
        if (!BetSpace.BetsEnabled)
            return;

        if (ChipManager.Instance.selected != null && ChipManager.Instance.selected != this)
        {
            AudioManager.Instance?.PlaySound(3);
            ChipManager.Instance.selected.ResetVisual();
        }

        Select();
        StartShakeAnimation();
    }

    private void Select()
    {
        ChipManager.Instance.selected = this;

        if (ring != null)
            ring.SetActive(true);
    }

    private void ResetVisual()
    {
        StopCurrentAnimation();
        StartCoroutine(ScaleTo(normalScale, 0.2f));

        if (ring != null)
            ring.SetActive(false);
    }


    public void OnPointEnter()
    {
        if (!BetSpace.BetsEnabled)
            return;

        StopCurrentAnimation();
        scaleRoutine = StartCoroutine(ScaleTo(hoverScale, 0.3f));
    }

    public void OnPointExit()
    {
        if (!BetSpace.BetsEnabled)
            return;

        StopCurrentAnimation();
        scaleRoutine = StartCoroutine(ScaleTo(normalScale, 0.2f));
    }

    private void StartShakeAnimation()
    {
        StopCurrentAnimation();
        scaleRoutine = StartCoroutine(ShakeScale());
    }

    private IEnumerator ScaleTo(Vector3 targetScale, float duration)
    {
        Vector3 start = transform.localScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.localScale = Vector3.Lerp(start, targetScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }

    private IEnumerator ShakeScale()
    {
        Vector3 original = normalScale;
        float time = 0.3f;
        float strength = 0.2f;
        float frequency = 20f;
        float elapsed = 0f;

        while (elapsed < time)
        {
            float shake = Mathf.Sin(elapsed * frequency) * strength;
            transform.localScale = original + Vector3.one * shake;
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = original;
    }

    private void StopCurrentAnimation()
    {
        if (scaleRoutine != null)
        {
            StopCoroutine(scaleRoutine);
            scaleRoutine = null;
        }
    }
}
