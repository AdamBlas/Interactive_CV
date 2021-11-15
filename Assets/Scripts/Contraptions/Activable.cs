using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activable: MonoBehaviour
{
    [HideInInspector] public Animator animator;

    public void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    public virtual void OnActivate()
    {
        animator?.SetTrigger("Activate");
    }
    public virtual void OnDeactivate()
    {
        animator?.SetTrigger("Deactivate");
    }
}
