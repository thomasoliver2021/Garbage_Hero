using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagedBehavior : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    CircleCollider2D circleCollider;
    private int livesLeft;

    public int numOfTrashInBarrier;

    [SerializeField]
    Sprite normalPlayer;
    [SerializeField]
    Sprite damagedPlayer;
    [SerializeField]
    GameObject explosionPrefab;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        livesLeft = 3;
        numOfTrashInBarrier = 0;
    }

    void TakeDamage()
    {
        //handle collision to garbage if any

        livesLeft--;
        if (livesLeft > 0) StartCoroutine(FlashRed());
        else StartCoroutine(Die());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "enemy")
        {
            TakeDamage();
        }
    }

    void OnLaserBulletHit()
    {
        TakeDamage();
    }

    public void updateColliderSize(){
        if(numOfTrashInBarrier == 0){
            circleCollider.radius = 0.08f;
        }
        if(numOfTrashInBarrier == 1){
            circleCollider.radius = 0.2f;
        }
        for(int i = 0; i < numOfTrashInBarrier - 1; i++){
            circleCollider.radius += (0.2f * i);
        }
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.sprite = damagedPlayer;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.sprite = normalPlayer;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.sprite = damagedPlayer;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.sprite = normalPlayer;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.sprite = damagedPlayer;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.sprite = normalPlayer;
    }

    IEnumerator Die()
    {
        spriteRenderer.sprite = damagedPlayer;
        yield return new WaitForSeconds(0.4f);
        Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
