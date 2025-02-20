using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo : MonoBehaviour
{
    public Color color = Color.red;

    private void OnDrawGizmos()
    {
        Gizmos.color = color; //º×À» ¹Ù²Ù¾î Àâ´Â´Ù, ÀÌ »ö±ò º×À¸·Î
        //Gizmos.DrawSphere(transform.position, radius);  

        Vector3 end = transform.position + transform.forward * 10f;
        Gizmos.DrawLine(transform.position, end);

    }
}
