using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public Transform camPos;
    Camera cam;

    private void Start()
    {

        cam = Camera.main;
    }



    private void LateUpdate()
    {
        cam.transform.position = camPos.position;
    }









}
