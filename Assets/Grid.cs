using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] GameObject dotPrefab;
    public List<Vector3> positions = new List<Vector3>();

    void Start()
    {
        // GenerateOutwards(Vector3.zero, 10,.5f);
    }
    // void Update()
    // {
    //     GenerateOutwards(Vector3.zero,desiredDegrees,.5f);
    // }
    void GenerateOutwards(Vector3 origin, int degrees, float spacing)
    {
        positions = new List<Vector3>();

        positions.Add(origin);

        List<Vector3> outerPositions = new List<Vector3>();
        outerPositions.Add(origin);

        for(int i = 0; i<degrees; i++)
        {
            List<Vector3> newPositions = new List<Vector3>();
            foreach(Vector3 position in outerPositions)
            {
                for(int j = 0; j<6; j++)
                {
                    float currentRadian = (float)j/6 * 2*Mathf.PI;
                    float newX = position.x + Mathf.Cos(currentRadian)*spacing;
                    float newY = position.y + Mathf.Sin(currentRadian)*spacing;
                    float roundNewX = Mathf.Round(newX * 100f) / 100f;
                    float roundNewY = Mathf.Round(newY * 100f) / 100f;
                    Vector3 newPosition = new Vector3(roundNewX, roundNewY, 0);
                    if(!positions.Contains(newPosition))
                    {
                        positions.Add(newPosition);
                    }
                    if(!newPositions.Contains(newPosition))
                    {
                        newPositions.Add(newPosition);
                    }
                }
            }
            outerPositions = new List<Vector3>(newPositions);
        }
        SpawnDots(positions);
    }
    void SpawnDots(List<Vector3> positions)
    {
        foreach(Vector3 position in positions)
        {
            Instantiate(dotPrefab, position, Quaternion.identity,this.transform);
        }
    }


    //video-related stuff.
        
    public Vector3 originPoint;
    
    public void GenerateAt()
    {
        Vector3 origin =originPoint;
        float spacing = .5f;
        GameObject newObj = Instantiate(dotPrefab, origin, Quaternion.identity);
        newObj.GetComponent<SpriteRenderer>().color = Color.green;
        newObj.transform.parent = this.transform;
        for(int i = 0; i<6; i++)
        {
            float currentRadian = (float)i/6 * 2*Mathf.PI;
            float newX = origin.x + Mathf.Cos(currentRadian)*spacing;
            float newY = origin.y + Mathf.Sin(currentRadian)*spacing;
            float roundNewX = Mathf.Round(newX * 100f) / 100f;
            float roundNewY = Mathf.Round(newY * 100f) / 100f;
            Vector3 newPosition = new Vector3(roundNewX, roundNewY, 0);
            newObj = Instantiate(dotPrefab, newPosition, Quaternion.identity);
            newObj.GetComponent<SpriteRenderer>().color = Color.red;
            newObj.transform.parent = this.transform;
        }
    }
    public void Animate()
    {
        StartCoroutine(AnimateAround());
    }
    IEnumerator AnimateAround()
    {
        GenerateAt();
        for(int i = 0; i<6; i++)
        {
            yield return new WaitForSeconds(1);
            foreach(Transform child in this.transform)
            {
                child.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
            float currentRadian = (float)i/6 * 2*Mathf.PI;
            originPoint = new Vector3(Mathf.Cos(currentRadian)*.5f, Mathf.Sin(currentRadian)*.5f,0);
            GenerateAt();
        }
        yield return null;
    }
}
