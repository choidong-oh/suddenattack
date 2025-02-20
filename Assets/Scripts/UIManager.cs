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
        //enemy ��üũ
        FindObjectOfType<EnemyAi>().OnHealthChanged += UpdateHealthUI;

        //Playerźâ
        weaponHit = FindObjectOfType<WeaponHit>();
        weaponHit.OnWeaponAmmo += UpdateWeaponAmmoUI;

        //ũ�ν����
        //Player�� ����ִ���
        CrossHairStartValue();
        weaponHit.OnShoot += UpdateCrossHair;

        //���ٽ� 
        //FindObjectOfType<EnemyAi>().OnHealthChanged += (newHealth) =>
        //{
        //    EnemyHpText.text = "Enemy HP: " + newHealth;
        //};

    }

    //�ִ� ���ڰ� �ȳ־��
    void UpdateCrossHair()
    {
        StartCoroutine(UICrossHair(0.1f));
        StartCoroutine(UICrossHair2(2f));

    }

    //ui ũ�ν���� �������� ����
    Vector2 targetPos1 = new Vector2(0, 50);
    Vector2 targetPos2 = new Vector2(0, -50);
    Vector2 targetPos3 = new Vector2(50, 0);
    Vector2 targetPos4 = new Vector2(-50, 0);
    IEnumerator UICrossHair(float duration)
    {
        float time = 0;
        //time2 = 0;
        //������
        while (time < duration)
        {
            CrossHair[0].anchoredPosition = Vector2.Lerp(CrossHairTr[0], targetPos1, time / duration);
            CrossHair[1]!.anchoredPosition = Vector2.Lerp(CrossHairTr[1], targetPos2, time / duration);
            CrossHair[2]!.anchoredPosition = Vector2.Lerp(CrossHairTr[2], targetPos3, time / duration);
            CrossHair[3]!.anchoredPosition = Vector2.Lerp(CrossHairTr[3], targetPos4, time / duration);
            time += Time.deltaTime;
            yield return null;
        }


    }
    IEnumerator UICrossHair2(float duration)
    {
        float time = 0;
        //���ƿ�
        while (time < duration)
        {
            CrossHair[0].anchoredPosition = Vector2.Lerp(targetPos1, CrossHairTr[0], time / duration);
            CrossHair[1]!.anchoredPosition = Vector2.Lerp(targetPos2, CrossHairTr[1], time / duration);
            CrossHair[2]!.anchoredPosition = Vector2.Lerp(targetPos3, CrossHairTr[2], time / duration);
            CrossHair[3]!.anchoredPosition = Vector2.Lerp(targetPos4, CrossHairTr[3], time / duration);
            time += Time.deltaTime;
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
