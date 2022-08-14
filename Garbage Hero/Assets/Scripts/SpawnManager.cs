using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    float spawnRate = 5f;
    [SerializeField]
    GameObject trashPrefab;

    void Start(){
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while(true){
            float xPos = Random.Range(-2f, 2f);
            float yPos = Random.Range(-1f, 0f);

            Vector3 position = new Vector3(xPos, yPos, 0);

            Instantiate(trashPrefab, position, Quaternion.identity);

            yield return new WaitForSeconds(spawnRate);
        }
    }
}
