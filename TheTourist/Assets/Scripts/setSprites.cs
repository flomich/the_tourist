using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class setSprites : MonoBehaviour
{
    public GameObject lLeg;

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
        }
        if (Input.GetKeyDown(KeyCode.J))
        {

            lLeg.GetComponent<SpriteResolver>().SetCategoryAndLabel("LeftLeg", "Purple");
        }
    }

    void SetBones(GameObject obj)
    {
    }
}
