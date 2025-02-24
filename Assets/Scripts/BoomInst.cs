using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomInst : MonoBehaviour
{
    public GameObject Boom;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Instantiate(Boom, transform.position, Quaternion.identity); 
        }
    }







}
