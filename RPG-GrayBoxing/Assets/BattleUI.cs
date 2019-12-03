using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    public GameObject battle_UI;
    // Start is called before the first frame update
    void Start()
    {
        battle_UI.SetActive(!battle_UI.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
