using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    Animator animator;
    Collider col; 

    void Start()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<Collider>();
    }

    public override void Interaction()
    {
        base.Interaction();
        if (Inventory.instance.key_found)
        {
            Open();
        }
        else
            Debug.Log("Full key is not found");
    }

    void Open()
    {
        animator.SetTrigger("Open");
        if (col != null)
            col.enabled = false;
    }
}
