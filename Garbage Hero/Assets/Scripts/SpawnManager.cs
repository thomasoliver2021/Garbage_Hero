using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Rate at which something spawns
    float spawnRate = 5f;

    [SerializeField]
    // Array of all spawnable objects
    // 0 = trash
    // 1 = enemy ship
    // 2 = obstacle
    GameObject[] prefabs;

    void Start(){
        // Start the spawning routine
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while(true){
            // Pick a prefab to spawn
            int choice = Random.Range(1, 10);

            // 60% chance to spawn trash
            int prefabChoice = 0;
            // 30% chance to spawn an enemy
            if(choice >= 7 && choice <= 9){
                prefabChoice = 1;
            }
            // 10% chance to spawn an obstacle
            if(choice == 10){
                prefabChoice = 2;
            }

            // Pick a random side to spawn on
            choice = Random.Range(0, 4);
            float xPos = 0;
            float yPos = 0;
            // Top
            if(choice == 0){
                xPos = Random.Range(0f, 1f);
                yPos = 1.1f;
            }
            // Bottom
            if(choice == 1){
                xPos = Random.Range(0f, 1f);
                yPos = -0.1f;
            }
            // Left
            if(choice == 2){
                xPos = -0.1f;
                yPos = Random.Range(0f, 1f);
            }
            // Right
            if(choice == 3){
                xPos = 1.1f;
                yPos = Random.Range(0f, 1f);
            }
            // Adjust the xPos and yPos to be relative to the camera viewport
            Vector2 position =  Camera.main.ViewportToWorldPoint(new Vector2(xPos, yPos));

            // Spawn the object
            Instantiate(prefabs[prefabChoice], position, Quaternion.identity);

            // Wait until next spawn is ready
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
