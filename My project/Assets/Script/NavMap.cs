using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMap : MonoBehaviour
{
    // Start is called before the first frame update
    public VisualMap VisualMap;
    GameObject[,] visualMap;
    public List<GameObject> wallList;
    AstartCell[,] navMap;
    public float[,] floatNavmap;
    public Transform testEndPos;

    //A*
     

    void Start()
    {
        visualMap = VisualMap.visualMap;
        navMap = new AstartCell[visualMap.GetLength(0), visualMap.GetLength(1)];
        floatNavmap = new float[visualMap.GetLength(0), visualMap.GetLength(1)];
        for (int w = 0; w < visualMap.GetLength(0); w++)
        {

            for (int h = 0; h < visualMap.GetLength(1); h++)
            {
                Bounds cellBound = visualMap[w, h].GetComponent<Renderer>().bounds;
                bool iswall = false;
                foreach (GameObject b in wallList) {
                    if (b.GetComponent<Renderer>().bounds.Intersects(cellBound)) {
                        iswall = true;
                        floatNavmap[w, h] = -1;
                    }
                }
                navMap[w, h] = new AstartCell(w, h, iswall);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        testInput();
    }
    public void testInput() {

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool res = Physics.Raycast(ray, out hit);
            if (res == true) {
                Vector2Int sp = getCell(hit.point);
                floatNavmap[sp.x, sp.y] = 3;
                Vector2Int ep = getCell(testEndPos.position);
                floatNavmap[ep.x, ep.y] = 2;
                A_Start a_Start = new A_Start(navMap);
                //List<AstartCell> paths = a_Start.getPath(a_Start.navMap[sp.x, sp.y], a_Start.navMap[ep.x, ep.y]);
            }
            
        
        }

    }

    Vector2Int getCell(Vector3 pos) {
        return new Vector2Int((int)Mathf.Floor((pos.x - VisualMap.startPos.x) / VisualMap.cellSize),
                                (int)Mathf.Floor((VisualMap.startPos.z - pos.z ) / VisualMap.cellSize));
    }
}

public class A_Start
{
    public AstartCell[,] navMap;
    int mapHight;
    int mapWhight;
    List<AstartCell> openList;
    List<AstartCell> closeList;
    public A_Start(AstartCell[,] navMap)
    {
        openList = new List<AstartCell>();
        closeList = new List<AstartCell>();
        this.navMap = navMap;
        mapWhight = navMap.GetLength(0);
        mapHight = navMap.GetLength(1);


    }
    int sortOpenlist(AstartCell cell1, AstartCell cell2)
    {
        if (cell1.F_Dis > cell2.F_Dis) return 1;
        else if (cell1.F_Dis < cell2.F_Dis) return -1;
        else return 0;
    }

    public List<AstartCell> getPath(AstartCell startPoint, AstartCell Endpoint)
    {
        startPoint.fatherCell = null;
        startPoint.G_Dis = 0;
        closeList.Add(startPoint);
        List<AstartCell> path = new List<AstartCell>();
        while(true)
        {
            int curX = startPoint.x;
            int curY = startPoint.y;
            //左
            addCell(curX - 1, curY, startPoint, Endpoint);
            //右
            addCell(curX + 1, curY, startPoint, Endpoint);
            //上
            addCell(curX, curY - 1, startPoint, Endpoint);
            //下
            addCell(curX, curY + 1, startPoint, Endpoint);

            openList.Sort(sortOpenlist);
            startPoint = openList[0];


            closeList.Add(startPoint);
            openList.Remove(startPoint);
            if (startPoint == Endpoint)
            {


                path.Add(Endpoint);
                while (Endpoint != null)
                {
                    path.Add(Endpoint.fatherCell);
                    Endpoint = Endpoint.fatherCell;

                }
                
                path.Reverse();

                return path;
            }

        }
        

        return path;
    }

    void addCell(int x , int y, AstartCell startPoint, AstartCell Endpoint) {
        Debug.Log(x+","+y+ "," + mapWhight +","+mapHight);
        if (x >= 0 && x < mapWhight && y >= 0 && y < mapHight
            ) {
            AstartCell newcell = navMap[x, y];
            newcell.fatherCell = startPoint;
            newcell.G_Dis = startPoint.G_Dis + 1;
            newcell.H_Dis = getHdis(x, y, Endpoint.x, Endpoint.y);
            newcell.F_Dis = newcell.G_Dis + newcell.H_Dis;
            
            openList.Add(newcell);
        }
        
    }

    float getHdis(int x, int y, int x2, int y2)
    {

        return Mathf.Abs(x2 - x) + Mathf.Abs(y2 - y);


    }
    

}


public class AstartCell
{
    public float F_Dis;//总
    public float G_Dis;//离起点
    public float H_Dis;//预估
    public AstartCell fatherCell;
    public int x;
    public int y;
    public bool isWall;

    public AstartCell(int x, int y, bool isWall) {
        this.x = x;
        this.y = y;
        this.isWall = isWall;
    }
    
}
