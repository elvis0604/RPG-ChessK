using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int max_hp = 100;

    [SerializeField]
    private int exp_obtained = 20;

    private float alert_radius = 15f;
    private float alert_time = 5f;
    private int cur_hp;

    public string enemy_name = null;
    public Sprite avatar = null;

    private EnemyController controller;
    public EnemyWeapon weapon;

    [SerializeField]
    private Behaviour[] disableOnDeath;

    [SerializeField]
    private Behaviour[] disableOnEncounter;

    private bool[] wasEnabled;  //save for respawn

    private bool alive = true;
    public bool isAlive
    {
        get { return alive; }
        protected set { alive = value; }
    }  

    void Awake()
    {
        enemy_name = avatar.name;
    }

    void Start()
    {
        controller = GetComponent<EnemyController>();
    }

    public void Setup()
    {
        wasEnabled = new bool[disableOnDeath.Length];

        for (int i = 0; i < wasEnabled.Length; i++) //store default setting
            wasEnabled[i] = disableOnDeath[i].enabled;

        SetDefault();
    }

    public bool TakeDamage(int damage)
    {
        cur_hp -= damage;

        Alert();

        if (cur_hp <= 0)
        {
            EnemyKilled(transform.name);
            cur_hp = 0;
            return isAlive;
        }

        return isAlive;
    }

    public void EnemyKilled(string id)
    {
        isAlive = false;
        Disable();

        Debug.Log(transform.name + " is DEAD");
        GameManager.UnregisterEnemy(id);
        Invoke("Destroy", 3);
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    #region disablecomponent
    public void Disable()
    {
        for (int i = 0; i < disableOnDeath.Length; i++) //deactivate default setting on death
            disableOnDeath[i].enabled = false;

        Collider col = GetComponent<Collider>();
        if (col != null)
            col.enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;

        EnemyAttack attack = GetComponent<EnemyAttack>();
        if (attack != null)
            attack.enabled = false;

        EnemyController controller = GetComponent<EnemyController>();
        if (controller != null)
            controller.enabled = false;

        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null)
            agent.enabled = false;
    }

    public void DisableOnEncounter()
    {
        for (int i = 0; i < disableOnEncounter.Length; i++)
        {
            disableOnEncounter[i].enabled = false;
        }
    }
    #endregion

    public void SetDefault()
    {
        isAlive = true;

        cur_hp = max_hp;

        for (int i = 0; i < disableOnDeath.Length; i++) //load default setting
            disableOnDeath[i].enabled = wasEnabled[i];
    }

    public void Alert()
    {
        controller.detected = true;
    }

    #region Getter
    public int GetCurHp()
    {
        return cur_hp;
    }

    public int GetMaxHp()
    {
        return max_hp;
    }

    public int GetExpGained()
    {
        return exp_obtained;
    }
    #endregion
}
