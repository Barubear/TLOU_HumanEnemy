using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionMap : MonoBehaviour
{
    public int mapWidth;
    public int mapHight;
    public float[,] valueMap;
    public VisualMap visualMap;
    public Transform agent;
    public float MaxDis;
    public float spreadRate;

    Vector2Int agentPos;
    // Start is called before the first frame update
    void Start()
    {
        valueMap = new float[mapWidth, mapHight];
        agentPos = visualMap.postionToCell(agent.position);
    }

    // Update is called once per frame
    void Update()
    {
        agentPos = visualMap.postionToCell(agent.position);
        fellValue();
    }

    void fellValue() {
        for (int w = 0; w < mapWidth; w++)
        {
            for (int h = 0; h < mapHight; h++)
            {
                float dis = Vector3.Distance(agent.position, visualMap.visualMap[w, h].transform.position);
                valueMap[w, h] = Mathf.Clamp(1 - spreadRate * (dis / MaxDis),0,1);


            }
        }

    }
}
