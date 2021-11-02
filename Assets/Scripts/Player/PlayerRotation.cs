using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] float rotationResetDelay;
    [SerializeField] float resetSpeed;

    Transform sprite;
    new Rigidbody2D rigidbody;
    float circumference;

    Coroutine resetCoroutine;

    void Start()
    {
        sprite = transform.GetChild(0);
        rigidbody = GetComponent<Rigidbody2D>();
        circumference = 2 * Mathf.PI * GetComponent<CircleCollider2D>().radius;
    }

    void Update()
    {
        float velocity = rigidbody.velocity.x;

        if (velocity != 0)
        {
            sprite.Rotate(new Vector3(0, 0, 1), -360 * (velocity / circumference) * Time.deltaTime);
            if (resetCoroutine != null)
            {
                StopCoroutine(resetCoroutine);
                resetCoroutine = null;
            }
        }
        else
        {
            if (resetCoroutine == null)
            {
                resetCoroutine = StartCoroutine(ResetRotation());
            }
        }
    }

    IEnumerator ResetRotation()
    {
        yield return new WaitForSeconds(rotationResetDelay);

        int i = 0;
        while (true)
        {
            sprite.localRotation = Quaternion.Euler(0, 0, Mathf.LerpAngle(sprite.localRotation.eulerAngles.z, 0, resetSpeed * i * Time.deltaTime));
            i++;
            yield return null;

            if (sprite.localRotation.eulerAngles.z < 0.05f)
            {
                sprite.localRotation = Quaternion.identity;
                break;
            }
        }
    }
}
