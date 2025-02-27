using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

//PPT
//1. 순서 : 제목, 차례 => 영상 => 목표, 만드는과정, 머티리얼 이미지 입히기 => 유니티 기능 => 조명이슈 => 아쉬운점 => 참고
//   -제목 : 서든어택2.5F
//   -영상 : 처음부터 끝까지 보여주고, 볼륨 저장, 자유시점, 윗폭 카메라전환, 적많이소환하고 죽여보기 렉없음
//   -목표 : 기능구현, UI, 풀링
//   -만드는과정 : 전에 해왔던 프로그램 뜯어보면서 만들다가 괴도000씨가 맵을 주셔서 욕심이 생겻음
//                -어떤 패턴이라도 하나 써봐야겟다 맘잡앗음, 그게 UI옵저버임
//                -그러다 ACTION으로 GAMEMANAGER가 다 해보면 어떨까 싶어서 PLAYER의 UPDATE,FIXEDUPDATE를 게임매니저에서햇음
//

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
