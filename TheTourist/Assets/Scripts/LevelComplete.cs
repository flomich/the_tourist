using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public SelfiePose selfie;
    public ScreenShot screenShot;

    public TextMeshPro dateField;
    public TextMeshPro levelNameField;
    public TextMeshPro levelDurationField;

    // Start is called before the first frame update
    void Start()
    {
        string levelName = "Schloßberg - Uhrturm";
        DateTime time = DateTime.UtcNow.Date;
        dateField.text = time.ToShortDateString();

        levelNameField.text = levelName;
        selfie.GeneratePose();

        string levelNameForPath = levelName.Replace(' ', '_');
        screenShot.TakeScreenShot(levelNameForPath, path => {
            GalleryEntry entry = new GalleryEntry(levelName, path);
            GalleryManager.Instance.SaveEntry(entry);
        } );
    }
}
