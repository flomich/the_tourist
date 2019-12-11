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
        outfit = GameObject.Find("GameController").GetComponent<gameController>().outfit;
    }

    // Update is called once per frame
    void Update()
    {
        head.GetComponent<SpriteResolver>().SetCategoryAndLabel("head",      outfit[0].ToString());
        body.GetComponent<SpriteResolver>().SetCategoryAndLabel("body",      outfit[1].ToString());
        armL.GetComponent<SpriteResolver>().SetCategoryAndLabel("arm_left",  outfit[1].ToString());
        armR.GetComponent<SpriteResolver>().SetCategoryAndLabel("arm_right", outfit[1].ToString());
        legL.GetComponent<SpriteResolver>().SetCategoryAndLabel("leg_left",  outfit[2].ToString());
        legR.GetComponent<SpriteResolver>().SetCategoryAndLabel("leg_right", outfit[2].ToString());
    }

}
