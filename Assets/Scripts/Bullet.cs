using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;



public class Bullet : MonoBehaviour
{
    IObjectPool<Bullet> Pool;

    public void setPool(IObjectPool<Bullet> _Pool)
    {
        this.Pool = _Pool;
    }

    void destrory()
    {
        Pool.Release(this);
        //Destroy(gameObject);
    }
   
    public void poolShoot()
    {
        Invoke("destrory", 3f);
    }
  



}

