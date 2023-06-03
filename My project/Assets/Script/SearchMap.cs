using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchMap : MonoBehaviour
{
    public int mapWidth;
    public int mapHight;
    float[,] valueMap;
    
    // Start is called before the first frame update
    void Start()
    {
        valueMap = new float[mapWidth, mapHight];
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        
        

    }

    void ValueAdjust() {
        float tolerance = 0.01f;
        for (int w = 0; w < mapWidth; w++)
        {
            for (int h = 0; h < mapHight; h++)
            {
                if (0.5f - valueMap[w,h] > tolerance)
                {
                    valueMap[w, h] += 0.01f;
                    
                }
                if (valueMap[w, h] - 0.5f > tolerance)
                {
                    valueMap[w, h] -= 0.01f;
                    
                }
            }
        }
                
        

    }
}
