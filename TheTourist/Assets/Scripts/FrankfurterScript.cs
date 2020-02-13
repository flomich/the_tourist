using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FrankfurterScript : MonoBehaviour
{

    public float max_impact = 6.0f;
    public GameObject frankfurter_sprite;

    void OnCollisionEnter2D(Collision2D collison)
    {
        movePlayer move_script = collison.otherCollider.gameObject.GetComponent<movePlayer>();
        if (collison.gameObject.tag.Contains("Player"))
        {
            InventoryScript inventory = collison.gameObject.GetComponent<InventoryScript>();
            if (inventory != null)
            {
                SoundEffectScript.Instance.playUpgradeSound(gameObject.transform.position);
                inventory.addFrankfurterCount(1);

                // spwan frankfurter icon
                Vector3 position = collison.gameObject.transform.position + new Vector3(0.0f, 2.7f, 0.0f);
                GameObject icon = Instantiate(frankfurter_sprite, position, Quaternion.identity);
                icon.transform.SetParent(collison.gameObject.transform);
                Destroy(gameObject);
            }


        }
        else
        {
            if (collison.relativeVelocity.magnitude > max_impact)
            {
                SoundEffectScript.Instance.playBreakingFrankfurter(gameObject.transform.position);
                Collider2D collider = gameObject.GetComponent<Collider2D>();
                Vector3 offset = new Vector3(0.0f, collider.bounds.max.y, 0.0f);
                ParticleEffectsScript.Instance.frankfurterExplosion(transform.position + offset);
                Destroy(gameObject);
            }
        }
    }
}
