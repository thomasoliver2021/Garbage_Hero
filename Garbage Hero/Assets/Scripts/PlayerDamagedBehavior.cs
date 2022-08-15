using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
    [SerializeField]
    ScoreTracker scoreTracker;
    [SerializeField]
    GameObject gameOverText;
    [SerializeField]
    AudioClip damagesfx;
    [SerializeField]
    TextMeshProUGUI livestext;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        livesLeft = 3;
        numOfTrashInBarrier = 0;
        gameOverText.SetActive(false);
    }

    void TakeDamage()
    {
        GetComponent<AudioSource>().clip = damagesfx;
        GetComponent<AudioSource>().Play();
        
        //handle collision to garbage if any

        livesLeft--;
        livestext.text = "lives: " + livesLeft;
        if (livesLeft > 0) StartCoroutine(FlashRed());
        else StartCoroutine(Die());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "enemy")
        {
            if(numOfTrashInBarrier >= 1){
                GetComponent<BarrierControl>().scatterTrash(1);
            }
            else{
                TakeDamage();
            }
        }
        if(collision.collider.tag == "laser")
        {
            if(numOfTrashInBarrier >= 1){
                GetComponent<BarrierControl>().scatterTrash(2);
            }
            else{
                TakeDamage();
            }
        }
        if(collision.collider.tag == "asteroid")
        {
            if(numOfTrashInBarrier >= 1){
                GetComponent<BarrierControl>().scatterTrash(3);
            }
            else{
                TakeDamage();
            }
        }
    }

    public void updateColliderSize(){
        if(numOfTrashInBarrier == 0){
            circleCollider.radius = 0.08f;
        }
        else{
            circleCollider.radius = 0.2f;
            for(int i = 0; i < numOfTrashInBarrier - 1; i++){
                circleCollider.radius += 0.02f;
            }
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
