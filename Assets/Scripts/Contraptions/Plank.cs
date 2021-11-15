using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plank : Activable
{
    Vector2 posDiff;
    Vector2 startPosition;
    [SerializeField] Vector2 targetPosition;
    [SerializeField] float speed;
    [SerializeField] Vector2 startOffset;
    [SerializeField] Vector2 endOffset;
    [SerializeField] Vector2 startSize;
    [SerializeField] Vector2 endSize;
    Vector2 offsetDiff;
    Vector2 sizeDiff;

    float dist;
    Coroutine extractCoroutine;
    Coroutine retractCoroutine;
    new BoxCollider2D collider;

    private new void Start()
    {
        collider = GetComponentInParent<BoxCollider2D>();

        if (collider != null)
        {
            collider.offset = startOffset;
            collider.size = startSize;
        }

        startPosition = transform.localPosition;
        posDiff = targetPosition - startPosition;

        dist = posDiff.magnitude;

        offsetDiff = endOffset - startOffset;
        sizeDiff = endSize - startSize;
    }

    public override void OnActivate()
    {
        if (retractCoroutine != null)
        {
            StopCoroutine(retractCoroutine);
            retractCoroutine = null;
        }

        extractCoroutine = StartCoroutine(Extract());
    }

    public override void OnDeactivate()
    {
        if (extractCoroutine != null)
        {
            StopCoroutine(extractCoroutine);
            extractCoroutine = null;
        }

        retractCoroutine = StartCoroutine(Retract());
    }

    IEnumerator Extract()
    {
        while (Vector2.Distance(startPosition, transform.localPosition) < dist)
        {
            float time = speed * Time.deltaTime;
            transform.localPosition += new Vector3(posDiff.x, posDiff.y) * time;

            if (collider != null)
            {
                collider.offset += offsetDiff * time;
                collider.size += sizeDiff * time;
            }

            yield return null;
        }
        transform.localPosition = targetPosition;

        if (collider != null)
        {
            collider.offset = endOffset;
            collider.size = endSize;
        }
    }

    IEnumerator Retract()
    {
        while (Vector2.Distance(targetPosition, transform.localPosition) < dist)
        {
            float time = speed * Time.deltaTime;
            transform.localPosition -= new Vector3(posDiff.x, posDiff.y) * time;

            if (collider != null)
            {
                collider.offset -= offsetDiff * time;
                collider.size -= sizeDiff * time;
            }

            yield return null;
        }
        transform.localPosition = startPosition;

        if (collider != null)
        {
            collider.offset = startOffset;
            collider.size = startSize;
        }
    }
}
