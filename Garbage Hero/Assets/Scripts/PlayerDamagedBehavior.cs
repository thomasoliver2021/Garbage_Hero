using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagedBehavior : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private int livesLeft;

    [SerializeField]
    Sprite normalPlayer;
    [SerializeField]
    Sprite damagedPlayer;
    [SerializeField]
    GameObject explosionPrefab;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        livesLeft = 3;
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
