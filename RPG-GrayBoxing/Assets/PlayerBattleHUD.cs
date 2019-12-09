using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleHUD : MonoBehaviour
{
    public Text name_text;
    public Text level_text;
    public Slider hp_slider;
    public Slider exp_slider;
    public Image player_avatar;

    public void SetHUD(Player p)
    {
        name_text.text = p.player_name;
        level_text.text = "Lvl " + (p.GetLevel() + 1);
        hp_slider.maxValue = p.GetMaxHp();
        hp_slider.value = p.GetCurHp();
        exp_slider.maxValue = p.GetMaxExp();
        exp_slider.value = p.GetCurExp();
        player_avatar.sprite = p.avatar;
    }

    public void SetHP(int hp)
    {
        hp_slider.value = hp;
    }
}
