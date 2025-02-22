using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

interface IPooling
{
    void resetCount();
}


public class WaitDestory : MonoBehaviour
{
    
    void Update()
    {
        StartCoroutine(timeDlay());
    }


    IEnumerator timeDlay()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }



}

