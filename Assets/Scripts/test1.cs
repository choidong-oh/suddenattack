using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

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

public class test1 : MonoBehaviour
{
    private void ChangeLoading()
    {
        SceneManager.LoadScene("suddenLoading");
    }

    private void ChangeMain()
    {
        SceneManager.LoadScene("suddenMain");
    }

    private void ChangeStart()
    {
        SceneManager.LoadScene("suddenStart");
    }


   


}
