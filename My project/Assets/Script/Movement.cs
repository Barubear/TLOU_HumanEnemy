using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public int mapWidth;
    public int mapHight;
    public ExposureMap exposureMap;
    public NavMap navMap;
    public PositionMap PositionMap;
    public SearchMap SearchMap;
    public VisualMap visualMap;
    Vector2Int maxCell;
    float maxValue = 0 ;
    A_Start a_Start;
    List<AstartCell> paths;
    // Start is called before the first frame update
    void Start()
    {
        //a_Start = new A_Start(navMap.navMap);
        
    }

    // Update is called once per frame
    void Update()
    {
        maxValue = 0;
        Vector2Int pos = visualMap.postionToCell(transform.position);
        for (int w = 0; w < mapWidth; w++)
        {
            for (int h = 0; h < mapHight; h++)
            {
                //float newValue = exposureMap.vauleMap[w,h] * PositionMap.valueMap[w,h] * SearchMap.valueMap[w,h];
                float newValue =  SearchMap.valueMap[w, h];
                visualMap.floatmap[w,h] = newValue;
                if (newValue > maxValue) {
                    maxCell = new Vector2Int(w, h);
                    maxValue = newValue;
                }
                
            }
        }
        Debug.Log(maxCell);
        //paths = a_Start.getPath(navMap.navMap[pos.x,pos.y], navMap.navMap[maxCell.x, maxCell.y]);

    }
}
