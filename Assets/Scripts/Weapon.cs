using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Weapon : MonoBehaviour
{
    //********************event action °¢°¢ ±¸ÇöÇØ¾ß´ï
    //ÇÊ¼ö ±¸¼º¿ä¼Ò
    [SerializeField] protected LayerMask GetMask;
    protected Animator animator;
    [SerializeField] protected GameObject raygameobj;
    [SerializeField] protected GameObject EffactPrefab;
    [SerializeField] protected GameObject bloodPrefab;
    [SerializeField] protected GameObject wallPrefab;



    protected int weaponpower = 43;
    protected int maxAmmo;//ÃÑÅºÃ¢°³¼ö270
    protected int ammo;//ÇÑÅºÃ¢
    protected int ammoCount;//ÇöÀçÅºÃ¢°¹¼ö
    protected bool canShoot = false; //½ò¼öÀÖ´ÂÁö
    protected bool isReload = true;

    protected Vector3 startTransform;
    [SerializeField] protected GameObject cam;

    //Ç®¸µ ±¸Çö
    //»ç¿ëÇÒ¶§ weponPool.Get(); ÇØÁà¾ß´ï
    protected IObjectPool<Bullet> weponPool;
    protected IObjectPool<Bullet> weponPool2;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        weponPool = new ObjectPool<Bullet>(
            () => inst(bloodPrefab),
            bullet => bullet.gameObject.SetActive(true),
            bullet => bullet.gameObject.SetActive(false),
            bullet => Destroy(bullet.gameObject),
            maxSize: 20);
        weponPool2 = new ObjectPool<Bullet>(
          () => inst2(wallPrefab),
          bullet => bullet.gameObject.SetActive(true),
          bullet => bullet.gameObject.SetActive(false),
          bullet => Destroy(bullet.gameObject),
          maxSize: 20);
    }


    Bullet inst(GameObject prefab)
    {
        Bullet bullet = Instantiate(prefab).GetComponent<Bullet>();
        bullet.setPool(weponPool);
        return bullet;
    }
    Bullet inst2(GameObject prefab)
    {
        Bullet bullet = Instantiate(prefab).GetComponent<Bullet>();
        bullet.setPool(weponPool2);
        return bullet;
    }



    //±âº»°ª, ¹Ù²ã¾ß´ï
    protected virtual void Reset()
    {
        ammo = 30;
        maxAmmo = 270;
        ammoCount = ammo;
        weaponpower = 43;
        canShoot = true;
        isReload = true;
    }


    protected virtual IEnumerator Reload()
    {
        Debug.Log("rÅ°");
        isReload = false;
        animator.SetTrigger("Reload");
        canShoot = false;
        yield return new WaitForSeconds(2.2f);
        canShoot = true;
        maxAmmo -= (ammo - ammoCount);
        ammoCount = ammo;
        isReload = true;
    }

    //ÃÑ ¸ÓÁñ ÀÌÆåÆ®
    protected IEnumerator EffactTimedelay()
    {
        EffactPrefab.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        EffactPrefab.SetActive(false);
    }


}
