using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class EnemySetSpritesScript : MonoBehaviour
{
    public GameObject head;
    public GameObject body;
    public GameObject armL;
    public GameObject armR;
    public GameObject legL;
    public GameObject legR;

    public string head_outfit = "0";
    public string body_outfit = "0";
    public string leg_outfit = "0";

    void Start()
    {
        head.GetComponent<SpriteResolver>().SetCategoryAndLabel("head", head_outfit);
        body.GetComponent<SpriteResolver>().SetCategoryAndLabel("body", body_outfit);
        armL.GetComponent<SpriteResolver>().SetCategoryAndLabel("arm_left", body_outfit);
        armR.GetComponent<SpriteResolver>().SetCategoryAndLabel("arm_right", body_outfit);
        legL.GetComponent<SpriteResolver>().SetCategoryAndLabel("leg_left", leg_outfit);
        legR.GetComponent<SpriteResolver>().SetCategoryAndLabel("leg_right", leg_outfit);
    }
}
