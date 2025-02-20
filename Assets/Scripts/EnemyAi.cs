using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour, IDamageable
{
    public float enemyHp = 100;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();    
    }

    public void GetDamge(int damage)
    {
        enemyHp -= damage;
        Debug.Log(enemyHp);
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



