using UnityEngine;
using System.Collections;

public class SlowMotionHandler : MonoBehaviour
{
    [SerializeField] private float slowMoTimeInSeconds = 1f;
    [SerializeField] private float slowMoFactor = 3f;
    [SerializeField] private TimerScript timerScript;
    [SerializeField] private TrajectoryLineScript trajectoryLineScript;
    private ISlowMotionCallBacks slowMotionCallBacks;

    public bool IsInSlowMo { get; private set; } = false;

    private void Start()
    {
        timerScript.SetTimerDuration(slowMoTimeInSeconds);
    }

    public void SetSlowMotionCallbacks(ISlowMotionCallBacks slowMotionCallBacks)
    {
        this.slowMotionCallBacks = slowMotionCallBacks;
    }

    public void StartSlowMotion()
    {
        slowMotionCallBacks.OnSlowMotionStart();
        StartCoroutine(SlowMoTimer());
    }

    public void StopSlowMo()
    {
        IsInSlowMo = false;
        Time.timeScale = 1f;
        timerScript.ResetTimer();
        trajectoryLineScript.HideTrajectoryLine();
        slowMotionCallBacks.OnSlowMotionEnd();
    }

    private IEnumerator SlowMoTimer()
    {
        if (IsInSlowMo)
        {
            timerScript.ResetTimer();
        }
        IsInSlowMo = true;
        Time.timeScale = 1f / slowMoFactor;
        timerScript.StartTimer();
        yield return new WaitForSeconds(slowMoTimeInSeconds);
        if (IsInSlowMo)
        {
            StopSlowMo();
        }
    }
}

public interface ISlowMotionCallBacks
{
    void OnSlowMotionStart();
    void OnSlowMotionEnd();
}