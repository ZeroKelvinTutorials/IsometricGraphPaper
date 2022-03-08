using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDraw : MonoBehaviour
{
    public static TouchDraw Instance;
    void Awake()
    {
        if(Instance==null)
            Instance = this;
        else
            Destroy(this);
    }
    [SerializeField] GameObject linePrefab;
    LineRenderer currentLine;
    public static void StartLine(Vector3 position)
    {
        GameObject lineGameObject = GameObject.Instantiate(Instance.linePrefab, Vector3.zero, Quaternion.identity);
        Instance.currentLine = lineGameObject.GetComponent<LineRenderer>();
        Instance.currentLine.positionCount = 2;
        Instance.currentLine.SetPosition(0,position);
        Instance.currentLine.SetPosition(1,position);
    }
    public static void UpdateLine(Vector3 position)
    {
        Instance.currentLine.SetPosition(1,position);
    }
    public static void FinishLine(Vector3 position)
    {
        Instance.currentLine = null;
        // next lines are commented out because of onmouseup passing onmousedown position 
        // which made the second point same as the first.

        // Instance.currentLine.SetPosition(1,position);
        // Instance.currentLine = null;
    }
}
