using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FrankfurterScript : MonoBehaviour
{

    public float max_impact = 6.0f;

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
                Destroy(gameObject);

            }
        }
        else
        {
            if (collison.relativeVelocity.magnitude > max_impact)
            {
                // TODO add suitable sound
                //SoundEffectScript.Instance.playBreakingGlass(gameObject.transform.position);
                Destroy(gameObject);
                Collider2D collider = gameObject.GetComponent<Collider2D>();
                Vector3 offset = new Vector3(0.0f, collider.bounds.max.y, 0.0f);
                // TODO replace this with serious particle effect
                ParticleEffectsScript.Instance.BeerExplosion(transform.position + offset);
            }
        }
    }
}
