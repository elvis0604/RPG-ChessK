using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    RectTransform hp_fill;

    private Player player;

    public GameObject map_UI;

    void Start()
    {
        map_UI.SetActive(!map_UI.activeSelf);
    }

    void SetHpAmount(float amount)
    {
        hp_fill.localScale = new Vector3(amount, 1f, 1f);
    }

    void Update()
    {
        if(player == null)
            player = GameManager.GetPlayer();
        SetHpAmount(player.GetHealthPct());

        if (Input.GetButtonDown("Map"))
        {
            map_UI.SetActive(!map_UI.activeSelf);
        }
    }
}
