using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo : MonoBehaviour
{
    public Color color = Color.red;

    private void OnDrawGizmos()
    {
        Gizmos.color = color; //���� �ٲپ� ��´�, �� ���� ������
        //Gizmos.DrawSphere(transform.position, radius);  

        Vector3 end = transform.position + transform.forward * 10f;
        Gizmos.DrawLine(transform.position, end);

    }
}
