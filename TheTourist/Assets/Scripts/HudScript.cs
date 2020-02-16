using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudScript : MonoBehaviour
{
    public GameObject hud_text_gameobject = null;
    public GameObject player_gamer_object = null;
    public GameObject vignette_game_object = null;

    private HealthScript health_script;
    private InventoryScript inventory_script;
    private Image vignette_image;

    // Start is called before the first frame update
    void Start()
    {
        health_script = player_gamer_object.GetComponent<HealthScript>();
        inventory_script = player_gamer_object.GetComponent<InventoryScript>();
        vignette_image = vignette_game_object.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Text hud_text = hud_text_gameobject.GetComponent<Text>();

        string text = health_script.getHealth().ToString();
        text += "%\nx " + inventory_script.getDoenerCount().ToString();
        text += "\nx " + inventory_script.getPuntigamerCount().ToString();
        text += "\nx " + inventory_script.getFrankfurterCount().ToString();

        hud_text.text = text;


        float blood = health_script.getHealth() / 100.0f;
        blood = Mathf.Exp(-10.0f * blood);
        blood *= Mathf.Sin(Time.time * 3.0f) * 0.5f + 0.5f;

        vignette_image.color = new Color(blood, blood, blood, 1.0f);
    }
}
