using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
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
            Encounter();
        }
    }

    void Encounter()
    {
        if (player == null)
            player = GameManager.GetPlayer();

        if (player.isAlive == true)
        {
            player.DisableOnEncounter();
            enemy.DisableOnEncounter();
            BattleManager.instance.SetupBattle(player, enemy);
        }
    }
}
