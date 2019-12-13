using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public SelfiePose selfie;
    public ScreenShot screenShot;

    public GameObject flashCanvas;

    public TextMeshPro dateField;
    public TextMeshPro levelNameField;
    public TextMeshPro levelDurationField;
    public TextMeshPro tagsField;

    public int tagsPerPolaroid = 5;
    private string[] allTags = {
        "Graz", "2019", "instagraz",
        "leiwand", "gdd", "toursim",
        "Austria", "Styria", "bestcity",
        "polaroid", "socialmedia"
    };

    List<string> GetRandomTags(int count) {
        List<string> all = new List<string>(allTags);
        if (count > allTags.Length)
        {
            return all;
        }
        List<string> result = new List<string>();
        for (int i = 0; i < count; i++) {
            int selected = (int)UnityEngine.Random.Range(0.0f, all.Count);
            result.Add(all[selected]);
            all.RemoveAt(selected);
        }
        return result;
    }

    string GetRandomTagsText() {
        var tags = GetRandomTags(tagsPerPolaroid);
        var tagsString = "";
        bool first = true;
        tags.ForEach(tag => {
            tagsString += (first ? "#": ", #") + tag;
            first = false;
        });
        return tagsString;
    }

    // Start is called before the first frame update
    void Start()
    {
        flashCanvas.SetActive(true);
        string levelName = "Schloßberg - Uhrturm";
        DateTime time = DateTime.UtcNow.Date;
        dateField.text = time.ToShortDateString();

        levelNameField.text = levelName;
        selfie.GeneratePose();
        tagsField.text = GetRandomTagsText();
        Debug.Log(tagsField.text);

        string levelNameForPath = levelName.Replace(' ', '_');
        screenShot.TakeScreenShot(levelNameForPath, path => {
            GalleryEntry entry = new GalleryEntry(levelName, path);
            GalleryManager.Instance.SaveEntry(entry);
        } );
    }
}
