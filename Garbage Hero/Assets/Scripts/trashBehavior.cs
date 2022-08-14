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

    bool inGrabbingRange = false;
    float grabbingRange = 0.5f;
    GameObject player;

    
    [SerializeField]
    // Array of trash sprites
    Sprite[] trashSprites;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Pick a random trash to be
        int spriteTexture = Random.Range(0, trashSprites.Length);

        // Set sprite
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
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
        
        transform.Translate(movementDirection * Time.deltaTime * moveSpeed, Space.World);

        if (Input.GetMouseButton(0))
        {
            float distance = Vector2.Distance(gameObject.transform.position, player.transform.position);
            if (distance < grabbingRange){
                //print("HERE");
            }
        }
        

    }
}
