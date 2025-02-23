using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //스크립트 달려있는거 넣어야함
    [SerializeField] private EnemyAi enemyAi;
    [SerializeField] private WeaponHit weaponHit;

    [SerializeField] private inventory inventory;
    public event Action<int> OnChangeWeapon;


    private void Update()
    {
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
    }




}
