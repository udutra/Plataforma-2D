using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private bool used;
    public bool singleUse;
    public UnityEvent OnTrigger, OnExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (used)
        {
            return;
        }
        if (singleUse)
        {
            used = true;
        }

        OnTrigger.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (singleUse)
        {
            return;
        }
        OnExit.Invoke();
    }
}