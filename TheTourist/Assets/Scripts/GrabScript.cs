using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GrabScript : MonoBehaviour
{
    private List<GameObject> objects_in_range;
    // grab range in meters
    public float grab_range = 1.5f;

    // grabbed object
    private GameObject grabbed_object = null;
    private bool grab_state;

    // is the player currently grabbing something
    private bool grabbing = false;
 

    // Start is called before the first frame update
    void Start()
    {
        objects_in_range = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(grab_state)
        {
            if (!grabbing)
            {
                findObjectToGrab();
            }
            else
            {
                Vector3 forward_vector = getForwardVectorFromScale();

                // get intersection distance with collider
                float distance_to_other = Mathf.Infinity;
                Vector3 closest_point = transform.position;
                Collider2D collider = grabbed_object.GetComponent<Collider2D>();

                if (collider != null)
                {
                    closest_point = collider.bounds.ClosestPoint(gameObject.transform.position);
                    distance_to_other = Mathf.Max(Vector3.Distance(closest_point,
                                        gameObject.transform.position), 0.0f);

                }

                if (distance_to_other > grab_range)
                {
                    grabbing = false;
                    grabbed_object = null;
                }
                else
                {
                    float rest_length = grab_range * 0.5f;

                    Rigidbody2D rigidbody = grabbed_object.GetComponent<Rigidbody2D>();

                    if (rigidbody != null)
                    {
                        if (distance_to_other <= rest_length)
                        {
                            rigidbody.AddForce(-forward_vector * distance_to_other);
                        }
                        else
                        {
                            rigidbody.AddForce(forward_vector * distance_to_other);
                        }
                    }


                }
            }

        }
        else
        {
            grabbed_object = null;
            grabbing = false;
        }


    }

    private Vector3 getForwardVectorFromScale()
    {
        // get player forward vector from scale
        Vector3 forward_vector;
        if (gameObject.transform.localScale.x < 0.0f)
        {
            forward_vector = new Vector3(1.0f, 0.0f, 0.0f);
        }
        else
        {
            forward_vector = new Vector3(-1.0f, 0.0f, 0.0f);

        }

        return forward_vector;
    }

    private void findObjectToGrab()
    {
        float min_distance = Mathf.Infinity;
        foreach (GameObject o in objects_in_range)
        {
            Vector3 forward_vector = getForwardVectorFromScale();

            //grab only objects in front of player
            Vector3 vec_to_other = (o.transform.position - gameObject.transform.position);

            // get intersection distance with collider
            float distance_to_other = Mathf.Infinity;
            Vector3 closest_point = transform.position;
            Collider2D collider = o.GetComponent<Collider2D>();

            if (collider != null)
            {
                closest_point = collider.bounds.ClosestPoint(gameObject.transform.position);
                distance_to_other = Mathf.Max(Vector3.Distance(closest_point,
                                    gameObject.transform.position), 0.0f);

                if ((Vector3.Dot(vec_to_other.normalized, forward_vector) < 0.0f &&
                    distance_to_other > 0.1) || distance_to_other > grab_range)
                {
                    // other object is not in front of player
                    continue;
                }
            }

            // grab the closest object in front of player
            if (distance_to_other < min_distance)
            {
                min_distance = distance_to_other;
                grabbed_object = o;
                grabbing = true;
            }
        }
    }

    public void AddObjectInRange(GameObject o)
    {
        objects_in_range.Add(o);
    }

    public void removeObjectInRange(GameObject o)
    {
        if (objects_in_range.Contains(o))
        {
            if(o.Equals(grabbed_object))
            {
                grabbed_object = null;
            }
            objects_in_range.Remove(o);
        }
    }

    public void setGrabState(bool state)
    {
        grab_state = state;
    }
}
