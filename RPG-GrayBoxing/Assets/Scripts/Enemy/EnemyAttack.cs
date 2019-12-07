using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyWeapon weapon;
    private Enemy enemy;
    private Player player;

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log(transform.name + " is hitting the Player");
            MeleeAttack();
        }
    }

    void MeleeAttack()
    {
        if (player == null)
            player = GameManager.GetPlayer();

        if (player.isAlive == true)
        {
            player.DisableOnEncounter();
            enemy.DisableOnEncounter();
            BattleUI.instance.BattleStart(player, enemy);
        }
    }
}
