using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class setSprites : MonoBehaviour
{
    public Transform bone1;
    public Transform bone2;
    public Transform bone3;
    public Transform bone4;
    public Transform bone5;
    public Transform bone6;
    public Transform bone7;
    public Transform bone8;
    public Transform bone9;
    public Transform bone10;
    public Transform bone11;
    public GameObject rLeg;
    public GameObject rLegSprite1;
    public GameObject rLegSprite2;

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
            rLeg.GetComponent<SpriteRenderer>().sprite = rLegSprite1.GetComponent<SpriteRenderer>().sprite;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {

            rLeg.GetComponent<SpriteRenderer>().sprite = rLegSprite2.GetComponent<SpriteRenderer>().sprite;
        }
    }

    void SetBones(GameObject obj)
    {
    }
}
