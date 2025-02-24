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
    //    if (GUI.Button(new Rect(20, 40, 400, 300), "씬 로딩"))
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
        var oper = SceneManager.LoadSceneAsync("suddenMain"); //씬매니저.로드에이싱크

        oper.allowSceneActivation = false;
        image.gameObject.SetActive(true);//로딩 이미지 켜주고 시작


        while (oper.isDone == false)//로딩이 끝나지 않앗다면
        {
            //진짜 로딩중 ~0.9임
            if (oper.progress < 0.9f)
            {

            }
            //여까지가 로딩 0.9
            else
            {
                break;
            }

            yield return null;
        }
        float time = 0; //임시시간
        while (time < 5f)
        {
            time += Time.deltaTime; //시간누적
            slider.value = time / 5f; //가짜 진행률 담음
            yield return null;
        }
        oper.allowSceneActivation = true;
        image.gameObject.SetActive(false);

    }







}
