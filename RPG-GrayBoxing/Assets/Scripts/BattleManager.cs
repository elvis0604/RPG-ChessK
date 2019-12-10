using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public enum BattleState { START, PLAYERTURN, ENEMYTURN, TRANSITION, WON, LOST }

    public BattleState state;
    public GameObject battle_UI;
    public GameObject attack_button;
    public GameObject run_button;

    private Player player;
    private Enemy enemy;
    bool isAlive = true;
    public Text dialogue_text;

    public PlayerBattleHUD player_hud;
    public EnemyBattleHUD enemy_hud;


    #region Singleton
    public static BattleManager instance;

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

    void InitializeUI()
    {
        if(battle_UI.activeSelf == false)
            battle_UI.SetActive(!battle_UI.activeSelf);

        attack_button.SetActive(!attack_button.activeSelf);
        run_button.SetActive(!run_button.activeSelf);
    }

    public void SetupBattle(Player p, Enemy e)
    {
        player = p;
        enemy = e;

        state = BattleState.START;
        InitializeUI();

        dialogue_text.text = e.enemy_name + " approaches you...";

        player_hud.SetHUD(player);
        enemy_hud.SetHUD(enemy);

        StartCoroutine(PlayerTurn());
    }

    IEnumerator PlayerTurn()
    {
        yield return new WaitForSeconds(2f);

        if (state == BattleState.START)
            InitializeUI();

        state = BattleState.PLAYERTURN;
        dialogue_text.text = "Choose an action: ";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        int hp_left = enemy.GetCurHp() - player.weapon.damage;
        state = BattleState.TRANSITION;

        while (enemy.GetCurHp() > hp_left)
        {
            isAlive = enemy.TakeDamage(1);
            yield return new WaitForSeconds(0.05f);
            enemy_hud.SetHP(enemy.GetCurHp());
        }
        dialogue_text.text = "The attack is successful";

        yield return new WaitForSeconds(2f);

        if (!isAlive)
        {
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogue_text.text = "You have won!";

            yield return new WaitForSeconds(2.5f);
            dialogue_text.text = enemy.enemy_name + " died...";
            yield return new WaitForSeconds(2.5f);
            dialogue_text.text = "You have gained " + enemy.GetExpGained();

            int exp_gained = player.GetCurExp() + enemy.GetExpGained();
            while (player.GetCurExp() < exp_gained)
            {
                player.AddExp(1);
                yield return new WaitForSeconds(0.1f);
                player_hud.SetEXP(player.GetCurExp());
            }

            yield return new WaitForSeconds(2.5f);
            battle_UI.SetActive(!battle_UI.activeSelf);
        }
        else if (state == BattleState.LOST)
            dialogue_text.text = "You have lost!";

    }

    IEnumerator EnemyTurn()
    {
        dialogue_text.text = enemy.enemy_name + " attacks!";

        state = BattleState.TRANSITION;
        yield return new WaitForSeconds(1f);
        int hp_left = player.GetCurHp() - enemy.weapon.damage;

        while (player.GetCurHp() > hp_left)
        {
            isAlive = player.TakeDamage(1);
            yield return new WaitForSeconds(0.05f);
            player_hud.SetHP(player.GetCurHp());
        }

        yield return new WaitForSeconds(1f);
        if (!isAlive)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        } else
        {
            StartCoroutine(PlayerTurn());
        }
    }
}
