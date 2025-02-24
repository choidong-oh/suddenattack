using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class WeaponAK47 : Weapon
{
    //각각구현
    public event Action<int, int> OnAk47Ammo;
    public event Action<bool> OnAk47Shoot;


    public AudioSource audioSource;
    public AudioClip shoot;
    public AudioClip[] reload;

    public void PlayEffect()
    {
        audioSource.PlayOneShot(shoot);
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Reset();
        startTransform = transform.localPosition;
    }

    private void Update()
    {
        //총알 ui//근데 애는 업데이트에 계속 하면 뭔의미냐 생각해볼수있도록 ?????
        OnAk47Ammo?.Invoke(ammoCount, maxAmmo);

        Ray ray = new Ray(raygameobj.transform.position, transform.forward); //진짜 레이
        RaycastHit hit; //맞은 정보 기억할 임시 변수
        Debug.DrawRay(raygameobj.transform.position, transform.forward, Color.yellow, 10f);

        int layerMaskEnemy = LayerMask.GetMask("Enemy");
        int layerMaskWall = LayerMask.GetMask("Wall");

        if (Input.GetKeyDown(KeyCode.R) && canShoot == true)
        {
            StartCoroutine(Reload());
        }
        else if (ammoCount <= 0 && isReload == true)
        {
            StartCoroutine(Reload());
        }
        else if (Input.GetMouseButtonDown(0) && canShoot == true)
        {
            if (ammoCount >= 1)
            {
                //UI 쏨
                OnAk47Shoot?.Invoke(true);

                //ray = new Ray(raygameobj.transform.position+Random.onUnitSphere, transform.forward);
                ammoCount--;
                //Raycast(발사할 레이, 충돌 정보 저장할 변수, 최대거리, 감지하고자 하는 레이어)
                if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Enemy")))
                {
                    var enemy = hit.collider.gameObject.GetComponent<IDamageable>();
                    enemy.GetDamge(weaponpower);
                    //var dd =   Instantiate(bloodPrefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));

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

                //cam.transform.localRotation  = Quaternion.Euler(-0.1f, 0, 0);


                //총 머즐 프리팹
                StartCoroutine(EffactTimedelay());
                //PlayEffect();
            }
            //총쏘는 애니메이션
            StartCoroutine(Shootwait(0.2f));

        }


    }

    public void ReloadSound(int index)
    {
        audioSource.PlayOneShot(reload[index]);
    }

    protected override IEnumerator Reload()
    {
        

        Debug.Log("r키");
        isReload = false;
        animator.SetTrigger("Reload");
        canShoot = false;
        yield return new WaitForSeconds(2.2f);
        canShoot = true;
        maxAmmo -= (ammo - ammoCount);
        ammoCount = ammo;
        isReload = true;
    }

    IEnumerator waitsecond(float second)
    {
        yield return new WaitForSeconds(second);
    }

    //총을 쏘면 뒤로 가는 반동처럼 보임
    protected IEnumerator Shootwait(float duration)
    {
        Vector3 targetPos = transform.localPosition;
        targetPos.z -= 0.1f;
        transform.localPosition = targetPos;

        yield return new WaitForSeconds(0.1f);

        transform.localPosition = startTransform;

        yield return null;



    }

    void OnEnable()
    {
        canShoot = true; //쏠수있는지
        isReload = true;
    }

   
    public void GetDamge(int damage)
    {



    }




}
