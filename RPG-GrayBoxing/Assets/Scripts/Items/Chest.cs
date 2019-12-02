using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    Animator animator;
    Collider col;
    public SpawnItem[] spawns;

    void Start()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<Collider>();
        //spawn = GetComponentInChildren<SpawnItem>();
    }

    public override void Interaction()
    {
        base.Interaction();
        Open();
    }

    void Open()
    {
        animator.SetTrigger("Open");
        if (col != null)
            col.enabled = false;
        foreach (SpawnItem s in spawns)
            s.Spawn();
    }
}
