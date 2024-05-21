using UnityEngine;

public class Timer
{
    private float _time;
    private float _waitingTime = 0;

    public Timer(float timeWait)
    {
        _time = timeWait;
    }

    public bool IsTimeUp => _waitingTime <= 0;

    public void MakeCountdown()
    {
        if (IsTimeUp == false)
            _waitingTime -= Time.deltaTime;
    }

    public void UpdateWaitingTime()
    {
        _waitingTime = _time;
    }

    public void SetWaitTime(float timeWait)
    {
        _time = timeWait;
    }

    public void SetRandomWaitTime(float minTime, float maxTime)
    {
        _time = Random.Range(minTime, maxTime);
    }
}
