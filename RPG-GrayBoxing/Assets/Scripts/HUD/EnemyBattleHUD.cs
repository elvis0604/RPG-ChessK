using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBattleHUD : MonoBehaviour
{
    public Text name_text;
    public Slider hp_slider;
    public Image enemy_avatar;

    public void SetHUD(Enemy e)
    {
        name_text.text = e.enemy_name;
        hp_slider.maxValue = e.GetMaxHp();
        hp_slider.value = e.GetCurHp();
        enemy_avatar.sprite = e.avatar;
    }

    public void SetHP(int hp)
    {
        hp_slider.value = hp;
    }
}
