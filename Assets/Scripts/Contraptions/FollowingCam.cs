using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCam : MonoBehaviour
{
    public Transform target;
    public Vector2 rotRange;
    public float speed;
    public float range;

    LineRenderer scanner;
    Transform camSprite;
    float step = 0;
    bool showingBox = false;

    void Start()
    {
        camSprite = transform.Find("Cam-Body");
        scanner = GetComponent<LineRenderer>();
        scanner.SetPosition(0, camSprite.position);
    }

    void Update()
    {
        Vector3 dirTotarget = target.position - camSprite.position;
        Quaternion targetRotation = Quaternion.identity;
        bool idle = true;

        if (dirTotarget.magnitude < range)
        {
            float angle = Vector3.SignedAngle(dirTotarget, Vector3.up, Vector3.back) + 180;
            
            if (angle > rotRange.x && angle < rotRange.y)
            {
                idle = false;
                targetRotation = Quaternion.LookRotation(Vector3.forward, Vector3.Cross(dirTotarget, Vector3.forward));

                if (step < 1)
                    step += speed * Time.deltaTime;

                scanner.SetPosition(1, target.position);

                if (!showingBox)
                {
                    DialogueBox.ClearTextQueue();
                    DialogueBox.SetText("SCANNER:\nScanning" + DialogueBox.GetPause(5) + "." + DialogueBox.GetPause(5) + "." + DialogueBox.GetPause(5) + ".");
                    DialogueBox.SetText("SCANNER:\nLanguages found.\nPolish:  Native\nEnglish: C1");
                    DialogueBox.ShowBox();
                    showingBox = true;
                }

            }
        }

        if (idle)
        {
            targetRotation = Quaternion.Euler(0, 0, (rotRange.y - rotRange.x) / 2);
            if (step > 0)
                step -= speed * Time.deltaTime;

            scanner.SetPosition(1, camSprite.position);

            if (showingBox)
            {
                DialogueBox.HideBox();
                showingBox = false;
            }
        }

        camSprite.rotation = Quaternion.Lerp(camSprite.rotation, targetRotation, step);
    }


    IEnumerator ShowScan()
    {
        yield return null;
    }

    IEnumerator HideScan()
    {
        yield return null;
    }
}

