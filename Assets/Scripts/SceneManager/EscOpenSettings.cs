﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class EscOpenSettings : MonoBehaviour
{
    public GameObject BGM;
    public GameObject SoundFX;
    public GameObject Setting;
    public GameObject Cam;
    public GameObject Player;
    public GameObject UIs;

    private void Start()
    {
    }
    public void replay() {
		SceneManager.LoadScene("MainMaze");
	}
    public void BackToHomePage()
    {
        SceneManager.LoadScene("HomePage");
    }

    public void MuteBGM()
    {
        BGM.SetActive(!BGM.activeSelf);
    }

    public void MuteFX()
    {
        SoundFX.SetActive(!SoundFX.activeSelf);
    }

    public void Resume()
    {
        Setting.SetActive(false);
        Cam.SetActive(true);
        Player.GetComponent<MoveCam>().enabled = true;
        UIs.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
