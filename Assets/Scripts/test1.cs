using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

//PPT //ui���ַ� �ұ�?
//1. ���� : ����, ���� => ���� => ��ǥ, ����°���, ��Ƽ���� �̹��� ������ => ����Ƽ ��� => �����̽� => �ƽ����� => ����
//   -���� : �������2.5F
//   -���� : ó������ ������ �����ְ�, ���� ����, ��������, ���� ī�޶���ȯ, �����̼�ȯ�ϰ� �׿����� ������
//   -��ǥ : ��ɱ���, UI, Ǯ��
//   -����°��� : ���� �ؿԴ� ���α׷� ���鼭 ����ٰ� ����000���� ���� �ּż� ����� ������
//                -� �����̶� �ϳ� ����߰ٴ� �������, �װ� UI��������
//                -�׷��� ACTION���� GAMEMANAGER�� �� �غ��� ��� �; PLAYER�� UPDATE,FIXEDUPDATE�� ���ӸŴ�����������
//   -�ẻ ��� : Ǯ��, action ���������� �����, ����������, json
//   -�̽� : ������ ������ ������ �������°�, ���̸� �ȹٲٰ� �ϴ� �Ҹųֱ� ���ؼ� ����
//   -�ƽ����� : �ð��� �� ���־����� action �������������� ��� update�� ����� �ϳ��� ��ũ��Ƽ���� �غ����� ����
//                -������ Ǯ���� ���µ� �̰� �� �Լ� �ȿ� ��������
//   -���� : ��, ���̹���α�, ��Ʃ��, ����Ƽ����Ʈ

public class test1 : MonoBehaviour, IDamageable
{
    [SerializeField] GameObject[] city;
    [SerializeField] int nowTr = 0;
    int asd = 0;
    [SerializeField] int playerMoney = 100000;
    public event Action<int> OnChangeMoney;


    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    StartCoroutine(ddddddddd(1));
            
        //}
    }
    public void buttonNum(int num)
    {
        StartCoroutine(ddddddddd(num));

    }


    IEnumerator ddddddddd(int num)
    {
        asd = nowTr + num;
        float time = 0;
        Vector3 startPos = transform.position;
        if (asd > city.Length-1)
        {
            asd -= city.Length;
        }
        Vector3 goPos = city[asd].transform.position;
        while (time>1f)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, goPos, time/1);
            yield return null;
        }
        nowTr = asd;
        transform.position = goPos;

        var Test123456 = city[asd].GetComponent<test123456>();

        playerMoney -= Test123456.money;
        OnChangeMoney?.Invoke(playerMoney);

    }

    public void GetDamge(int damage)
    {

    }
}
