using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBlinker : MonoBehaviour
{
    [SerializeField] float waveRange;
    [SerializeField] float waveSpeed;
    [SerializeField] float blinkSpeed;

    Transform arrowPos;
    SpriteRenderer blinkingArrow;
   

    void Start()
    {
        arrowPos = transform.GetChild(0);
        blinkingArrow = arrowPos.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        arrowPos.localPosition = new Vector3(0, Mathf.Sin(Time.time * waveSpeed) * waveRange, 0);
        blinkingArrow.color = new Color(0, 1, 1, 0.5f + Mathf.Sin(Time.time * blinkSpeed) * 0.5f);
    }
}
