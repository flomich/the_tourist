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
        {"Oper", new LevelData("Oper", "Images/opernhaus")},
        {"Stadtpark", new LevelData("Stadpark","Images/stadtpark")},
        {"Schlossberg", new LevelData("Schlossberg","Images/uhrturm")},
        {"Kunsthaus", new LevelData("Kunsthaus","Images/kunsthaus")},
        {"Stadthalle", new LevelData("Stadthalle","Images/Stadthalle")},
        {"Zentralfriedhof", new LevelData("Zentralfriedhof","Images/Zentralfriedhof")},

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