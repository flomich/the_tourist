using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayBoxScript : MonoBehaviour
{
    public string abc;

    private int counter;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        counter++;
        //Debug.Log("enter" + abc + " " + counter);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        counter--;
        //Debug.Log("leave" + abc + " " + counter);
    }

    public bool isBlocked()
    {
        return counter != 0;
    }
}
