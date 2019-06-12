using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTime : MonoBehaviour
{
    public float destroyTime;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
