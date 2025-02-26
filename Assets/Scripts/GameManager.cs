using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action<int> OnChangeWeapon;
    public event Action OnTurnBack;
    public event Action<float> OnMouseSpeed;
    public event Action OnRotateUp;//�ݵ�ó�� �ö�
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
        //�κ��丮 �Ѽ���
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

        //��������
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            OnFreeView?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Home))
        {
            OnHome?.Invoke();
        }
        //����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump?.Invoke();
        }

        //���콺 rotate
        OnMouseRotate?.Invoke();

        //�ڵ���
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnTurnBack?.Invoke();
        }


        //���콺 ���ǵ� ����// [ ]
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            Debug.Log("dsds");
            OnMouseSpeed?.Invoke(-1f);
        } 
        else if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            OnMouseSpeed?.Invoke(1f);
        }


        //�ݵ� ó�� ����
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

        //����esc
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

        //�׳� ������
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
