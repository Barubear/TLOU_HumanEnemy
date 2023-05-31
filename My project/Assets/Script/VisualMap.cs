using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualMap : MonoBehaviour
{
    public int mapWidth;
    public int mapHight;
    
    public GameObject mapCell;
    public GameObject ground;

    public NavMap NavMap;
    public float visualHifht;
    
    public float cellSize = 1;

    public Vector3 startPos;
    public GameObject[,] visualMap;
    // Start is called before the first frame update
    void Awake()
    {
        visualMap = new GameObject[mapWidth, mapHight];
        Renderer renderer = ground.GetComponent<Renderer>();
        Vector3 size = renderer.bounds.size;
        Vector3 center = renderer.bounds.center;
        startPos = new Vector3(center.x-0.5f*size.x, center.y + visualHifht , center.z+ 0.5f* size.z);

        for (int w = 0; w < mapWidth; w++)
        {
            
            for (int h = 0; h < mapHight; h++)
            {
                
                Vector3 newPos = new Vector3(startPos.x + cellSize * w, startPos.y, startPos.z - cellSize * h);



                visualMap[w, h] = Instantiate(mapCell, newPos, Quaternion.identity); 
                
                
                
               
                
                
            }

            

        }
    }

    // Update is called once per frame
    void Update()
    {
        visualize(NavMap.floatNavmap);
    }
    void visualize(float[,] Map)
    {
        for (int w = 0; w < Map.GetLength(0); w++)
        {

            for (int h = 0; h < Map.GetLength(1); h++)
            {
                if (Map[w, h] == -1)
                {
                    visualMap[w, h].GetComponent<Renderer>().material.color = Color.black;
                }
                if (Map[w, h] == 1)
                {
                    visualMap[w, h].GetComponent<Renderer>().material.color = Color.green;
                }
                if (Map[w, h] == 2)//ÖÕµã
                {
                    visualMap[w, h].GetComponent<Renderer>().material.color = Color.red;
                }
                if (Map[w, h] == 3)//Æðµã
                {
                    visualMap[w, h].GetComponent<Renderer>().material.color = Color.yellow;
                }
            }

        }
    }
}   
