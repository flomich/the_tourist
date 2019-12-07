using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoenerScript : MonoBehaviour
{

    public float max_impact = 10.0f;
    void OnCollisionEnter2D(Collision2D collison)
    {
        movePlayer move_script = collison.otherCollider.gameObject.GetComponent<movePlayer>();
        if (collison.gameObject.tag.Contains("Player"))
        {
            InventoryScript inventory = collison.gameObject.GetComponent<InventoryScript>();
            if (inventory != null)
            {
                inventory.addDoenerCount(1);
                Destroy(gameObject);

            }
        }
        else
        {
            if (collison.relativeVelocity.magnitude > max_impact)
            {
                SoundEffectScript.Instance.playBreakingGlass(gameObject.transform.position);
                Destroy(gameObject);
                Collider2D collider = gameObject.GetComponent<Collider2D>();
                Vector3 offset = new Vector3(0.0f, collider.bounds.max.y, 0.0f);
                //TODO create effect for despawning doener
            }
        }
    }
}
