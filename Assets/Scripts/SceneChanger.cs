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
    public void ChangeLoading()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("suddenLoading");
    }

    public void ChangeMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("suddenMain");
    }

    public void ChangeStart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("suddenStart");
    }

    public void ChangeHomepage()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("suddenHomepageLoading");
    }
   




    public void Change()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("suddenLoading");
    }
    IEnumerator Loading()
    {
        if (SceneManager.GetActiveScene().name == "suddenHomepageLoading")
        {
            var oper = SceneManager.LoadSceneAsync("suddenStart"); //���Ŵ���.�ε忡�̽�ũ


            oper.allowSceneActivation = false;
            image.gameObject.SetActive(true);//�ε� �̹��� ���ְ� ����


            while (oper.isDone == false)//�ε��� ������ �ʾѴٸ�
            {
                //��¥ �ε��� ~0.9��
                if (oper.progress < 0.9f)
                {
                    //slider.value = oper.progress;
                }
                //�������� �ε� 0.9
                else
                {
                    break;
                }

                yield return null;
            }

            float time = 0; //�ӽýð�
            float totalTime = Random.Range(4f, 7f);
            while (time < totalTime)
            {
                time += Time.deltaTime * Random.Range(1f, 2f); 
                slider.value = time / totalTime; // ����� �ݿ�

                yield return null;
            }
            oper.allowSceneActivation = true;
            image.gameObject.SetActive(false);

        }
        else if(SceneManager.GetActiveScene().name == "suddenLoading")
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





}
