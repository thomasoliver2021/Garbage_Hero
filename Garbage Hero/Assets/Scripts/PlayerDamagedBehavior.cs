using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    ScoreTracker scoreTracker;
    [SerializeField]
    GameObject gameOverText;
    [SerializeField]
    AudioClip damagesfx;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        livesLeft = 3;
        gameOverText.SetActive(false);
    }

    void TakeDamage()
    {
        GetComponent<AudioSource>().clip = damagesfx;
        GetComponent<AudioSource>().Play();
        
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
        GetComponent<PlayerMovement>().StopPlayer();
        gameOverText.SetActive(true);
        spriteRenderer.sprite = damagedPlayer;
        yield return new WaitForSeconds(0.4f);
        Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
        spriteRenderer.enabled = false;
        yield return scoreTracker.SubmitScoreToLeaderboard();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("HighScores");
    }
}
