using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Activating
{
    [SerializeField] Sprite offSprite;
    [SerializeField] Sprite onSprite;
    [SerializeField] Activable[] activables;
    [SerializeField] float uiOpenCloseSpeed;
    [SerializeField] bool singleUse;
    bool used = false;

    float startUiWidth = 0.1f;

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

        ui.localScale = new Vector3(startUiWidth, 0);
        switchSprite.sprite = offSprite;
    }

    public override void Activate()
    {
        switchSprite.sprite = onSprite;
        
        foreach (Activable a in activables)
            a?.OnActivate();
    }

    public override void Deactivate()
    {
        switchSprite.sprite = offSprite;

        foreach (Activable a in activables)
            a?.OnDeactivate();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (used && singleUse)
            return;

        if (collision != null && !collision.CompareTag("Player"))
            return;

        playerInside = true;

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
        if (collision != null && !collision.CompareTag("Player"))
            return;

        playerInside = false;

        if (uiOpenCoroutine != null)
        {
            StopCoroutine(uiOpenCoroutine);
            uiOpenCoroutine = null;
        }

        if (uiCloseCoroutine == null)
        {
            uiCloseCoroutine = StartCoroutine(UiClose());
        }
    }

    public void Update()
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

                used = true;
                if (singleUse)
                    OnTriggerExit2D(null);
            }
        }
    }

    IEnumerator UiOpen()
    {
        while (ui.localScale.y < 1)
        {
            ui.localScale += new Vector3(0, uiOpenCloseSpeed * Time.deltaTime);
            yield return null;
        }
        ui.localScale = new Vector3(ui.localScale.x, 1);

        while (ui.localScale.x < 1)
        {
            ui.localScale += new Vector3(uiOpenCloseSpeed * Time.deltaTime, 0);
            yield return null;
        }
        ui.localScale = Vector3.one;
    }

    IEnumerator UiClose()
    {
        while (ui.localScale.x > startUiWidth)
        {
            ui.localScale -= new Vector3(uiOpenCloseSpeed * Time.deltaTime, 0);
            yield return null;
        }
        ui.localScale = new Vector3(startUiWidth, 1);

        while (ui.localScale.y > 0)
        {
            ui.localScale -= new Vector3(0, uiOpenCloseSpeed * Time.deltaTime);
            yield return null;
        }
        ui.localScale = new Vector3(startUiWidth, 0);
    }
}
