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

    void Update()
    {
        //TODO refactor this so that this is not called in each frame for the player game object
        string head_outfit = PlayerPrefs.GetInt("HeadOutfit", 0).ToString();
        string body_outfit = PlayerPrefs.GetInt("BodyOutfit", 0).ToString();
        string leg_outfit = PlayerPrefs.GetInt("LegOutfit", 0).ToString();

        head.GetComponent<SpriteResolver>().SetCategoryAndLabel("head", head_outfit);
        body.GetComponent<SpriteResolver>().SetCategoryAndLabel("body", body_outfit);
        armL.GetComponent<SpriteResolver>().SetCategoryAndLabel("arm_left", body_outfit);
        armR.GetComponent<SpriteResolver>().SetCategoryAndLabel("arm_right", body_outfit);
        legL.GetComponent<SpriteResolver>().SetCategoryAndLabel("leg_left", leg_outfit);
        legR.GetComponent<SpriteResolver>().SetCategoryAndLabel("leg_right", leg_outfit);
    }
}
