using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

//질문
//1. FindObjectOfType, FindAnyObjectByType 무슨원리인지
//2. 풀링여러개는 아직 생각해보고
//3. 마우스? 움직임이 렉걸림?

//할거
//기능구현 이제 안함
//1. 무기 클래스 쪼개기
//2. 각각의 무기 ui연결, 인벤토리 자식으로 추가하는 기능
//3. 대충 처음씬 => 로딩씬 => 메인 => 상점,인게임씬 => 종료씬(메인씬)
//4. 사운드 추가
//5. 인게임에서 볼륨조절 가능하게 설정
//6. 상점구현




public class test1 : MonoBehaviour
{
    public float turnSpeed = 4.0f;    
    private float xRotate = 0.0f;
    public float moveSpeed = 4.0f; 

    void Update()
    {
        float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;
        float yRotate = transform.eulerAngles.y + yRotateSize;

        float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);

        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);

        Vector3 move =
            transform.forward * Input.GetAxis("Vertical") +
            transform.right * Input.GetAxis("Horizontal");

        transform.position += move * moveSpeed * Time.deltaTime;
    }

}
