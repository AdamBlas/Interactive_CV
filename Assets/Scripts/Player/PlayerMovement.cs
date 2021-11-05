using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    public float minSpeed;
    public float maxSpeed;

    [SerializeField] [Range(0f, 1f)] float slowDownRatio;
    [SerializeField] float stopThreshold;

    [SerializeField] Boundary rightBoundary;
    [SerializeField] Boundary leftBoundary;

    new Rigidbody2D rigidbody;



    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float input = Input.GetAxis("Horizontal");

        if (input != 0)
        {
            rigidbody.AddForce(new Vector2(movementSpeed * Time.deltaTime * input, 0));

            if (input > 0)
            {
                if (!rightBoundary.isTouching)
                {
                    if (rigidbody.velocity.x < minSpeed)
                    {
                        rigidbody.velocity = new Vector2(minSpeed * input, rigidbody.velocity.y);
                    }
                    else if (rigidbody.velocity.x > maxSpeed)
                    {
                        rigidbody.velocity = new Vector2(maxSpeed, rigidbody.velocity.y);
                    }
                }
            }
            else if (input < 0)
            {
                if (!leftBoundary.isTouching)
                {
                    if (rigidbody.velocity.x > -minSpeed)
                    {
                        rigidbody.velocity = new Vector2(minSpeed * input, rigidbody.velocity.y);
                    }
                    else if (rigidbody.velocity.x < -maxSpeed)
                    {
                        rigidbody.velocity = new Vector2(-maxSpeed, rigidbody.velocity.y);
                    }
                }
            }
        }
        else
        {
            if (rigidbody.velocity.x > 0)
            {
                if (rigidbody.velocity.x > stopThreshold)
                {
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x * slowDownRatio, rigidbody.velocity.y);
                }
            }
            else if (rigidbody.velocity.x < 0)
            {
                if (rigidbody.velocity.x < -stopThreshold)
                {
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x * slowDownRatio, rigidbody.velocity.y);
                }
            }

            if (rigidbody.velocity.x > -stopThreshold && rigidbody.velocity.x < stopThreshold)
            {
                rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            }
        }
    }
}
