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
        bool isAlive = enemy.TakeDamage(player.weapon.damage);
        Debug.Log(isAlive);
        enemy_hud.SetHP(enemy.GetCurHp());
        dialogue_text.text = "The attack is successful";

        state = BattleState.TRANSITION;
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
            battle_UI.SetActive(!battle_UI.activeSelf);
        }
        else if (state == BattleState.LOST)
            dialogue_text.text = "You have lost!";

    }

    IEnumerator EnemyTurn()
    {
        dialogue_text.text = enemy.enemy_name + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isAlive = player.TakeDamage(enemy.weapon.damage);
        player_hud.SetHP(player.GetCurHp());

        state = BattleState.TRANSITION;
        yield return new WaitForSeconds(1f);
        Debug.Log(isAlive);
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
