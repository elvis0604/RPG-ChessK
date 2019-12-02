using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public GameObject object_prefab;
    
    public void Spawn()
    {
        Instantiate(object_prefab, transform.position, transform.rotation);
    }
}
