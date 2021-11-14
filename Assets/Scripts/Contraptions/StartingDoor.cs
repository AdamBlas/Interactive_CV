using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingDoor : MonoBehaviour
{
    [SerializeField] SpriteRenderer playerRenderer;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerJump playerJump;
    [SerializeField] Animator doorAnimator;
    [SerializeField] Animator playerAnimator;

    void Start()
    {
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        playerRenderer.enabled = false;
        playerMovement.enabled = false;
        playerJump.enabled = false;

        yield return new WaitForSeconds(2f);

        doorAnimator.SetTrigger("Open");

        yield return new WaitForSeconds(0.5f);

        playerRenderer.enabled = true;
        playerAnimator.SetTrigger("Appear");

        yield return new WaitForSeconds(0.8f);

        doorAnimator.SetTrigger("Close");

        yield return new WaitForSeconds(0.2f);

        playerMovement.enabled = true;
        playerJump.enabled = true;
    }

}
