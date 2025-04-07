using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{       
   

    [Header("Setting")]
    [SerializeField] private float range;
    [SerializeField] protected LayerMask enemyMask;


    [Header("Attack")]
    [SerializeField] protected int damage;
    [SerializeField] protected float attackDelay;
    [SerializeField] protected Animator animator;

    [Header("Animations")]
    [SerializeField] protected float aimLerp;

    protected float attackTimer;



    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       

       
    } 

    
    
    protected Enemy GetClosestEnemy()
    {
        Enemy closestEnemy = null;
        Vector2 targetUpVector = Vector3.up;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);

        if (enemies.Length <= 0)
                    
            return null;
        
        float minDistance = range;

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemyChecked = enemies[i].GetComponent<Enemy>();

            float distanceToEnemy = Vector2.Distance(transform.position, enemyChecked.transform.position);

            if (distanceToEnemy < minDistance)
            {
                closestEnemy = enemyChecked;
                minDistance = distanceToEnemy;               
            }
        }

        return closestEnemy;
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);
    
    }
}
