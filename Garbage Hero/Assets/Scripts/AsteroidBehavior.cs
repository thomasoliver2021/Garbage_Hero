using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Renderer renderer;

    private float speed;
    private Vector2 movementDirection;
    private int asteroidType;
    //private bool 

    [SerializeField]
    Sprite[] sprites;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        renderer = gameObject.GetComponent<Renderer>();

        asteroidType = Random.Range(0, 2);
        if (asteroidType == 0) spriteRenderer.sprite = sprites[0];
        else spriteRenderer.sprite = sprites[2];

        speed = Random.Range(0.1f, 0.4f);

        gameObject.transform.up = GameObject.FindGameObjectWithTag("Player").transform.position - gameObject.transform.position;
        gameObject.transform.Rotate(0, 0, Random.Range(-20, 20));
        movementDirection = gameObject.transform.up;
    }

    void Update()
    {
        gameObject.transform.Translate(movementDirection * Time.deltaTime * speed, Space.World);
        gameObject.transform.Rotate(Vector3.forward * Time.deltaTime * speed * 100);
        // if (!renderer.isVisible) Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            movementDirection = collision.collider.transform.up;
            speed *= 3.5f;
        }
        else if(collision.collider.tag == "enemy") collision.gameObject.SendMessage("OnAstroidHit");
    }

}
