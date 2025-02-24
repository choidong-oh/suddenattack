using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class WeaponAK47 : Weapon
{
    //��������
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
        //�Ѿ� ui//�ٵ� �ִ� ������Ʈ�� ��� �ϸ� ���ǹ̳� �����غ����ֵ��� ?????
        OnAk47Ammo?.Invoke(ammoCount, maxAmmo);

        Ray ray = new Ray(raygameobj.transform.position, transform.forward); //��¥ ����
        RaycastHit hit; //���� ���� ����� �ӽ� ����
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
                //UI ��
                OnAk47Shoot?.Invoke(true);

                //ray = new Ray(raygameobj.transform.position+Random.onUnitSphere, transform.forward);
                ammoCount--;
                //Raycast(�߻��� ����, �浹 ���� ������ ����, �ִ�Ÿ�, �����ϰ��� �ϴ� ���̾�)
                if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Enemy")))
                {
                    var enemy = hit.collider.gameObject.GetComponent<IDamageable>();
                    enemy.GetDamge(weaponpower);
                    //var dd =   Instantiate(bloodPrefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));

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

                //cam.transform.localRotation  = Quaternion.Euler(-0.1f, 0, 0);


                //�� ���� ������
                StartCoroutine(EffactTimedelay());
                //PlayEffect();
            }
            //�ѽ�� �ִϸ��̼�
            StartCoroutine(Shootwait(0.2f));

        }


    }

    public void ReloadSound(int index)
    {
        audioSource.PlayOneShot(reload[index]);
    }

    protected override IEnumerator Reload()
    {
        

        Debug.Log("rŰ");
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

    //���� ��� �ڷ� ���� �ݵ�ó�� ����
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
        canShoot = true; //����ִ���
        isReload = true;
    }

   
    public void GetDamge(int damage)
    {



    }




}
