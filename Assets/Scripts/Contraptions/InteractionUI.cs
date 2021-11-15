using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUI : Activating
{
    [SerializeField] float uiOpenCloseSpeed;

    float startUiWidth = 0.1f;
    Transform ui;
    public bool playerInside { get; private set; } = false;
    Coroutine uiOpenCoroutine;
    Coroutine uiCloseCoroutine;
    public Activable activable;
    bool active = false;


    void Start()
    {
        ui = transform.GetChild(0);
        ui.localScale = new Vector3(startUiWidth, 0);
    }

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            if (active)
            {
                active = false;
                Deactivate();
            }
            else
            {
                active = true;
                Activate();
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
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

    IEnumerator UiOpen()
    {
        while (ui.localScale.y < 1)
        {
            ui.localScale += new Vector3(0, uiOpenCloseSpeed * Time.deltaTime);
            yield return null;
        }
        ui.localScale = new Vector3(startUiWidth, 1);

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

    public void Disable()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public override void Activate()
    {
        activable.OnActivate();
    }

    public override void Deactivate()
    {
        activable.OnDeactivate();
    }
}
