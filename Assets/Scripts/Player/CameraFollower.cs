using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] Vector2 maxOffset;
    [SerializeField] float cameraSpeedHorizontal;
    [SerializeField] float cameraSpeedVertical;
    [SerializeField] float horizontalIncrementator;
    [SerializeField] float horizontalDecrementator;
    [SerializeField] float verticalIncrementator;
    [SerializeField] float verticalDecrementator;

    float horizontalOffset = 0.5f;
    float verticalOffset = 0.5f;

    [SerializeField] PlayerMovement player;
    Transform playerTransform;
    Rigidbody2D playerRigidbody;

    Vector2 prevOffset;
    new Transform camera;



    void Start()
    {
        playerTransform = player.transform;
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        camera = Camera.main.transform;
    }

    void Update()
    {
        Vector2 velocity = playerRigidbody.velocity;

        ApplyOffset(ref horizontalOffset, velocity.x, horizontalIncrementator, horizontalDecrementator, cameraSpeedHorizontal);
        ApplyOffset(ref verticalOffset, velocity.y, verticalIncrementator, verticalDecrementator, cameraSpeedVertical);

        float xOffset = Mathf.Lerp(-maxOffset.x, maxOffset.x, LerpVal(horizontalOffset));
        float yOffset = Mathf.Lerp(-maxOffset.y, maxOffset.y, LerpVal(verticalOffset));

        camera.position = new Vector3(playerTransform.position.x + xOffset, playerTransform.position.y + yOffset, camera.position.z);
    }

    float LerpVal(float x)
    {
        return x;

        if (x <= 0.5f)
        {
            return 0.5f - Mathf.Sqrt(0.25f - (x * x));
        }
        else
        {
            return 0.5f + Mathf.Sqrt(0.25f - Mathf.Pow(x - 1, 2));
        }
    }

    void ApplyOffset(ref float offset, float velocity, float incrementator, float decrementator, float cameraSpeed)
    {
        if (velocity == 0)
        {
            if (offset < 0.5f)
            {
                offset += decrementator * Time.deltaTime * cameraSpeed;
                if (offset > 0.5f)
                {
                    offset = 0.5f;
                }
            }
            else if (offset > 0.5f)
            {
                offset -= decrementator * Time.deltaTime * cameraSpeed;
                if (offset < 0.5f)
                {
                    offset = 0.5f;
                }
            }
        }
        else if (velocity > 0)
        {
            if (offset < 1)
            {
                offset += incrementator * Time.deltaTime * cameraSpeed;
            }
        }
        else
        {
            if (offset > 0)
            {
                offset -= incrementator * Time.deltaTime * cameraSpeed;
            }
        }
    }
}
