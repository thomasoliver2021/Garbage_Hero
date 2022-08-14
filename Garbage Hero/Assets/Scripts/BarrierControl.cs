using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierControl : MonoBehaviour
{
    List<SpriteRenderer> trashInBarrier;
    SpriteRenderer[] toShoot;
    GameObject player;
    [SerializeField] Sprite[] shotSprites;

    bool shooting = false;
    bool shot = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        trashInBarrier = new List<SpriteRenderer>();
        toShoot = new SpriteRenderer[3];
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
            }
        }

        foreach(var trash in toShoot){

        }

    }

    public void addTrashToArray(SpriteRenderer newTrash){
        // Add trash from space into the barrier
        trashInBarrier.Add(newTrash);
    }
}
