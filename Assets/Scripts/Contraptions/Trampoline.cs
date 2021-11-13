using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] float strengh;

    Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.CompareTag("Player"))
        {
            Rigidbody2D player = collision.attachedRigidbody;
            player.velocity = new Vector2(player.velocity.x, strengh);
            anim.SetTrigger("Activate");
        }
    }
}
