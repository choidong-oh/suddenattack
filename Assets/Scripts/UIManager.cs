using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text EnemyHpText;

    public Text WeaponAmmoText;

    public RectTransform[] CrossHair;
    Vector2[] CrossHairTr;

    WeaponHit weaponHit;
    void Start()
    {
        //enemy 피체크
        FindObjectOfType<EnemyAi>().OnHealthChanged += UpdateHealthUI;

        //Player탄창
        weaponHit = FindObjectOfType<WeaponHit>();
        weaponHit.OnWeaponAmmo += UpdateWeaponAmmoUI;

        //크로스헤어
        //Player가 쏘고있는지
        CrossHairStartValue();
        weaponHit.OnShoot += UpdateCrossHair;

        //람다식 
        //FindObjectOfType<EnemyAi>().OnHealthChanged += (newHealth) =>
        //{
        //    EnemyHpText.text = "Enemy HP: " + newHealth;
        //};

    }

    //애는 인자값 안넣어보자
    void UpdateCrossHair(bool isShoot)
    {
        StartCrossHair(0.1f);

    }

    //ui 크로스헤어 벌어지는 변수
    Vector2 targetPos1 = new Vector2(0, 50);
    Vector2 targetPos2 = new Vector2(0, -50);
    Vector2 targetPos3 = new Vector2(50, 0);
    Vector2 targetPos4 = new Vector2(-50, 0);

    Coroutine crossHairCoroutine;

    void StartCrossHair(float duration)
    {
        if (crossHairCoroutine != null)
        {
            StopCoroutine(crossHairCoroutine);
        }
        crossHairCoroutine = StartCoroutine(UICrossHair(duration));
    }

    IEnumerator UICrossHair(float duration)
    {
        float time = 0;
        //time2 = 0;
        //증가함
        while (time < duration)
        {
            CrossHair[0].anchoredPosition = Vector2.Lerp(CrossHair[0].anchoredPosition, targetPos1, time / duration);
            CrossHair[1]!.anchoredPosition = Vector2.Lerp(CrossHair[1].anchoredPosition, targetPos2, time / duration);
            CrossHair[2]!.anchoredPosition = Vector2.Lerp(CrossHair[2].anchoredPosition, targetPos3, time / duration);
            CrossHair[3]!.anchoredPosition = Vector2.Lerp(CrossHair[3].anchoredPosition, targetPos4, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        float time1 = 0;
        //돌아옴
        while (time1 < duration)
        {
            CrossHair[0].anchoredPosition = Vector2.Lerp(targetPos1, CrossHairTr[0], time1 / duration);
            CrossHair[1]!.anchoredPosition = Vector2.Lerp(targetPos2, CrossHairTr[1], time1 / duration);
            CrossHair[2]!.anchoredPosition = Vector2.Lerp(targetPos3, CrossHairTr[2], time1 / duration);
            CrossHair[3]!.anchoredPosition = Vector2.Lerp(targetPos4, CrossHairTr[3], time1 / duration);
            time1 += Time.deltaTime;
            yield return null;
        }

    }
   
    void CrossHairStartValue()
    {
        CrossHairTr = new Vector2[4];
        for (int i = 0; i < CrossHairTr.Length; i++)
        {
            CrossHairTr[i] = CrossHair[i]!.anchoredPosition;
        }
    }

  

    void UpdateHealthUI(int newHealth)
    {
        EnemyHpText.text = "Enemy HP: " + newHealth;
    }
    void UpdateWeaponAmmoUI(int ammo,int maxAmmo)
    {
        WeaponAmmoText.text = ammo + " / " + maxAmmo;
    }


}
