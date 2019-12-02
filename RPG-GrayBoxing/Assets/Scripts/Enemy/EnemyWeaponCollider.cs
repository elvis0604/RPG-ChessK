using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponCollider : MonoBehaviour
{
    public void WeaponDisable()
    {
        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;
    }
}
