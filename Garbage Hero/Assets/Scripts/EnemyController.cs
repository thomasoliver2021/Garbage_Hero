using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;

    private float speed;
    private float distanceToPlayer;
    private float stoppingRange;
    private float shootingRange;
    private bool inStoppingRange;
    private bool inShootingRange;
    private bool currentlyShooting;
    private int enemyType;

    [SerializeField]
    Sprite[] sprites;
    [SerializeField]
    GameObject explosionPrefab;
    [SerializeField]
    GameObject laserPrefab;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();

        enemyType = Random.Range(0, 2);
        if (enemyType == 0) spriteRenderer.sprite = sprites[0];
        else spriteRenderer.sprite = sprites[2];

        speed = Random.Range(.6f, 1.1f);
        shootingRange = Random.Range(1f, 1.5f);
        stoppingRange = Random.Range(0.2f, shootingRange-0.6f);

        inStoppingRange = false;
        inShootingRange = false;
        currentlyShooting = false;
        SetDistance();
    }
    
    void Update()
    {
        SetDistance();
        gameObject.transform.up = player.transform.position - gameObject.transform.position;
        if (!inStoppingRange) MoveTowardsPlayer();
        else if(rigidBody.velocity.magnitude > 0) SlowMovement();

        if (inShootingRange && !currentlyShooting) { InvokeRepeating("FireLaser", 0.5f, 1.5f); currentlyShooting = true; }
        else if (!inShootingRange && currentlyShooting) { CancelInvoke("FireLaser"); currentlyShooting = false; }
    }

    void SetDistance()
    {
        distanceToPlayer = Vector2.Distance(gameObject.transform.position, player.transform.position);
        inStoppingRange = (distanceToPlayer <= stoppingRange);
        inShootingRange = (distanceToPlayer <= shootingRange);
    }

    void MoveTowardsPlayer()
    {
        rigidBody.velocity = (player.transform.position - gameObject.transform.position) * speed;
    }

    void SlowMovement()
    {
        rigidBody.velocity *= 0.7f;
        if (rigidBody.velocity.magnitude < 0.03) rigidBody.velocity *= 0.0f;
    }

    void FireLaser()
    {
        Instantiate(laserPrefab, rigidBody.transform.position + rigidBody.transform.up * 0.12f, rigidBody.transform.rotation);
    }

    void Die()
    {
        spriteRenderer.sprite = (enemyType == 0) ? sprites[1] : sprites[3];
        StartCoroutine(TriggerExplosion());
    }

    IEnumerator TriggerExplosion()
    {
        yield return new WaitForSeconds(0.4f);
        Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void OnTrashHit()
    {
        Die();
    }

    public void OnAstroidHit()
    {
        Die();
    }
}
