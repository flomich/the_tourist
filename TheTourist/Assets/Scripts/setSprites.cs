using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class setSprites : MonoBehaviour
{
    public GameObject lLeg;
    public GameObject rLeg;
    public GameObject lArm;
    public GameObject rArm;

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
            lLeg.GetComponent<SpriteResolver>().SetCategoryAndLabel("LeftLeg", "Cop");
            rLeg.GetComponent<SpriteResolver>().SetCategoryAndLabel("RightLeg", "Cop");
            lArm.GetComponent<SpriteResolver>().SetCategoryAndLabel("LeftArm", "Cop");
            rArm.GetComponent<SpriteResolver>().SetCategoryAndLabel("RightArm", "Cop");
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            lLeg.GetComponent<SpriteResolver>().SetCategoryAndLabel("LeftLeg",  "Purple");
            rLeg.GetComponent<SpriteResolver>().SetCategoryAndLabel("RightLeg", "Purple");
            lArm.GetComponent<SpriteResolver>().SetCategoryAndLabel("LeftArm",  "Purple");
            rArm.GetComponent<SpriteResolver>().SetCategoryAndLabel("RightArm", "Purple");
        }
    }

    void SetBones(GameObject obj)
    {
    }
}
