using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PuntigamerScript : MonoBehaviour
{

    public float max_impact = 7.0f;

    public GameObject puntigamer_sprite;

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

                // spwan puntigamer icon
                Vector3 position = collison.gameObject.transform.position + new Vector3(0.0f, 2.7f, 0.0f);
                GameObject icon = Instantiate(puntigamer_sprite, position, Quaternion.identity);
                icon.transform.SetParent(collison.gameObject.transform);

                ParticleEffectsScript.Instance.speedBoostEffect(position, collison.gameObject, 1.0f);

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
