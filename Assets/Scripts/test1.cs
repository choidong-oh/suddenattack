using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;



//질문
//1. FindObjectOfType, FindAnyObjectByType 무슨원리인지
//2. 풀링여러개는 아직 생각해보고

//할거
//기능구현 이제 안함
//4. 사운드 추가,( 발소리,  권총, 폭탄)
//5. 헤드샷더블킬울트라킬 구현
//6. 상점구현




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
