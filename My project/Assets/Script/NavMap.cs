using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMap : MonoBehaviour
{
    // Start is called before the first frame update
    public VisualMap VisualMap;
    GameObject[,] visualMap;
    public List<GameObject> wallList;
    float[,] navMap;
    void Start()
    {
        visualMap = VisualMap.visualMap;
        navMap = new float[visualMap.GetLength(0), visualMap.GetLength(1)];
        for (int w = 0; w < visualMap.GetLength(0); w++)
        {

            for (int h = 0; h < visualMap.GetLength(1); h++)
            {
                Bounds cellBound = visualMap[w, h].GetComponent<Renderer>().bounds;
                foreach (GameObject b in wallList) {
                    if (b.GetComponent<Renderer>().bounds.Intersects(cellBound)) {
                        //Destroy(visualMap[w, h]);
                        navMap[w,h] = -1;
                        //Debug.Log("!");
                    }
                }

            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
