using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Dialogue Panel")]
    public Text dialogueText;
    public Animator dialoguePanel;

    [Header("Lives")]
    public GameObject[] lives;

    [Header("Keys")]
    public GameObject[] keys;

    private void Awake()
    {
        instance = this;
    }

    public void SetText(string txt)
    {
        CancelInvoke();
        dialogueText.text = txt;
        dialoguePanel.gameObject.SetActive(true);
    }

    public void SetTextOut()
    {
        Invoke("TextOut", 1f);
    }

    private void TextOut()
    {
        dialoguePanel.Play("Dialogue Exit");
        Invoke("DisableDialoguePanel", 0.5f);
    }

    private void DisableDialoguePanel()
    {
        dialoguePanel.gameObject.SetActive(false);
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

    public void SetKeys(int amount)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].SetActive(false);
        }
        for (int i = 0; i < amount; i++)
        {
            keys[i].SetActive(true);
        }
    }
}
