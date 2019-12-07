using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public GameObject battle_UI;
    public Image player_avatar;
    public Image enemy_avatar;

    #region Singleton
    public static BattleUI instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        battle_UI.SetActive(!battle_UI.activeSelf);
    }

    public void BattleStart(Player p, Enemy e)
    {
        battle_UI.SetActive(!battle_UI.activeSelf);
        player_avatar.sprite = p.avatar;
        enemy_avatar.sprite = e.avatar;
    }
}
