using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //UI�ؽ�Ʈ
    [SerializeField] private Text EnemyHpText;
    [SerializeField] private Text WeaponAmmoText;

    [SerializeField] private RectTransform[] CrossHair;
    Vector2[] CrossHairTr;

    //��ũ��Ʈ �޷��ִ°� �־����
    [SerializeField] private EnemyAi enemyAi;
    [SerializeField] private WeaponAK47 weaponAK47;
    [SerializeField] private WeaponDeagle weaponDeagle;

    void Start()
    {
        //enemy ��üũ
        enemyAi.OnHealthChanged += UpdateHealthUI;

        //Playerźâ
        weaponAK47.OnAk47Ammo += UpdateWeaponAmmoUI;
        weaponDeagle.OnDeagleAmmo += UpdateWeaponAmmoUI;

        //Player�� ����ִ���
        CrossHairStartValue();
        weaponAK47.OnAk47Shoot += UpdateCrossHair;
        weaponDeagle.OnDeagleShoot += UpdateCrossHair;

    }
    private void OnEnable()
    {
        


    }
    private void OnDisable()
    {
        
    }
    //�ִ� ���ڰ� �ȳ־��
    void UpdateCrossHair(bool isShoot)
    {
        StartCrossHair(0.1f);

    }

    //ui ũ�ν���� �������� ����
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
        //������
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
        //���ƿ�
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

  

    void UpdateHealthUI(int Hp)
    {
        EnemyHpText.text = "Enemy HP: " + Hp;
    }
    void UpdateWeaponAmmoUI(int ammo,int maxAmmo)
    {
        WeaponAmmoText.text = ammo + " / " + maxAmmo;
    }

}
