using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBehavior : MonoBehaviour
{
    // Speed at which the trash rotates
    float rotationSpeed;
    
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
            print(rotationSpeed);
        }

    }

    void Update()
    {
        // Slowly rotate
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
    }
}
