using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Slider slider;

    //private void OnGUI()
    //{
    //    if (GUI.Button(new Rect(20, 40, 400, 300), "�� �ε�"))
    //    {
    //        StartCoroutine(Loading());
    //    }



    //}
    private void Start()
    {
        StartCoroutine(Loading());
    }

    public void Change()
    {
        SceneManager.LoadScene("suddenLoading");
    }
    IEnumerator Loading()
    {
        var oper = SceneManager.LoadSceneAsync("suddenMain"); //���Ŵ���.�ε忡�̽�ũ

        oper.allowSceneActivation = false;
        image.gameObject.SetActive(true);//�ε� �̹��� ���ְ� ����


        while (oper.isDone == false)//�ε��� ������ �ʾѴٸ�
        {
            //��¥ �ε��� ~0.9��
            if (oper.progress < 0.9f)
            {

            }
            //�������� �ε� 0.9
            else
            {
                break;
            }

            yield return null;
        }
        float time = 0; //�ӽýð�
        while (time < 5f)
        {
            time += Time.deltaTime; //�ð�����
            slider.value = time / 5f; //��¥ ����� ����
            yield return null;
        }
        oper.allowSceneActivation = true;
        image.gameObject.SetActive(false);

    }







}
