using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
    //���⽽��
    [SerializeField] GameObject ak47;
    [SerializeField] GameObject deagle;
    [SerializeField] GameObject boom;

    [SerializeField] GameManager gameManager;
    private void Start()
    {
        gameManager.OnChangeWeapon += ChangeWeapon;
    }
    void ChangeWeapon(int weaponNum)
    {
        switch (weaponNum)
        {
            case 1:

                Ak47();
                break;
            case 2:
                Deagle();
                break;
            case 3:
                Boom();
                break;

            default:
                defalut();
                break;
        }
    }


    void defalut()
    {
       
        ak47.SetActive(true);
        deagle.SetActive(false);
        if (boom != null)
        {
            boom?.SetActive(false);
        }
    }

    void Ak47()
    {
        ak47.SetActive(true);
        deagle.SetActive(false);
        if (boom != null)
        {
            boom?.SetActive(false);
        }
    }

    void Deagle()
    {
        var ds = deagle.GetComponent<WeaponDeagle>();
        ds.enabled = true;
        ak47.SetActive(false);
        deagle.SetActive(true);
        if (boom != null)
        {
            boom?.SetActive(false);
        }
    }

    void Boom()
    {
        ak47.SetActive(false);
        deagle.SetActive(false);
        if (boom != null)
        {
            boom?.SetActive(true);
        }
    }



}
