using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [Header("Elements")]
    private Player player;

    [Header("Spawn Sequence Related ")]
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private SpriteRenderer spawnIndicator;
    private bool hasSpawned;

    [Header("Setting")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float playerDetectionRadiusd;

    [Header("Elements")]
    [SerializeField] private ParticleSystem passAwayParticles;

    [Header("DEBUG")]
    [SerializeField] private bool gizmos;


    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<Player>();
        if(player == null)
        {
            Debug.LogWarning("No player found,Auto-destroying...");
            Destroy(gameObject);
        }


        renderer.enabled = false;
        spawnIndicator.enabled = true;
        Vector3 targetScale = spawnIndicator.transform.localScale * 1.2f;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, .3f)
            .setLoopPingPong(4)
            .setOnComplete(SpawnSequenceCompleted);

    }

    // Update is called once per frame
    void Update()
    {

        if (!hasSpawned)
            return;

       
            FollowPlayer();
            TrayAttack();
        
    }

    private void SpawnSequenceCompleted()
    {
        renderer.enabled = true;

        spawnIndicator.enabled = false;

        hasSpawned = true;
    }
    private void FollowPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        Vector2 targetPosition = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;

        transform.position = targetPosition;
    }
    private void TrayAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if(distanceToPlayer <=  playerDetectionRadiusd
)
        {
            PassAway();
          
        }
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
