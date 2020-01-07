using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GalleryManager
{
    public string directoryName = "gallery_data";
    public string fileName = "data.json";

    private static GalleryManager instance;

    public static GalleryManager Instance {
        get { return instance ?? (instance = new GalleryManager()); }
    }

    private string directoryPath { get { return Application.persistentDataPath + "/" + directoryName; } }
    private string fullPath { get { return directoryPath + "/" + fileName; }}

    public List<GalleryEntry> LoadEntries() {
        try
        {
            string json = File.ReadAllText(fullPath);
            var ges = JsonUtility.FromJson<GalleryEntriesSerializer>(json); ;
            return new List<GalleryEntry>(ges.entries);
        }
        catch (Exception) {
            return new List<GalleryEntry>();
        }
    }

    public void SaveEntry(GalleryEntry entry) {
        var entries = LoadEntries();
        entries.RemoveAll(item => item.name == entry.name);
        entries.Add(entry);
        OverrideEntriesOnDisk(entries);
    }

    public void RemoveEntry(string name) {
        var entries = LoadEntries();
        entries.RemoveAll(item => item.name == name);
        OverrideEntriesOnDisk(entries);
    }

    private void OverrideEntriesOnDisk(List<GalleryEntry> entries) {
        var ges = new GalleryEntriesSerializer(entries);
        string json = JsonUtility.ToJson(ges, true);
        if (!Directory.Exists(directoryPath)) {
            Directory.CreateDirectory(directoryPath);
        }
        File.WriteAllText(fullPath, json);
    }

    [Serializable]
    private class GalleryEntriesSerializer
    {
        public GalleryEntry[] entries;
        public GalleryEntriesSerializer(List<GalleryEntry> entries) {
            this.entries = entries.ToArray();
        }
    }
}
