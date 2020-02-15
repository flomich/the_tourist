using UnityEngine;
using System.Collections.Generic;

public class LevelDatabase
{

    public class LevelData
    {
        public string name;
        public string polaroidPath;
        public LevelData(string name, string path)
        {
            this.name = name;
            this.polaroidPath = path;
        }
    }

    private static Dictionary<string, LevelData> levels = new Dictionary<string, LevelData>(){
        {"Hauptplatz", new LevelData("Hauptplatz", "Images/hauptplatz")},
        {"Opernhaus", new LevelData("Opernhaus", "Images/opernhaus")},
        {"Stadtpark", new LevelData("Stadpark","Images/stadtpark")},
        {"Schlossberg", new LevelData("Schlossberg","Images/uhrturm")},
        // TODO: Kunsthaus polaroid image not implemented yet
        {"Kunsthaus", new LevelData("Kunsthaus","Images/uhrturm")},

    };

    public static LevelData GetLevelData(string sceneName)
    {
        if (!levels.ContainsKey(sceneName))
        {
            Debug.Log("No level data found for scene: " + sceneName);
            return null;
        }
        return levels[sceneName];
    }
}