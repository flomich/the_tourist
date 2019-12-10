using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GalleryView : MonoBehaviour
{

    public GameObject prefabGalleryPolaroid;

    float width = 0.0f;
    float height = 0.0f;
    float size = 3.5f;

    int itemsPerRow = 5;
    int objectsCreated = 0;

    public GameObject emptyPlaceHolder;

    void Start()
    {
        var bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        var topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

        width = topRight.x - bottomLeft.x;
        height = topRight.y - bottomLeft.y;

        var entries = GalleryManager.Instance.LoadEntries();
        emptyPlaceHolder.SetActive(entries.Count == 0);
  
        //for (int i = 0; i < 10; i++) //Debug stuff
        entries.ForEach(CreatePolaroidGameObject);
    }

    void CreatePolaroidGameObject(GalleryEntry entry)
    {
        var polaroid = Instantiate(prefabGalleryPolaroid);
        Sprite sprite = CreateSprite(entry.path);
        polaroid.GetComponent<SpriteRenderer>().sprite = sprite;
        polaroid.transform.position = GetVectorPosition(objectsCreated);
        objectsCreated++;
    }

    Vector3 GetVectorPosition(int index) {

        int x = index % itemsPerRow;
        int y = index / itemsPerRow;
        return new Vector3(
            x * size - width / 2.0f + 0.4f,
            y * size - height / 2.0f + 0.4f);
    } 

    public Sprite CreateSprite(string filePath, float ppu = 100.0f)
    {
        Texture2D texture = LoadTexture(filePath);
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0), ppu);
    }

    private Texture2D LoadTexture(string FilePath)
    {
        Texture2D texture = new Texture2D(2, 2);
        if (File.Exists(FilePath))
        {
            byte[] data = File.ReadAllBytes(FilePath);
            if (texture.LoadImage(data))
                return texture;
        }
        return null;
    }
}
