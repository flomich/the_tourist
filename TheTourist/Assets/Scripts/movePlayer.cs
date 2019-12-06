using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour
{
    public float mult;
    public Vector2 direction;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        mult = 1.0f;

    }

    // Update is called once per frame
    void Update()
    {
        float speed = 0.5f * mult;

        float moveHorizontal = Input.GetAxis("Horizontal");
        if (moveHorizontal < 0.0)
        {
            transform.localScale = new Vector3(1.0f,1.0f, 1.0f);
        }
        else
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        float moveVertical = Input.GetAxis("Vertical");

        direction = new Vector3(moveHorizontal, moveVertical, 0.0f);

        MovePlayer(direction, speed);

        direction.Normalize();

        if (direction == Vector2.zero)
        {
            animator.SetInteger("WalkState", 0);
        }
        else
        {
            animator.SetInteger("WalkState", (int)direction.x);
        }
    }

    void MovePlayer(Vector2 direction, float speed)
    {
        transform.position += new Vector3(direction.x * speed, direction.y * speed);
    
    }
}
