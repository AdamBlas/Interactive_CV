using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, IActivating
{
    [SerializeField] Sprite offSprite;
    [SerializeField] Sprite onSprite;
    [SerializeField] IActivable activable;
    [SerializeField] float uiOpenCloseSpeed;

    SpriteRenderer switchSprite;
    Transform ui;
    bool playerInside = false;
    bool isActivated = false;
    Coroutine uiOpenCoroutine;
    Coroutine uiCloseCoroutine;

    void Start()
    {
        switchSprite = GetComponentInChildren<SpriteRenderer>();
        ui = transform.Find("UI Scaler");

        ui.localScale = Vector3.zero;
        switchSprite.sprite = offSprite;
    }

    public void Activate()
    {
        switchSprite.sprite = onSprite;
        activable?.OnActivate();
    }

    public void Deactivate()
    {
        switchSprite.sprite = offSprite;
        activable?.OnDeactivate();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = true;
        }

        if (uiCloseCoroutine != null)
        {
            StopCoroutine(uiCloseCoroutine);
            uiCloseCoroutine = null;
        }
        
        if (uiOpenCoroutine == null)
        {
            uiOpenCoroutine = StartCoroutine(UiOpen());
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = false;
        }

        if (uiOpenCoroutine != null)
        {
            StopCoroutine(uiOpenCoroutine);
            uiOpenCoroutine = null;
        }

        if (uiCloseCoroutine == null)
        {
            uiCloseCoroutine = StartCoroutine(UiOpen());
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            if (isActivated)
            {
                isActivated = false;
                Deactivate();
            }
            else
            {
                isActivated = true;
                Activate();
            }
        }
    }

    IEnumerator UiOpen()
    {
        while (true)
        {
            ui.localScale += new Vector3(0, 0);
            yield return null;
        }
    }

    IEnumerator UiClose()
    {
        yield return null;
    }
}
