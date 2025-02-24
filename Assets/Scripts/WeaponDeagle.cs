using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponDeagle : Weapon
{
    //��������
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
        //�Ѿ� ui//�ٵ� �ִ� ������Ʈ�� ��� �ϸ� ���ǹ̳� �����غ����ֵ��� ?????
        OnDeagleAmmo?.Invoke(ammoCount, maxAmmo);

        Ray ray = new Ray(raygameobj.transform.position, -transform.right); //��¥ ����
        RaycastHit hit; //���� ���� ����� �ӽ� ����
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
                //UI ��
                OnDeagleShoot?.Invoke(true);
                //ray = new Ray(raygameobj.transform.position+Random.onUnitSphere, transform.forward);
                ammoCount--;
                //Raycast(�߻��� ����, �浹 ���� ������ ����, �ִ�Ÿ�, �����ϰ��� �ϴ� ���̾�)
                if (Physics.Raycast(ray, out hit, 100f, layerMaskEnemy))
                {
                    Debug.Log("dsddsds1");
                    var enemy = hit.collider.gameObject.GetComponent<IDamageable>();
                    enemy.GetDamge(weaponpower);


                    //Ǯ��
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

                //�� ���� ������
                StartCoroutine(EffactTimedelay());

                //�ѽ�� �ִϸ��̼�
                StartCoroutine(ShootWait(0.6f));

            }

        }


    }

    void OnEnable()
    {
        canShoot = true; //����ִ���
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
