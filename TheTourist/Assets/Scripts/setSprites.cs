using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class setSprites : MonoBehaviour
{
    public GameObject head;
    public GameObject body;
    public GameObject armL;
    public GameObject armR;
    public GameObject legL;
    public GameObject legR;

    public int[] outfit;

    // Start is called before the first frame update
    void Start()
    {
        GameObject game_controller_game_object = GameObject.Find("GameController");
        outfit = new int[3];
        bool set_outfit = false;
        if (game_controller_game_object != null)
        {
            gameController game_controller = game_controller_game_object.GetComponent<gameController>();
            if(game_controller != null)
            {
                outfit = game_controller.outfit;
                set_outfit = true;
            }
        }
        if(!set_outfit)
        {
            outfit[0] = 0;
            outfit[1] = 0;
            outfit[2] = 0;
        }
        
        head.GetComponent<SpriteResolver>().SetCategoryAndLabel("head", outfit[0].ToString());
        body.GetComponent<SpriteResolver>().SetCategoryAndLabel("body", outfit[1].ToString());
        armL.GetComponent<SpriteResolver>().SetCategoryAndLabel("arm_left", outfit[1].ToString());
        armR.GetComponent<SpriteResolver>().SetCategoryAndLabel("arm_right", outfit[1].ToString());
        legL.GetComponent<SpriteResolver>().SetCategoryAndLabel("leg_left", outfit[2].ToString());
        legR.GetComponent<SpriteResolver>().SetCategoryAndLabel("leg_right", outfit[2].ToString());
    }

    // Update is called once per frame
    void Update()
    {

    }

}
