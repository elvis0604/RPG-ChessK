﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public Player player;
    
    static Dictionary<string, Enemy> enemies = new Dictionary<string, Enemy>();

    public GameObject gameoverUI;
    public GameObject[] UI;
    public Sprite[] level_progress_white;

    private bool UI_on = true;

    #region Singleton
    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public static void RegisterPlayer(Player p)
    {
        player = p;
        player.transform.name = p.player_name;
    }

    public static void RegisterEnemy(string enemy_id, Enemy enemy)
    {
        enemies.Add(enemy_id, enemy);
        enemy.transform.name = enemy_id;
    }

    public static Enemy GetEnemy(string enemy_id)
    { 
        return enemies[enemy_id];
    }

    public static Player GetPlayer()
    {
        return player;
    }

    public static void UnregisterEnemy(string enemy_id)
    {
        Debug.Log("Unregistering: " + enemy_id);
        enemies.Remove(enemy_id);
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER");
        gameoverUI.SetActive(true);
        Invoke("Restart", 3f);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (UI_on == true)
            {
                foreach (GameObject ob in UI)
                    ob.SetActive(false);
                UI_on = false;
            }
            else
            {
                foreach (GameObject ob in UI)
                    ob.SetActive(true);
                UI_on = true;
            }
        }
    }

    public GameObject player_object;
}
