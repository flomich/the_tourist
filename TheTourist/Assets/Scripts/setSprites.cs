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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetBones(this.gameObject);

        if (Input.GetKeyDown(KeyCode.H))
        {
            head.GetComponent<SpriteResolver>().SetCategoryAndLabel("head",      "def");
            body.GetComponent<SpriteResolver>().SetCategoryAndLabel("body",      "def");
            armL.GetComponent<SpriteResolver>().SetCategoryAndLabel("arm_left",  "def");
            armR.GetComponent<SpriteResolver>().SetCategoryAndLabel("arm_right", "def");
            legL.GetComponent<SpriteResolver>().SetCategoryAndLabel("leg_left",  "def");
            legR.GetComponent<SpriteResolver>().SetCategoryAndLabel("leg_right", "def");
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            head.GetComponent<SpriteResolver>().SetCategoryAndLabel("head",      "jobs");
            body.GetComponent<SpriteResolver>().SetCategoryAndLabel("body",      "jobs");
            armL.GetComponent<SpriteResolver>().SetCategoryAndLabel("arm_left",  "jobs");
            armR.GetComponent<SpriteResolver>().SetCategoryAndLabel("arm_right", "jobs");
            legL.GetComponent<SpriteResolver>().SetCategoryAndLabel("leg_left",  "jobs");
            legR.GetComponent<SpriteResolver>().SetCategoryAndLabel("leg_right", "jobs");
        }
    }

    void SetBones(GameObject obj)
    {
    }
}
