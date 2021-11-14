using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] Vector2 hidePos;
    [SerializeField] Vector2 showPos;
    [SerializeField] Transform box;
    [SerializeField] Text text;
    [SerializeField] float boxSpeed;
    [SerializeField] float charsPerSec;

    static DialogueBox @this;

    Coroutine showCoroutine;
    Coroutine hideCoroutine;
    float t = 0;

    private void Start()
    {
        @this = this;
    }

    public static void ShowAndHide(float delay)
    {
        @this.StartCoroutine(@this._ShowAndHide(delay));
    }
    IEnumerator _ShowAndHide(float delay)
    {
        ShowBox();
        yield return new WaitForSeconds(delay);
        HideBox();
    }

    public static void ShowBox()
    {
        if (@this.hideCoroutine != null)
        {
            @this.StopCoroutine(@this.hideCoroutine);
            @this.hideCoroutine = null;
        }

        @this.showCoroutine = @this.StartCoroutine(@this._ShowBox());
    }
    IEnumerator _ShowBox()
    {
        while (true)
        {
            if (t > 1)
            {
                t = 1;
                break;
            }

            box.localPosition = Vector2.Lerp(hidePos, showPos, Mathf.Sqrt(t)); ;
            t += boxSpeed * Time.deltaTime;
            yield return null;
        }

        box.localPosition = showPos;
    }

    public static void HideBox()
    {
        if (@this.showCoroutine != null)
        {
            @this.StopCoroutine(@this.showCoroutine);
            @this.showCoroutine = null;
        }

        @this.hideCoroutine = @this.StartCoroutine(@this._HideBox());
    }
    IEnumerator _HideBox()
    {
        while (true)
        {
            if (t < 0)
            {
                t = 0;
                break;
            }

            box.localPosition = Vector2.Lerp(hidePos, showPos, Mathf.Sqrt(t)); ;
            t -= boxSpeed * Time.deltaTime;
            yield return null;
        }

        box.localPosition = hidePos;
    }

    public static void SetText(string text)
    {
        @this.StartCoroutine(@this._SetText(text));
    }
    IEnumerator _SetText(string text)
    {
        this.text.text = string.Empty;
        float delay = 1f / charsPerSec;

        foreach (char c in text)
        {
            if (c != 11)
                this.text.text += c;
            yield return new WaitForSeconds(delay);
        }
    }

    public static string GetPause(int pausesAmount)
    {
        string result = string.Empty;

        for (int i = 0; i < pausesAmount; ++i)
        {
            result += char.ConvertFromUtf32(11);
        }

        return result;
    }
}
