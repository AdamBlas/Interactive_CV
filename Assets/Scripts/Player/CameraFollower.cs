using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] Transform player;

    new Transform camera;

    void Start()
    {
        camera = Camera.main.transform;
    }

    void Update()
    {
        camera.position = player.position + offset;
    }
}
