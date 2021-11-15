using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Activable
{
    [SerializeField] Activable[] activables;
    [SerializeField] bool singleUse;

    InteractionUI ui;

    new void Start()
    {
        base.Start();
        ui = GetComponentInChildren<InteractionUI>();
        ui.activable = this;
    }

    public override void OnActivate()
    {
        base.OnActivate();
        foreach (Activable a in activables)
            a?.OnActivate();

        if (singleUse)
        {
            ui.Disable();
            ui.OnTriggerExit2D(null);
        }
    }

    public override void OnDeactivate()
    {
        base.OnDeactivate();
        foreach (Activable a in activables)
            a?.OnDeactivate();
    }
}
