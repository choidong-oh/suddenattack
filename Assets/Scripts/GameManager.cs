using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action<int> OnChangeWeapon;
    public event Action OnTurnBack;
    public event Action<float> OnMouseSpeed;
    public event Action OnRotateUp;//반동처럼 올라감
    public event Action OnMouseRotate;
    public event Action OnFreeView;
    public event Action OnHome;
    public event Action OnJump;
    public event Action OnSetDown;
    public event Action OnSetDownBack;
    public event Action<bool> OnMove;
    [SerializeField] private GameObject uiSetting;

    private void Update()
    {
        //인벤토리 총순서
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnChangeWeapon?.Invoke(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OnChangeWeapon?.Invoke(2);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            OnChangeWeapon?.Invoke(3);

        }

        //자유시점
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            OnFreeView?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Home))
        {
            OnHome?.Invoke();
        }
        //점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump?.Invoke();
        }

        //마우스 rotate
        OnMouseRotate?.Invoke();

        //뒤돌기
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnTurnBack?.Invoke();
        }


        //마우스 스피드 변경// [ ]
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            Debug.Log("dsds");
            OnMouseSpeed?.Invoke(-1f);
        } 
        else if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            OnMouseSpeed?.Invoke(1f);
        }


        //반동 처럼 보임
        if (Input.GetMouseButtonDown(0))
        {
            OnRotateUp?.Invoke();
        }


        if (Input.GetKey((KeyCode.LeftControl)))
        {
            OnSetDown?.Invoke();
        }
        else if (Input.GetKeyUp((KeyCode.LeftControl)))
        {
            OnSetDownBack?.Invoke();
        }

        //설정esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            uiSetting.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
    }


    private void FixedUpdate()
    {

        //그냥 움직임
        if (Input.GetKey(KeyCode.LeftShift))
        {
            OnMove?.Invoke(true);
        }
        else
        {
            OnMove?.Invoke(false);
        }

    }


    public void TimeScaleOne()
    {
        Time.timeScale = 1f;
        uiSetting.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }



}
