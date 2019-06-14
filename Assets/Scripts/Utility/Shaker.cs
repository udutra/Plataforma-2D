using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    public static Shaker instance;
    private float power, duration, frameFreezeTime;
    private bool shouldShake, canFreeze;
    private Vector3 startPosition;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        startPosition = transform.localPosition;
    }

    private void Update()
    {
        if (shouldShake)
        {
            if (canFreeze)
            {
                StartCoroutine(FrameFreeze());
            }

            if (duration > 0)
            {
                transform.localPosition = startPosition + (Random.insideUnitSphere * power) * Time.deltaTime;
                duration -= Time.deltaTime;
            }
            else
            {
                canFreeze = true;
                shouldShake = false;
                duration = 0;
                transform.localPosition = startPosition;
            }
        }
    }

    public void SetValues(float powerVal, float durationVal, float frameFreezeTimeVal)
    {
        power = powerVal;
        duration = durationVal;
        canFreeze = true;
        shouldShake = true;
        frameFreezeTime = frameFreezeTimeVal;
    }

    private IEnumerator FrameFreeze()
    {
        canFreeze = false;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(frameFreezeTime);
        Time.timeScale = 1;
    }
}
