using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMap : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[,] visualMap;
    float[,] navMap;
    void Start()
    {
        navMap = new float[visualMap.GetLength(0), visualMap.GetLength(1)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
