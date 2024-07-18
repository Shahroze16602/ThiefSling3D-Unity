using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerScript : MonoBehaviour
{
    [SerializeField] private Slider timerSlider;
    private float timerDuration = 1f;
    private float currentTime;
    private Coroutine timerCoroutine;

    private void Start()
    {
        if (timerSlider == null)
        {
            Debug.LogError("Timer Slider is not assigned.");
            return;
        }

        timerSlider.gameObject.SetActive(false);
        timerSlider.maxValue = timerDuration;
        timerSlider.value = timerDuration;
    }

    public void SetTimerDuration(float duration)
    {
        timerDuration = duration;
    }

    public void StartTimer()
    {
        timerSlider.gameObject.SetActive(true);
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        timerCoroutine = StartCoroutine(RunTimer());
    }

    public void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
        timerSlider.gameObject.SetActive(false);
    }

    public void ResetTimer()
    {
        StopTimer();
        currentTime = timerDuration;
        UpdateUI();
    }

    private IEnumerator RunTimer()
    {
        currentTime = timerDuration;

        while (currentTime > 0)
        {
            UpdateUI();
            yield return null;
            currentTime -= Time.deltaTime;
        }

        currentTime = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        timerSlider.value = currentTime;
    }
}
