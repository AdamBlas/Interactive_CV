using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] float jumpForce;
    [SerializeField] Boundary grounder;

    new Rigidbody2D rigidbody;



    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounder.isTouching)
        {
            rigidbody.AddForce(new Vector2(0, jumpForce));
        }
    }
}
