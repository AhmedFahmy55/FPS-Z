using FPS.Sounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    
    void Start()
    {
        SoundManager.Instannce.PlaySound(Sound.Login, transform.position);
        Time.timeScale = 0;
    }


    public void StartGame()
    {
        SoundManager.Instannce.PlaySound(Sound.Ambian, transform.position);
        Destroy(GameObject.Find("SoundFX"));
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;

    }

}
