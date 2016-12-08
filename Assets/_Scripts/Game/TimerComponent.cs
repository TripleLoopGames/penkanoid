using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerComponent : MonoBehaviour
{

    public TimerComponent StartTimer(int time, Action<int> onSecond, Action onEnd)
    {
        if(this.coroutine != null)
        {
            StopCoroutine(this.coroutine);
            this.coroutine = null;
        }      
        this.coroutine = WaitAndExecuteEverySecond(time, onSecond, onEnd);
        StartCoroutine(this.coroutine);
        return this;
    }

    public TimerComponent Reset()
    {
        if (this.coroutine != null)
        {
            StopCoroutine(this.coroutine);
            this.coroutine = null;
        }
        return this;
    }

    private IEnumerator WaitAndExecuteEverySecond(int time, Action<int> onSecond, Action onEnd)
    {
        int count = time;
        while (count > 0)
        {
            onSecond(count);
            yield return new WaitForSeconds(1f);
            count--;
        }
        onSecond(count);
        onEnd();
        this.coroutine = null;
    }

    private IEnumerator coroutine;
}