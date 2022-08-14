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
    private Vector2 movementDirection;

    
    [SerializeField]
    // Array of trash sprites
    Sprite[] trashSprites;

    void Start()
    {
        // Pick a random trash to be
        int spriteTexture = Random.Range(0, trashSprites.Length);

        // Set sprite
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = trashSprites[spriteTexture];

        // Randomize rotation speed
        rotationSpeed = Random.Range(0, 20);
        // Chance to rotate the opposite way
        if(rotationSpeed < 10){
            rotationSpeed += 10;
            rotationSpeed *= -1;
        }

        movementDirection = GameObject.FindGameObjectWithTag("Player").transform.position - gameObject.transform.position;

    }

    void Update()
    {
        // Slowly rotate
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        
        transform.Translate(movementDirection * Time.deltaTime * moveSpeed, Space.World);

    }
}
