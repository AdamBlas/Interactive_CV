using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactNPC : Activable
{
    [SerializeField] InteractionUI ui;

    public override void OnActivate()
    {
        base.OnActivate();
        ui.Disable();
    }
}
