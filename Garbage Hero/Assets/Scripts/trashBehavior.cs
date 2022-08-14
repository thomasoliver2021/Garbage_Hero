using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBehavior : MonoBehaviour
{
    // Speed at which the trash rotates
    float rotationSpeed;

    // Speed at which the trash moves
    float moveSpeed = 0.2f;

    // Direction trash moves in
    Vector2 movementDirection;
    bool grabbed;
    float grabbingRange = 0.5f;
    GameObject player;

    int spriteTexture;
    SpriteRenderer spriteRenderer;

    // Array of trash sprites
    [SerializeField] Sprite[] trashSprites;
    [SerializeField] Sprite[] grabbedTrashSprites;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Pick a random trash to be
        spriteTexture = Random.Range(0, trashSprites.Length);

        // Set sprite
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = trashSprites[spriteTexture];

        // Randomize rotation speed
        rotationSpeed = Random.Range(0, 20);
        // Chance to rotate the opposite way
        if(rotationSpeed < 10){
            rotationSpeed += 10;
            rotationSpeed *= -1;
        }

        movementDirection = player.transform.position - transform.position;

    }

    void Update()
    {
        // Slowly rotate
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        
        if(grabbed){
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed / 15);
            float distance = Vector2.Distance(gameObject.transform.position, player.transform.position);
            if (distance < 0.05){
                moveToBarrier();
            }
        }
        else{
            transform.Translate(movementDirection * Time.deltaTime * moveSpeed, Space.World);
        }

        if (Input.GetMouseButton(0))
        {
            float distance = Vector2.Distance(gameObject.transform.position, player.transform.position);
            if (distance < grabbingRange && !grabbed){
                grabbed = true;
                spriteRenderer.sprite = grabbedTrashSprites[spriteTexture];
            }
        }
    }

    void moveToBarrier(){
        player.GetComponent<BarrierControl>().addTrashToArray(spriteRenderer);
        enabled = false;
    }
}
