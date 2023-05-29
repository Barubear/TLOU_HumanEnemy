using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualMap : MonoBehaviour
{
    public int mapWidth;
    public int mapHight;
    
    public GameObject mapCell;
    public GameObject ground;
    public float visualHifht;
    
    public float cellSize = 1;

    Vector3 startPos;
    float[,] mapValueList ;
    // Start is called before the first frame update
    void Start()
    {
        mapValueList = new float[mapWidth, mapHight];
        Renderer renderer = ground.GetComponent<Renderer>();
        Vector3 size = renderer.bounds.size;
        Vector3 center = renderer.bounds.center;
        startPos = new Vector3(center.x-0.5f*size.x, center.y + visualHifht , center.z+ 0.5f* size.z);

        for (int w = 0; w < mapWidth; w++)
        {
            
            for (int h = 0; h < mapHight; h++)
            {
                mapValueList[w, h] = 0;
                Instantiate(mapCell,new Vector3(startPos.x + cellSize*w, startPos.y, startPos.z - cellSize*h),Quaternion.identity);
                
            }

            

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
