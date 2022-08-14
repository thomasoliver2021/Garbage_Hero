using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trashBehavior : MonoBehaviour
{
    // Speed at which the trash rotates. MAKE PRIVATE, public rn for testing
    public float rotationSpeed = 10;
    
    [SerializeField]
    // Array of trash sprites
    Sprite[] trashSprites;

    void Start()
    {
        // Pick a random trash to be
        int spriteTexture = Random.Range(0, trashSprites.Length - 1);

        // Set sprite
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = trashSprites[spriteTexture];
    }

    void Update()
    {
        // Slowly rotate
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
    }
}
