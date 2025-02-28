using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

//PPT //ui위주로 할까?
//1. 순서 : 제목, 차례 => 영상 => 목표, 만드는과정, 머티리얼 이미지 입히기 => 유니티 기능 => 조명이슈 => 아쉬운점 => 참고
//   -제목 : 서든어택2.5F
//   -영상 : 처음부터 끝까지 보여주고, 볼륨 저장, 자유시점, 윗폭 카메라전환, 적많이소환하고 죽여보기 렉없음
//   -목표 : 기능구현, UI, 풀링
//   -만드는과정 : 전에 해왔던 프로그램 뜯어보면서 만들다가 괴도000씨가 맵을 주셔서 욕심이 생겻음
//                -어떤 패턴이라도 하나 써봐야겟다 맘잡앗음, 그게 UI옵저버임
//                -그러다 ACTION으로 GAMEMANAGER가 다 해보면 어떨까 싶어서 PLAYER의 UPDATE,FIXEDUPDATE를 게임매니저에서햇음
//   -써본 기능 : 풀링, action 옵저버패턴 장단점, 사운드조절바, json
//   -이슈 : 조명이 많으면 프레임 떨어지는거, 씬이름 안바꾸고 하다 소매넣기 당해서 날라감
//   -아쉬운점 : 시간이 좀 더있엇으면 action 옵저버패턴으로 모든 update를 지우고 하나의 스크립티에서 해보면어떻게 싶음
//                -여러개 풀링을 썻는데 이걸 한 함수 안에 햇으면함
//   -참고 : 맵, 네이버블로그, 유튜브, 유니티사이트

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
