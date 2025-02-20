using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class test1 : MonoBehaviour
{
    Quaternion rotate;
    float rotateSpeed = 0.1f;

    private void Start()
    {
        rotate = transform.rotation;
    }
    private void Update()
    {
        rotateSpeed += 2f;
        transform.rotation = Quaternion.Euler(0,0,rotateSpeed); 
        
    }

}
