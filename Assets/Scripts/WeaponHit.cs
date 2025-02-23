using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class WeaponHit : MonoBehaviour, IDamageable
{
    public LayerMask GetMask;
    Animator animator;
    public GameObject raygameobj;
    public GameObject EffactPrefab;
    public GameObject bloodPrefab;
    public GameObject wallPrefab;

    public RectTransform[] uiCrossHairTr;
    Vector2[] uiCrossHairstartPos;

    int weaponpower = 43; 
    int maxAmmo;//총탄창개수270
    int ammo;//한탄창
    int ammoCount;//현재탄창갯수
    bool canShoot = false; //쏠수있는지
    bool isReload =true;

    Vector3 startTransform;
    public GameObject cam;

    //이벤트 ui
    public event Action<int,int> OnWeaponAmmo;
    public event Action<bool> OnShoot;

    //풀링
    IObjectPool<Bullet> weponPool;
    IObjectPool<Bullet> weponPool2;

    private void Start()
    {
        uiCrossHairstartPos = new Vector2[4];
        animator = GetComponent<Animator>();
        Reset();
        for (int i = 0; i < uiCrossHairTr.Length; i++)
        {
            uiCrossHairstartPos[i] = uiCrossHairTr[i]!.anchoredPosition;
        }
        startTransform = transform.localPosition;

        
    }

    private void Awake()
    {
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
   

    void Reset()
    {
        ammo = 30;
        maxAmmo = 270;
        ammoCount = ammo;
        weaponpower = 43;
        canShoot = true;
        isReload = true;    
    }


    private void Update()
    {
        //총알 ui//근데 애는 업데이트에 계속 하면 뭔의미냐 생각해볼수있도록 ?????
        OnWeaponAmmo?.Invoke(ammoCount, maxAmmo);


        Ray ray = new Ray(raygameobj.transform.position, transform.forward); //진짜 레이
        RaycastHit hit; //맞은 정보 기억할 임시 변수
        Debug.DrawRay(raygameobj.transform.position, transform.forward, Color.yellow, 10f);

        int layerMaskEnemy = LayerMask.GetMask("Enemy");
        int layerMaskWall = LayerMask.GetMask("Wall");

        if (Input.GetKeyDown(KeyCode.R) && isReload == true)
        {
            StartCoroutine(Reload());

        }
        else if (Input.GetMouseButtonDown(0) && canShoot == true)
        {
            //총이 0발일 경우
            if (ammoCount <= 0)
            {
                StartCoroutine(Reload());
            }
            else if (ammoCount >= 1)
            {
                //UI 쏨
                OnShoot?.Invoke(true);

                //ray = new Ray(raygameobj.transform.position+Random.onUnitSphere, transform.forward);
                ammoCount--;
                //Raycast(발사할 레이, 충돌 정보 저장할 변수, 최대거리, 감지하고자 하는 레이어)
                if (Physics.Raycast(ray, out hit, 100f, layerMaskEnemy))
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

            }
            StartCoroutine(waithit2(0.2f));

        }


    }

    IEnumerator Reload()
    {
        Debug.Log("r키");
        isReload = false;
        animator.SetTrigger("Reload");
        canShoot = false;
        yield return new WaitForSeconds(2.2f);
        canShoot= true;
        maxAmmo -= (ammo-ammoCount); 
        ammoCount = ammo;
        isReload = true;
    }

    //총 머즐 프리팹
    IEnumerator EffactTimedelay()
    {
        EffactPrefab.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        EffactPrefab.SetActive(false);
    }

   
    IEnumerator waithit2(float duration )
    {
        Vector3 targetPos = transform.localPosition;
        targetPos.z -= 0.1f;
        transform.localPosition = targetPos;

        yield return new WaitForSeconds(0.1f);

        transform.localPosition = startTransform;
       
        yield return null;


        
    }

    public void GetDamge(int damage)
    {



    }

}
