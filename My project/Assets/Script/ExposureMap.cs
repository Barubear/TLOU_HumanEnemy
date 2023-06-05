using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExposureMap : MonoBehaviour
{
    //workingMap
    public Transform Agent;
    public Transform target;
    public VisualMap VisualMap;
    public SearchMap SearchMap;
    public float visualDis = 90 ;
    public float visualAng = 30;
    public int mapWidth;
    public int mapHight;
    public float[,] vauleMap;
    List<Vector2Int> exposurePos;

    void Start()
    {
        vauleMap = new float[VisualMap.visualMap.GetLength(0), VisualMap.visualMap.GetLength(1)];
    }

    // Update is called once per frame
    void Update()
    {
        updataCell();
       Debug.DrawRay(Agent.position, Agent.forward*10, Color.red);
        exposurePos = new List<Vector2Int>();
    }


    void updataCell()
    {
        Vector2Int AgentPos = VisualMap.postionToCell(Agent.position);
        
        for (int w = 0; w < mapWidth; w++)
        {
            for (int h = 0; h < mapHight; h++)
            {
                
                Transform cell = VisualMap.visualMap[w, h].transform;
                //¾àÀë
                vauleMap[w, h] = 1;
                float dis = Vector2.Distance(new Vector2(w, h), AgentPos);
                if (dis <= visualDis) {

                    //Éí±ß
                    if (dis <= visualDis / 3)
                    {
                        vauleMap[w, h] = 0;
                        
                    }
                    else {
                        //½Ç¶È
                        float angle = Vector2.Angle(new Vector2(Agent.forward.x, Agent.forward.z), new Vector2((cell.position - Agent.position).x, (cell.position - Agent.position).z));
                        //Debug.DrawRay(Agent.position, AgentPos - new Vector2(w, h) ,Color.red);
                        if (angle < visualAng)
                        {

                            //ÉäÏß
                            vauleMap[w, h] = 0;
                            RaycastHit hit;
                            if (Physics.Raycast(Agent.position, cell.position - Agent.position, out hit, Vector2.Distance(new Vector2(w, h), AgentPos)))
                            {
                                if (hit.collider != null)
                                {
                                    if (hit.collider.gameObject.tag == "wall")
                                    {
                                        //Debug.DrawRay(Agent.position, new Vector3(cell.position.x, Agent.position.y, cell.position.z) - Agent.position, Color.red);
                                        vauleMap[w, h] = 1;
                                    }

                                }

                            }
                        }
                    
                        
                        
                    }
                }
                

            }
        }

        Vector2Int targetPos = VisualMap.postionToCell(target.position);

        if (vauleMap[targetPos.x, targetPos.y] == 0)
        {
            Debug.Log("SEE!");
            SearchMap.valueMap[targetPos.x, targetPos.y] = 1;
            SearchMap.isSeeing = true;
            SearchMap.hasTarget = true;
            SearchMap.target = target.position;
        }
        else {
            SearchMap.isSeeing = false;
        }
        
    }
}
