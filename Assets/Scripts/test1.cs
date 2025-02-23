using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

//����
//1. FindObjectOfType, FindAnyObjectByType ������������
//2. Ǯ���������� ���� �����غ���
//3. ���콺? �������� ���ɸ�?

//�Ұ�
//��ɱ��� ���� ����
//1. ���� Ŭ���� �ɰ���
//2. ������ ���� ui����, �κ��丮 �ڽ����� �߰��ϴ� ���
//3. ���� ó���� => �ε��� => ���� => ����,�ΰ��Ӿ� => �����(���ξ�)
//4. ���� �߰�
//5. �ΰ��ӿ��� �������� �����ϰ� ����
//6. ��������




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
