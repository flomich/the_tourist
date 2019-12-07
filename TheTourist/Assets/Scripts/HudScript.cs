using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudScript : MonoBehaviour
{
    public GameObject hud_text_gameobject;
    public GameObject player_gamer_object;

    private HealthScript health_script;
    private InventoryScript inventory_script;

    // Start is called before the first frame update
    void Start()
    {
        health_script = player_gamer_object.GetComponent<HealthScript>();
        inventory_script = player_gamer_object.GetComponent<InventoryScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Text hud_text = hud_text_gameobject.GetComponent<Text>();

        string text = "Health: " + health_script.getHealth().ToString();
        text += "\nDoener: " + inventory_script.getDoenerCount().ToString();
        text += "\nPuntigamer: " + inventory_script.getPuntigamerCount().ToString();
        text += "\nFrankfurter: " + inventory_script.getFrankfurterCount().ToString();

        hud_text.text = text;
    }
}
