﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GrabScript : MonoBehaviour
{
    //the animator of the player
    public Animator boneAnimator;
	
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
		boneAnimator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(grab_state)
        {
            boneAnimator.SetInteger("GrabState", 1);
            if (!grabbing)
            {
                findObjectToGrab();
            }
            
            if (grabbed_object == null) return;


            Vector3 forward_vector = getForwardVectorFromScale();

            // get intersection distance with collider
            float distance_to_other = Mathf.Infinity;
            Vector3 closest_point = transform.position;
            Collider2D collider = grabbed_object.GetComponent<Collider2D>();

            if (collider != null && !collider.isTrigger)
            {
                closest_point = collider.bounds.ClosestPoint(gameObject.transform.position);
                distance_to_other = Mathf.Max(Vector3.Distance(closest_point,
                                    gameObject.transform.position), 0.0f);

            }

            if (distance_to_other > grab_range * 2.0f)
            {
				boneAnimator.SetInteger("GrabState", 0);
                grabbing = false;
                grabbed_object = null;
                SoundEffectScript.Instance.playReleaseSound(transform.position);

            }
        }
        else
        {
            if(grabbing)
            {
                grabbed_object = null;
				boneAnimator.SetInteger("GrabState", 0);
                grabbing = false;
                SoundEffectScript.Instance.playReleaseSound(transform.position);
            }
            else
            {
                boneAnimator.SetInteger("GrabState", 0);
                grabbed_object = null;
                grabbing = false;
            }
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
        float distance_to_other = Mathf.Infinity;

        foreach (GameObject o in objects_in_range)
        {
            //Debug.Log(o.name + " in Box");
            if (!o.activeSelf) continue;
            PlatformAnimator animator = o.GetComponent<PlatformAnimator>();
            if (animator != null) continue;


            Vector3 forward_vector = getForwardVectorFromScale();

            //grab only objects in front of player
            Vector3 vec_to_other = (o.transform.position - gameObject.transform.position);
            distance_to_other = getDistanceToOther(o);

            if (Vector3.Dot(vec_to_other.normalized, forward_vector) < 0.7f || 
                distance_to_other > grab_range)
            {
                // other object is not in front of player
                continue;
            }

            // grab the closest object in front of player
            if (distance_to_other < min_distance)
            {
                min_distance = distance_to_other;
                grabbed_object = o;
                grabbing = true;
                SoundEffectScript.Instance.playGrabSound(transform.position);
            }
        }
    }

    public float getDistanceToOther(GameObject o)
    {
        // get intersection distance with collider
        float distance_to_other = Mathf.Infinity;
        Vector3 closest_point = transform.position;
        Collider2D[] colliders = o.GetComponentsInChildren<Collider2D>();

        foreach (Collider2D collider in colliders)
        {
            if (collider.isTrigger) continue;

            closest_point = collider.bounds.ClosestPoint(gameObject.transform.position);
            distance_to_other = Mathf.Max(Vector3.Distance(closest_point,
                                gameObject.transform.position), 0.0f);
        }

        return distance_to_other;
    }

    public void AddObjectInRange(GameObject o)
    {
        if(!objects_in_range.Contains(o))
        {
            objects_in_range.Add(o);
        }
        
    }

    public void removeObjectInRange(GameObject o)
    {
        if (objects_in_range.Contains(o))
        {
            if (o.Equals(grabbed_object))
            {
                grabbed_object = null;
                grabbing = false;
                SoundEffectScript.Instance.playReleaseSound(transform.position);
            }
            objects_in_range.Remove(o);
        }
    }

    public void setGrabState(bool state)
    {
        grab_state = state;
    }

    // if true, o holds a reference to the grabbed object
    public GameObject getGrabbedObject()
    {
        return grabbed_object;
    }
}
