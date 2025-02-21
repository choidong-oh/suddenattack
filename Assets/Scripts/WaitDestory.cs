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
    public void resetCount()
    {
    }

   
    IEnumerator timeDlay()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    //Ç®¸µ
    private void OnDisable()
    {
        FindObjectOfType<WeaponHit>().resetCount();
    }
    private void OnEnable()
    {
        StartCoroutine(timeDlay());
    }


}

