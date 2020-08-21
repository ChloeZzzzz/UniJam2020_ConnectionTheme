﻿using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Animation when starting the game, Camera falls from above
public class CameraInitial : MonoBehaviour
{
    private Vector3 finalPos;
    public float InitialHeight = 100; 
    public float accFactor = 15;
    public float dropSpeed = 3;
    private GameObject player;
    private MoveCam CamMoveScript;
    // Start is called before the first frame update
    void Start()
    {
        finalPos = this.transform.localPosition;
        this.transform.localPosition = finalPos + new Vector3 (0.0f,InitialHeight,0.0f);
        player = GameObject.Find("Player");
        CamMoveScript = player.GetComponent<MoveCam>();

        //disable camera controller at first and look from sky
        CamMoveScript.enabled = false;
        this.transform.LookAt(player.transform);
        this.transform.RotateAround(transform.position, transform.forward, 180f);
    }

    // Update is called once per frame
    void Update()
    {
        
        //if the camera falls and reached the player, starting rotation
        if (this.transform.localPosition.y<=finalPos.y+1){
            if (this.transform.localRotation.x >= finalPos.x+0.5){
                this.transform.localRotation *= Quaternion.AngleAxis(Time.deltaTime * 15*15, new Vector3 (-10.0f,0.0f,0.0f));
            }
            //finished camera movement, enable control and disable this script
            else{
                player.GetComponent<PressF1OpenGuide>().guide.SetActive(true);
                this.transform.localPosition = finalPos;
                CamMoveScript.enabled = true;
                this.enabled = false;
            }
        }

        //camera move down to reach player
        else {
            this.transform.localPosition += new Vector3 (0.0f,-dropSpeed*Time.deltaTime * accFactor,0.0f);
        }
    }
}
