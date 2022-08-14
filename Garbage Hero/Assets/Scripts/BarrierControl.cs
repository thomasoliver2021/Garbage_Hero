using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierControl : MonoBehaviour
{
    List<SpriteRenderer> trashInBarrier;
    // Start is called before the first frame update
    void Start()
    {
        trashInBarrier = new List<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addTrashToArray(SpriteRenderer newTrash){
        trashInBarrier.Add(newTrash);
    }
}
