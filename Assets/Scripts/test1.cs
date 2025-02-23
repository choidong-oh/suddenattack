using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

//질문
//1. FindObjectOfType, FindAnyObjectByType 무슨원리인지
//2. 풀링여러개는 아직 생각해보고
//3. 마우스? 움직임이 렉걸림?




public class test1 : MonoBehaviour
{
    public float turnSpeed = 4.0f;    
    private float xRotate = 0.0f;
    public float moveSpeed = 4.0f; 

    void Update()
    {
        float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
        float yRotate = transform.eulerAngles.y + yRotateSize;

        float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);

        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);

        Vector3 move =
            transform.forward * Input.GetAxis("Vertical") +
            transform.right * Input.GetAxis("Horizontal");

        transform.position += move * moveSpeed * Time.deltaTime;
    }

}
