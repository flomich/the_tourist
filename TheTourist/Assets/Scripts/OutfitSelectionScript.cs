using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OutfitSelectionScript : MonoBehaviour
{
    //the total number of outfits
    private int num_outfits = 7;
    
    public void incHead(int inc)
    {
        int head_outfit = PlayerPrefs.GetInt("HeadOutfit", 0);
        PlayerPrefs.SetInt("HeadOutfit", (head_outfit + inc) % num_outfits);
    }

    public void incBody(int inc)
    {
        int body_outfit = PlayerPrefs.GetInt("BodyOutfit", 0);
        PlayerPrefs.SetInt("BodyOutfit", (body_outfit + inc) % num_outfits);
    }

    public void incLegs(int inc)
    {
        int legt_outfit = PlayerPrefs.GetInt("LegOutfit", 0);
        PlayerPrefs.SetInt("LegOutfit", (legt_outfit + inc) % num_outfits);
    }
}
