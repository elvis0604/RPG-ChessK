using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int max_hp = 100;

    [SerializeField]
    private int exp = 0;

    private int exp_to_next_level = 100;
    private int cur_hp;
    private int level;
    private int max_level;

    public string player_name = "Catty";
    public Sprite level_icon = null;
    public Sprite avatar = null;
    public GameObject damaged_indication_UI;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;  //Save for respawn


    private bool alive = true;
    public bool isAlive
    {
        get { return alive; }
        protected set { alive = value; }
    }

    void Start()
    {
        level = 0;
        level_icon = GameManager.instance.level_progress_white[level];
        avatar = GameManager.instance.level_progress_white[level];
        max_level = GameManager.instance.level_progress_white.Length; //set max level as the length of level progress
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            AddExp(100);
        }
    }

    public void Setup()
    {
        wasEnabled = new bool[disableOnDeath.Length];

        for (int i = 0; i < wasEnabled.Length; i++) //Store default setting
            wasEnabled[i] = disableOnDeath[i].enabled;

        SetDefault();
    }

    public void Heal(int heal)
    {
        if (cur_hp <= max_hp - heal)
            cur_hp += heal;
        else
            cur_hp = cur_hp + (max_hp - cur_hp);
    }

    public float GetHealthPct()
    {
        return (float)cur_hp / max_hp;
    }

    public void TakeDamage(int damage)
    {
        if (!isAlive)
            return;

        cur_hp -= damage;

        damaged_indication_UI.SetActive(true);
        StartCoroutine(DeactiveUI());

        if (cur_hp <= 0)
        {
            PlayerKilled(transform.name);
            cur_hp = 0;
        }
    }

    public void AddExp(int amount)
    { 
        if (level < max_level - 1)
            exp += amount;
        if (exp >= exp_to_next_level)
        {
            Debug.Log("Player level up!");
            LevelUp();
        }
    }

    public int GetLevel()
    {
        return level;
    }

    private void LevelUp()
    {
        level++;
        level_icon = GameManager.instance.level_progress_white[level];
        avatar = GameManager.instance.level_progress_white[level];
        exp -= exp_to_next_level;
        exp_to_next_level = exp_to_next_level * 2;
    }

    IEnumerator DeactiveUI()
    {
        yield return new WaitForSeconds(0.3f);
        damaged_indication_UI.SetActive(false);
    }

    public void PlayerKilled(string id) //todo Gameover screen
    {
        isAlive = false;

        GameManager.instance.GameOver();
        Disable();

    }

    public void Disable()
    {
        for (int i = 0; i < disableOnDeath.Length; i++) //Deactivate default setting on death
            disableOnDeath[i].enabled = false;

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
    }

    public void SetDefault()
    {
        isAlive = true;

        cur_hp = max_hp;

        for (int i = 0; i < disableOnDeath.Length; i++) //Load default setting
            disableOnDeath[i].enabled = wasEnabled[i];
    }
}
