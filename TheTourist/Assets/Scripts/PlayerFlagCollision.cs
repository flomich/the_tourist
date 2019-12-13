using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlagCollision : MonoBehaviour
{
    public SceneLoaderScript scene_loader;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish") {
            Debug.Log("Collision with FinalFlag!");
            scene_loader.loadScene("LevelComplete");
        }
    }
}
