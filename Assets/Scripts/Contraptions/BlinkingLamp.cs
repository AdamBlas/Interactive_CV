using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingLamp : MonoBehaviour
{
    [SerializeField] Renderer lightRenderer;
    [SerializeField] float blinkingSpeed;
    [SerializeField][Range(0f, 1f)] float threshold;

    void Update()
    {
        lightRenderer.enabled = Mathf.PerlinNoise(Time.time * blinkingSpeed, 0) <= threshold;
    }
}
