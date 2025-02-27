using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomInst : MonoBehaviour
{
    public GameObject Boom;
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //Instantiate(Boom, transform.position, Quaternion.identity);
            GameObject temp = Instantiate(Boom, transform.position, Quaternion.identity);
            temp.transform.parent = this.transform;
            temp.transform.localScale = Vector3.one;
            temp.gameObject.SetActive(true);
        }
    
    }







}
