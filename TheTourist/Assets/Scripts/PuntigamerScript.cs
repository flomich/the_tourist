using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PuntigamerScript : MonoBehaviour
{

    public float max_impact = 7.0f;

    void OnCollisionEnter2D(Collision2D collison)
    {
        movePlayer move_script = collison.otherCollider.gameObject.GetComponent<movePlayer>();
        if(collison.gameObject.tag.Contains("Player"))
        {
            InventoryScript inventory = collison.gameObject.GetComponent<InventoryScript>();
            if(inventory != null)
            {
                SoundEffectScript.Instance.playUpgradeSound(gameObject.transform.position);
                inventory.addPuntigamerCount(1);
                Destroy(gameObject);

            }
        }
        else
        {
            if(collison.relativeVelocity.magnitude > max_impact)
            {
                SoundEffectScript.Instance.playBreakingGlass(gameObject.transform.position);
                Collider2D collider = gameObject.GetComponent<Collider2D>();
                Vector3 offset = new Vector3(0.0f, collider.bounds.max.y, 0.0f);
                ParticleEffectsScript.Instance.beerExplosion(transform.position + offset);
                Destroy(gameObject);
            }
        }
    }
}
