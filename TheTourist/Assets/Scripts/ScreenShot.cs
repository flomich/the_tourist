﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    private bool captureScreenShot = false;
    public string directoryName = "/screenshots";

    public Vector2Int polaroidSize = new Vector2Int(469, 684);

    private Camera cam;
    private bool wasEnabled;
    private string screenShotName;

    public delegate void ScreenShotTaken(string path);

    private ScreenShotTaken callback = null;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    public void TakeScreenShot(string name, ScreenShotTaken callback) {
        if (captureScreenShot) {
            // Already flagged to screenshot this renderpass
            return;
        }
        wasEnabled = cam.enabled;
        cam.enabled = true;

        float width = polaroidSize.x;
        float height = polaroidSize.y;

        cam.targetTexture = RenderTexture.GetTemporary((int)width, (int)height, 16);
        captureScreenShot = true;
        cam.aspect = width / height;
        this.callback = callback;
        this.screenShotName = name;
    }

    private void OnPostRender()
    {
        if (captureScreenShot) {
            captureScreenShot = false;
            RenderTexture texture = cam.targetTexture;

            Texture2D result = new Texture2D((int)texture.width, (int)texture.height, TextureFormat.ARGB32, false);
            result.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);

            byte[] imageData = result.EncodeToPNG();
            SaveImage(imageData, screenShotName);

            RenderTexture.ReleaseTemporary(texture);
            cam.targetTexture = null;
            cam.enabled = wasEnabled;
        }
    }

    private void SaveImage(byte[] data, string levelName) {
        string directoryPath = Application.persistentDataPath + directoryName;
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        string imagePath = directoryPath + "/" + levelName + ".png";
        File.WriteAllBytes(imagePath, data);
        Debug.Log("Saved image under: " + imagePath);

        callback?.Invoke(imagePath);
    }


}
