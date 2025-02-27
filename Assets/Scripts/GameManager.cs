using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


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
    [SerializeField] private GameObject uiSound;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject boomCamera;
    private bool isboomCamera = true;  

   

    private void Start()
    {
        Time.timeScale = 1.0f;  
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "suddenMain")
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
                TimeScaleZero();

            }

            //�ð��̾��� ����� ���⼭ ����
            if (Input.GetKeyDown((KeyCode.Insert)))
            {
                var po = player.transform.position;
                po.y = -11.345f;
                //����
                Instantiate(enemyPrefab, po, Quaternion.identity);
            }
            //��ź ī�޶�
            if (Input.GetKeyDown((KeyCode.PageUp)))
            {
                if (isboomCamera == false)
                {
                    isboomCamera = true;    
                    boomCamera.SetActive(false);
                }
                else
                {
                    isboomCamera = false;
                    boomCamera.SetActive(true);
                }
            }

        }

    }


    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "suddenMain")
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
    }

    public void TimeScaleZero()
    {
        Time.timeScale = 0f;
        uiSetting.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void TimeScaleOne()
    {
        Time.timeScale = 1f;
        uiSetting.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void TimeScaleOneMouse()
    {
        Time.timeScale = 1f;
        uiSetting.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
   
    public void SoundSetactive(bool setactive)
    {
        uiSound.SetActive(setactive);
    }


}
