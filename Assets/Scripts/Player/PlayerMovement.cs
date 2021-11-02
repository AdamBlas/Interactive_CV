using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;

    [SerializeField] [Range(0f, 1f)] float slowDownRatio;
    [SerializeField] float stopThreshold;

    new Rigidbody2D rigidbody;



    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");

        if (input != 0)
        {
            rigidbody.AddForce(new Vector2(movementSpeed * Time.deltaTime * input, 0));

            if (input > 0 && rigidbody.velocity.x < minSpeed)
            {
                rigidbody.velocity = new Vector2(minSpeed, rigidbody.velocity.y);
            }
            else if (input < 0 && rigidbody.velocity.x > -minSpeed)
            {
                rigidbody.velocity = new Vector2(-minSpeed, rigidbody.velocity.y);
            }
            else if (input > 0 && rigidbody.velocity.x > maxSpeed)
            {
                rigidbody.velocity = new Vector2(maxSpeed, rigidbody.velocity.y);
            }
            else if (input < 0 && rigidbody.velocity.x < -maxSpeed)
            {
                rigidbody.velocity = new Vector2(-maxSpeed, rigidbody.velocity.y);
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
