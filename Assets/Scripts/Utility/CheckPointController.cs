using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckPointController : MonoBehaviour
{
    private Vector3 respawnPosition;
    public Transform player;
    public Animator fade;
    public UnityEvent onRestart;
    

    public void SetPos(Vector3 pos)
    {
        respawnPosition = pos;
    }


    public void GameOver()
    {
        Invoke("Restart", 3f);
        fade.Play("Fade");
    }

    public void Restart()
    {
        player.position = respawnPosition;
        onRestart.Invoke();
    }
}
