using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GalleryView : MonoBehaviour
{
    public GameObject leftPage;
    public GameObject rightPage;

    private int polaroidsPerPage = 9;

    void Start()
    {

        SetProperCellSize(leftPage);
        SetProperCellSize(rightPage);

        var entries = GalleryManager.Instance.LoadEntries();

        for(int i = 0; i < entries.Count; i++) {
            var entry = entries[i];
            if(i < polaroidsPerPage) {
                SetPolaroid(entry, leftPage, i);
            }
            else {
                SetPolaroid(entry, rightPage, i - polaroidsPerPage);
            }
        }
    }

    private void SetProperCellSize(GameObject page){
        float fullWidth = page.GetComponent<RectTransform>().rect.width;
        float targetWidth = (fullWidth / 3) - page.GetComponent<GridLayoutGroup>().spacing.x;
        float targetHeight = targetWidth * 1.45f;
        Vector2 targetCellSize = new Vector2(targetWidth, targetHeight);
        page.GetComponent<GridLayoutGroup>().cellSize = targetCellSize;
    }

    private void SetPolaroid(GalleryEntry entry, GameObject page, int index) {
        var go = page.transform.GetChild(index);
        var image = go.GetComponent<Image>();
        image.sprite = CreateSprite(entry.path);
        image.color = new Color(1, 1, 1, 1);
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
