using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private int dir, index;
    private bool wait;
    private float timer;
    public float speed, waitTime;
    public Transform[] wayPoints;

    private void Start()
    {
        dir = 1;
        index = 0;
        wait = true;
    }

    private void Update()
    {
        if (wait)
        {
            CountingWaitTime();
            return;
        }

        ChangeWayPoint();
        Moving();
    }

    private void CountingWaitTime()
    {
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            wait = false;
            timer = 0;
        }
    }

    private void ChangeWayPoint()
    {
        float distance = Vector2.Distance(transform.position, wayPoints[index].position);

        if (dir > 0 && distance <= 0)
        {
            index++;
            if (index >= wayPoints.Length)
            {
                index = wayPoints.Length - 1;
                dir = -1;
                wait = true;
            }
        }
        else if (dir < 0 && distance <= 0)
        {
            index--;
            if (index < 0)
            {
                index = 0;
                dir = 1;
                wait = true;
            }
        }
    }

    private void Moving()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[index].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
