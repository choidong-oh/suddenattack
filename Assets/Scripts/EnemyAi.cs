using System;
using System.Collections;
using UnityEngine;

public class EnemyAi : MonoBehaviour, IDamageable
{
    int enemyHp = 1000;
    Animator animator;

    //이벤트//걍 인트받는 델리게이트
    public event Action<int> OnHealthChanged;

    private void Start()
    {
        animator = GetComponent<Animator>();    
    }

    public void GetDamge(int damage)
    {
        enemyHp -= damage;

        //이벤트
        OnHealthChanged?.Invoke(enemyHp);

        StartCoroutine(hit());
        if(enemyHp < 0)
        {
            enemyHp = 0;
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



