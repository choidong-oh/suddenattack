using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;



//����
//1. FindObjectOfType, FindAnyObjectByType ������������
//2. Ǯ���������� ���� �����غ���

//�Ұ�
//��ɱ��� ���� ����
//4. ���� �߰�,( �߼Ҹ�,  ����, ��ź)
//5. ��弦����ų��Ʈ��ų ����
//6. ��������




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
