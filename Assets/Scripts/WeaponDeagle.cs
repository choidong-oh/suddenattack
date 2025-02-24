using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponDeagle : Weapon
{
    //각각구현
    public event Action<int, int> OnDeagleAmmo;
    public event Action<bool> OnDeagleShoot;


    private void Start()
    {
        Reset();
        startTransform = transform.localPosition;
        OnDeagleAmmo?.Invoke(ammoCount, maxAmmo);

    }



    private void Update()
    {
        //총알 ui//근데 애는 업데이트에 계속 하면 뭔의미냐 생각해볼수있도록 ?????
        OnDeagleAmmo?.Invoke(ammoCount, maxAmmo);

        Ray ray = new Ray(raygameobj.transform.position, -transform.right); //진짜 레이
        RaycastHit hit; //맞은 정보 기억할 임시 변수
        Debug.DrawRay(raygameobj.transform.position, transform.forward, Color.yellow, 10f);

        int layerMaskEnemy = LayerMask.GetMask("Enemy");
        int layerMaskWall = LayerMask.GetMask("Wall");

        
        if (Input.GetKeyDown(KeyCode.R) && canShoot == true )
        {
            StartCoroutine(Reload());
        }
        else if(ammoCount <= 0 && isReload ==true)
        {
            StartCoroutine(Reload());
        }
        else if (Input.GetMouseButtonDown(0) && canShoot == true)
        {
           
            if (ammoCount >= 1)
            {
                    Debug.Log("dsddsds2");
                //UI 쏨
                OnDeagleShoot?.Invoke(true);
                //ray = new Ray(raygameobj.transform.position+Random.onUnitSphere, transform.forward);
                ammoCount--;
                //Raycast(발사할 레이, 충돌 정보 저장할 변수, 최대거리, 감지하고자 하는 레이어)
                if (Physics.Raycast(ray, out hit, 100f, layerMaskEnemy))
                {
                    Debug.Log("dsddsds1");
                    var enemy = hit.collider.gameObject.GetComponent<IDamageable>();
                    enemy.GetDamge(weaponpower);


                    //풀링
                    var dd = weponPool.Get();
                    dd.transform.position = hit.point;
                    dd.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                    dd.poolShoot();


                }
                else if (Physics.Raycast(ray, out hit, 100f, GetMask))
                {
                    //Instantiate(wallPrefab, hit.point - Vector3.forward * 0.1f, Quaternion.FromToRotation(Vector3.back, hit.normal));
                    var dd = weponPool2.Get();
                    dd.transform.position = hit.point - Vector3.forward * 0.1f;
                    dd.transform.rotation = Quaternion.FromToRotation(Vector3.back, hit.normal);
                    dd.poolShoot();


                }

                //총 머즐 프리팹
                StartCoroutine(EffactTimedelay());

                //총쏘는 애니메이션
                StartCoroutine(ShootWait(0.6f));

            }

        }


    }

    void OnEnable()
    {
        canShoot = true; //쏠수있는지
        isReload = true;
        animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        //StopAllCoroutines();


    }

    IEnumerator ShootWait(float second)
    {
        canShoot = false;
        animator.SetTrigger("Hit");
        yield return new WaitForSeconds(second);
        canShoot = true;
    }
    IEnumerator test()
    {
        yield return new WaitForSeconds(1);
    }


    protected override void Reset()
    {
        base.Reset();
        ammo = 7;
        ammoCount = ammo;
        maxAmmo = 270;
        weaponpower = 50;
    }

  

}
