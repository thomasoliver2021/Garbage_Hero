using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierControl : MonoBehaviour
{
    List<SpriteRenderer> trashInBarrier;
    SpriteRenderer[] toShoot;
    GameObject player;
    [SerializeField] Sprite[] shotSprites;

    List<SpriteRenderer[]> bullets;

    List<Vector2> shootDirections;

    bool shooting = false;
    bool shot = false;

    float shootSpeed = 1.5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        trashInBarrier = new List<SpriteRenderer>();
        toShoot = new SpriteRenderer[3];
        bullets = new List<SpriteRenderer[]>();
        shootDirections = new List<Vector2>();
    }

    void Update()
    {
        // Initial radius and theta for the spiral
        float radius = 0.15f;
        float theta = 0f;
        foreach(var trash in trashInBarrier){
            // Calculate the x and y positions for every piece of trash in the barrier
            float xPos = player.transform.position.x + (radius * Mathf.Cos(theta));
            float yPos = player.transform.position.y + (radius * Mathf.Sin(theta));

            // Move trash to proper location
            trash.transform.position = new Vector2(xPos, yPos);

            // Increment the angle
            theta += 0.6f;
            // Increment the radius
            radius += 0.01f;
        }

        if(Input.GetKeyDown("space")){
            if(trashInBarrier.Count >= 3){
                for(int i = 0; i < 3; i++){
                    toShoot[i] = trashInBarrier[trashInBarrier.Count - 1];
                    toShoot[i].sprite = shotSprites[(int)char.GetNumericValue(toShoot[i].sprite.name[6])];
                    trashInBarrier.RemoveAt(trashInBarrier.Count - 1);
                }
                shooting = true;
            }
        }

        if(shooting){
            foreach(var trash in toShoot){
                trash.transform.position = Vector2.MoveTowards(trash.transform.position, player.transform.position, shootSpeed / 15);
            }
            if(toShoot[0].transform.position.x == toShoot[1].transform.position.x &&
               toShoot[1].transform.position.x == toShoot[2].transform.position.x){
                SpriteRenderer[] bullet = new SpriteRenderer[3];
                for(int i = 0; i < 3; i++){
                    bullet[i] = toShoot[i];
                    toShoot[i] = null;
                }
                bullet[1].gameObject.AddComponent<BoxCollider2D>();
                bullets.Add(bullet);
                shootDirections.Add(player.transform.up);
                shooting = false;
                shot = true;
            }
        }

        if(shot){
            for(int i = 0; i < bullets.Count; i++){
                foreach(var bullet in bullets[i]){
                    bullet.transform.Translate(shootDirections[i] * Time.deltaTime * shootSpeed, Space.World);
                }
            }
        }

    }

    public void addTrashToArray(SpriteRenderer newTrash){
        // Add trash from space into the barrier
        trashInBarrier.Add(newTrash);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "enemy"){
            collision.gameObject.SendMessage("OnTrashHit");
        }
    }
}
