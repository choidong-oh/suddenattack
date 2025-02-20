using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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
    int maxAmmo;//��źâ����270
    int ammo;//źâ�ѵ� 30
    public int ammoCount;//����źâ����
    bool canShoot = false; //����ִ���
    bool isReload =true;

    Vector3 startTransform;
    public GameObject cam;


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
    void Reset()
    {
        maxAmmo = 270;
        ammo = 30;
        ammoCount = ammo;
        weaponpower = 43;
        canShoot = true;
        isReload = true;    
    }


    private void Update()
    {
        Ray ray = new Ray(raygameobj.transform.position, transform.forward); //��¥ ����
        RaycastHit hit; //���� ���� ����� �ӽ� ����
        Debug.DrawRay(raygameobj.transform.position, transform.forward, Color.yellow, 10f);

        int layerMaskEnemy = LayerMask.GetMask("Enemy");
        int layerMaskWall = LayerMask.GetMask("Wall");

        if (Input.GetKeyDown(KeyCode.R)&& isReload ==true)
        {
            StartCoroutine(Reload());

        }
        else if (Input.GetMouseButtonDown(0)&&canShoot ==true)
        {
            //���� 0���� ���
            if (ammoCount <= 0)
            {
                StartCoroutine(Reload());
            }
            else if (ammoCount >= 1)
            {
                //ray = new Ray(raygameobj.transform.position+Random.onUnitSphere, transform.forward);
                ammoCount--;
                //Raycast(�߻��� ����, �浹 ���� ������ ����, �ִ�Ÿ�, �����ϰ��� �ϴ� ���̾�)
                if (Physics.Raycast(ray, out hit, 100f, layerMaskEnemy))
                {
                    var enemy = hit.collider.gameObject.GetComponent<IDamageable>();
                    enemy.GetDamge(weaponpower);
                    Instantiate(bloodPrefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));

                }
                else if (Physics.Raycast(ray, out hit, 100f, GetMask))
                {
                    var asd = hit.collider.transform.rotation;

                    Instantiate(wallPrefab, hit.point-Vector3.forward*0.1f, Quaternion.FromToRotation(Vector3.back, hit.normal));


                }

                StartCoroutine(UICrossHair(0.1f));
                StartCoroutine(UICrossHair2(2f));

                //cam.transform.localRotation  = Quaternion.Euler(-0.1f, 0, 0);

                

                StartCoroutine(EffactTimedelay());

            }
            StartCoroutine(waithit2(0.2f));

        }

      
    }

    





    //ui ũ�ν���� �������� ����
    Vector2 targetPos1 = new Vector2(0, 50);
    Vector2 targetPos2 = new Vector2(0, -50);
    Vector2 targetPos3 = new Vector2(50, 0);
    Vector2 targetPos4 = new Vector2(-50, 0);
    float time1 = 0;
    float time2 = 0;
    IEnumerator UICrossHair(float duration)
    {
        time1 = 0;
        //time2 = 0;
        //������
        while (time1 < duration)
        {
            uiCrossHairTr[0].anchoredPosition = Vector2.Lerp(uiCrossHairstartPos[0], targetPos1, time1 / duration);
            uiCrossHairTr[1]!.anchoredPosition = Vector2.Lerp(uiCrossHairstartPos[1], targetPos2, time1 / duration);
            uiCrossHairTr[2]!.anchoredPosition = Vector2.Lerp(uiCrossHairstartPos[2], targetPos3, time1 / duration);
            uiCrossHairTr[3]!.anchoredPosition = Vector2.Lerp(uiCrossHairstartPos[3], targetPos4, time1 / duration);
            time1 += Time.deltaTime;
            yield return null;
        }
        

    }
    IEnumerator UICrossHair2(float duration)
    {
        time2 = 0;
        //���ƿ�
        while (time2 < duration)
        {
            uiCrossHairTr[0].anchoredPosition = Vector2.Lerp(targetPos1, uiCrossHairstartPos[0], time2 / duration);
            uiCrossHairTr[1]!.anchoredPosition = Vector2.Lerp(targetPos2, uiCrossHairstartPos[1], time2 / duration);
            uiCrossHairTr[2]!.anchoredPosition = Vector2.Lerp(targetPos3, uiCrossHairstartPos[2], time2 / duration);
            uiCrossHairTr[3]!.anchoredPosition = Vector2.Lerp(targetPos4, uiCrossHairstartPos[3], time2 / duration);
            time2 += Time.deltaTime;
            yield return null;
        }
    }






    IEnumerator Reload()
    {
        Debug.Log("rŰ");
        isReload = false;
        animator.SetTrigger("Reload");
        canShoot = false;
        yield return new WaitForSeconds(2.2f);
        canShoot= true;
        ammoCount = ammo;
        isReload = true;
    }

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
