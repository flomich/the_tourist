using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PuntigamerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 
    void OnCollisionEnter2D(Collision2D collison)
    {
        movePlayer move_script = collison.otherCollider.gameObject.GetComponent<movePlayer>();
        if(collison.gameObject.tag.Contains("Player"))
        {
            InventoryScript inventory = collison.gameObject.GetComponent<InventoryScript>();
            if(inventory != null)
            {
                inventory.addPuntigamerCount(1);
                Destroy(gameObject);

            }
        }
        else
        {
            if(collison.relativeVelocity.magnitude > 5.0f)
            {
                SoundEffectScript.Instance.playBreakingGlass(gameObject.transform.position);
                Destroy(gameObject);
                Collider2D collider = gameObject.GetComponent<Collider2D>();
                Vector3 offset = new Vector3(0.0f, collider.bounds.max.y, 0.0f);
                ParticleEffectsScript.Instance.BeerExplosion(transform.position + offset);
            }
        }
    }
}
