using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(EnemyMovement))]

public class Enemy : MonoBehaviour
{

    [Header("Components")]
    private EnemyMovement movement;


    [Header("Health")]
    [SerializeField] private int maxHealth;
    private int health;
    [SerializeField] private TextMeshPro healthText;

    [Header("Elements")]
    private Player player;

    [Header("Spawn Sequence Related ")]
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private SpriteRenderer spawnIndicator;
    private bool hasSpawned;


    [Header("Effects")]
    [SerializeField] private ParticleSystem passAwayParticles;

    [Header("Attack")]
    [SerializeField] private int damage;
    [SerializeField] private float attackFreqwency;
    [SerializeField] private float playerDetectionRadiusd;
    private float attackDelay;
    private float attckTimer;

    [Header("DEBUG")]
    [SerializeField] private bool gizmos;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthText.text = health.ToString();

        movement = GetComponent<EnemyMovement>();

        player = FindFirstObjectByType<Player>();
        if (player == null)
        {
            Debug.LogWarning("No player found,Auto-destroying...");
            Destroy(gameObject);        
        }
        StartSpawnSequence();

        attackDelay = 1f / attackFreqwency;
        Debug.Log("Attack Delay :" + attackDelay);
    }

    private void StartSpawnSequence()
    {
        SetRenderersVisibility(false);


        Vector3 targetScale = spawnIndicator.transform.localScale * 1.2f;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, .3f)
            .setLoopPingPong(4)
            .setOnComplete(SpawnSequenceCompleted);
             
    }
    private void SpawnSequenceCompleted()
    {
        SetRenderersVisibility();
        hasSpawned = true;

        movement.StorePlayer(player);
    }

    // Update is called once per frame
    void Update()
    {
        if (attckTimer >= attackDelay)
            TrayAttack();
        else
            Wait();
    }

    private void SetRenderersVisibility(bool visibility = true)
    {
        renderer.enabled = visibility;
        spawnIndicator.enabled = !visibility;
    }

    private void Wait()
    {
        attckTimer += Time.deltaTime;
    }

    private void TrayAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= playerDetectionRadiusd)
            Attack();


    }
    private void Attack()
    {
                attckTimer = 0;
        player.TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(damage, health);
        health -= realDamage;

        healthText.text = health.ToString();

        if (health <= 0)
            PassAway();
    }
    private void PassAway()
    {
        // Unparent the particles & play them
        passAwayParticles.transform.SetParent(null);
        passAwayParticles.Play();

        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        if (!gizmos)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadiusd);

        
    }
}
