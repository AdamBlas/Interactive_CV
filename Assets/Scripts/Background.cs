using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Transform sky;
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        Vector3 skyPosition = sky.position;

        skyPosition.x = player.position.x;
        skyPosition.y = player.position.y;

        sky.position = skyPosition;
    }
}
