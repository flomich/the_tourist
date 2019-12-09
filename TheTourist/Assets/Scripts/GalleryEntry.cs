using System;

[Serializable]
public struct GalleryEntry
{
    public string name;
    public string path;

    public GalleryEntry(string name, string path) {
        this.name = name;
        this.path = path;
    }
}
