using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum visualMode
{
    All,
    ExposureMap,
    PositionMap,
    SearchMap,
}
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
    public Transform target;
    public Transform[] patrolPoints;
    public bool auto = true;
    NavMeshAgent NavMeshAgent;
    int patrolPointInt = 0;
    Vector2Int maxCell;
    float maxValue = 0 ;
    A_Start a_Start;
    List<AstartCell> paths;
    bool stop = false;
    public float speed;
    public visualMode visualMode = visualMode.All;
    
    // Start is called before the first frame update
    void Start()
    {
        //a_Start = new A_Start(navMap.navMap);
        //paths = new List<AstartCell>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        maxCell = visualMap.postionToCell(transform.position);
    }
    private void Update()
    {
        if (Input.GetKeyDown("q")) {

            if (stop) Time.timeScale = 1;
            else Time.timeScale = 0;

            stop = !stop;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        float dis = Vector3.Distance(transform.position, target.position);
        
        //抓住目标
        if (dis < 5) {
            Debug.Log(dis);
            Time.timeScale = 0f;
        }

        maxValue = 0;
        Vector2Int currCell = maxCell;
        Vector2Int pos = visualMap.postionToCell(transform.position);
        //更新各点值
        for (int w = 0; w < mapWidth; w++)
        {
            for (int h = 0; h < mapHight; h++)
            {
                float newValue = SearchMap.valueMap[w, h] * PositionMap.valueMap[w,h]* exposureMap.vauleMap[w, h];
                
                if (navMap.navMap[w, h].isWall == false) {
                    if(visualMode == visualMode.All) visualMap.floatmap[w, h] = newValue;
                    else if (visualMode == visualMode.ExposureMap) visualMap.floatmap[w, h] = exposureMap.vauleMap[w, h];
                    else if (visualMode == visualMode.PositionMap) visualMap.floatmap[w, h] = PositionMap.valueMap[w, h];
                    else if (visualMode == visualMode.SearchMap) visualMap.floatmap[w, h] = SearchMap.valueMap[w, h];

                    if (newValue > maxValue)
                    {
                        
                        maxCell = new Vector2Int(w, h);
                        maxValue = newValue;
                    }
                }
                
                
            }
        }
        
        //生成路径
            if (currCell != maxCell && auto)
            {
            //Debug.Log(maxValue );

            if (SearchMap.isSeeing)
            {
                NavMeshAgent.SetDestination(target.position);

            }
            else if (maxValue > 0.5) {
                NavMeshAgent.SetDestination(visualMap.visualMap[maxCell.x,maxCell.y].transform.position);
            }
            else
            {
                Debug.Log("巡逻！");
                patrolPointInt = patrolPointInt % (patrolPoints.Length - 1);
                //按点巡逻
                if (Vector3.Distance(transform.position, patrolPoints[patrolPointInt].position) < 5)
                {
                    patrolPointInt++;
                }
                NavMeshAgent.SetDestination(patrolPoints[patrolPointInt].position);
            }
            }

            

    }

    

    
}
