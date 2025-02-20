using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffactCortin : MonoBehaviour
{
    public GameObject gameobj;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameobj.SetActive(true);
        }
    }

    IEnumerator EffactTimedelay()
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }



}
