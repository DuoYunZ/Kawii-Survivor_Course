using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


[RequireComponent(typeof(EnemyMovement))]

public class MeleeEnemy : Enemy
{    
    [Header("Attack")]
    [SerializeField] private int damage;
    [SerializeField] private float attackFreqwency;    
    private float attackDelay;
    private float attckTimer;

    // Start is called before the first frame update
    protected override void Start()
    {                     
        base.Start();
        attackDelay = 1f / attackFreqwency;
    }
       

    // Update is called once per frame
    void Update()
    {
        if (!CanAttack())
            return;

        if (attckTimer >= attackDelay)
            TryAttack();
        else
            Wait();

        movement.FollowPlayer();
    }


    private void Wait()
    {
        attckTimer += Time.deltaTime;
    }

    private void TryAttack()
    {
            
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
               
            if (distanceToPlayer <= playerDetectionRadius)
                Attack();        
    }
    private void Attack()
    {
        attckTimer = 0;
        player.TakeDamage(damage);
    }       
}
