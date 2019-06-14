using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject[] lives;

    private void Awake()
    {
        instance = this;
    }

    public void SetLives(int amount)
    {
        for (int i = 0; i < lives.Length; i++)
        {
            lives[i].SetActive(false);
        }
        for (int i = 0; i < amount; i++)
        {
            lives[i].SetActive(true);
        }
    }
}
