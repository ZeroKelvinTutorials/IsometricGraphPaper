using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public static bool pressed;
    void OnMouseDown()
    {
        pressed = true;
        TouchDraw.StartLine(this.transform.position);
    }
    void OnMouseEnter()
    {
        if(pressed)
        {
            TouchDraw.UpdateLine(this.transform.position);
        }
    }
    void OnMouseUp()
    {
        if(pressed)
        {
            TouchDraw.FinishLine(this.transform.position);
            pressed = false;
        }
    }
}
