using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlagCollision : MonoBehaviour
{
    public string level_name = "LevelComplete";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            SceneLoaderScript scene_loader = GetComponent<SceneLoaderScript>();

            scene_loader.loadScene(level_name);
        }

        //if (collision.tag == "Finish") {
        //    Debug.Log("Collision with FinalFlag!");
        //    scene_loader.loadScene("LevelComplete");
        //}
    }
}
