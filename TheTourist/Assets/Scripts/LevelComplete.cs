using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public SelfiePose selfie;
    public ScreenShot screenShot;
    
    // Start is called before the first frame update
    void Start()
    {
        string levelName = "level01";
        selfie.GeneratePose();
        screenShot.TakeScreenShot(levelName, path => {
            GalleryEntry entry = new GalleryEntry(levelName, path);
            GalleryManager.Instance.SaveEntry(entry);
        } );
    }
}
