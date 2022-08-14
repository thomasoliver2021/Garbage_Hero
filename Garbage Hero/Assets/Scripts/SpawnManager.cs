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
            int choice = Random.Range(1, 11); //random has an exclusive upper end, so if you want 10 to be an option must end with 11

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

            // Pick a random spot on screen to spawn an object
            // Hard-coded for rn just to test spawning
            float xPos = Random.Range(-1.3f, 1.3f);
            float yPos = Random.Range(-0.8f, 0f);

            Vector3 position = new Vector3(xPos, yPos, 0);

            // Spawn the object
            Instantiate(prefabs[prefabChoice], position, Quaternion.identity);

            // Wait until next spawn is ready
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
