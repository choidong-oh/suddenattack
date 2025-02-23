using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class EnemyAi : MonoBehaviour, IDamageable
{
    public int enemyHp = 1000;
    Animator animator;

    public  int EnemyHp
    {
        get { return enemyHp; }
        set
        {
            enemyHp = value;
            if (enemyHp <= 0)
            {
                enemyHp = 0;
            }
        }
    }

    //이벤트//걍 인트받는 델리게이트
    public event Action<int> OnHealthChanged;



    private void Start()
    {
        animator = GetComponent<Animator>();    
    }

    public void GetDamge(int damage)
    {
        EnemyHp -= damage;

        //이벤트
        OnHealthChanged?.Invoke(EnemyHp);

        StartCoroutine(hit());
        if(EnemyHp <= 0)
        {
            EnemyHp = 0;
            Destroy(gameObject);    
        }

    }

    IEnumerator hit()
    {
        animator.SetTrigger("Hit");
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("Hit");
    }

}



