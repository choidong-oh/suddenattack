using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongTimeNoSee : MonoBehaviour
{
    public static LongTimeNoSee instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }



}
