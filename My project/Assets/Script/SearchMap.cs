using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchMap : MonoBehaviour
{
    public int mapWidth;
    public int mapHight;
    public float[,] valueMap;
    public VisualMap visualMap;
    public Vector3 target ;
    public ExposureMap exposureMap;
    public bool isSeeing = false;
    public bool hasTarget = false;
    float currDis = 0;
    public float MaxDis;
    public float spreadSpeed;
    public float spreadRate;
    public float reduRate;
    public float adjustSpeed;
    // Start is called before the first frame update
    void Start()
    {
        valueMap = new float[mapWidth, mapHight];
        for (int w = 0; w < mapWidth; w++)
        {
            for (int h = 0; h < mapHight; h++)
            {
                
                    valueMap[w, h] = 0.5f;

               
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        spread();
        if (isSeeing) {
            currDis = 0;

        }else if(hasTarget) {
            currDis += spreadSpeed;
            if (currDis > MaxDis) {
                //超出可逃跑范围，停止扩散
                currDis = 0;
                hasTarget = false;
            } 
        }
        
        

    }
    void spread()
    {
        float tolerance = 0.01f;
        float dis = MaxDis + 1;
        for (int w = 0; w < mapWidth; w++)
        {
            for (int h = 0; h < mapHight; h++)
            {
                if (hasTarget)//看到目标计算距离
                     dis= Vector3.Distance(target, visualMap.visualMap[w, h].transform.position);
                
                if (dis < MaxDis)//在目标可逃跑范围内
                {

                    valueMap[w, h] = Mathf.Clamp( (1 - (Mathf.Abs(currDis - dis)/ MaxDis) * spreadRate - (currDis/ MaxDis)* reduRate), 0.5f , 1)*5;


                }
                else
                {//不在目标可逃跑范围内
                    
                    if (0.5f - valueMap[w, h] > tolerance)
                    {
                        valueMap[w, h] += adjustSpeed;

                    }
                    if (valueMap[w, h] - 0.5f > tolerance)
                    {
                        valueMap[w, h] -= adjustSpeed;

                    }
                }
                //valueMap[w, h] *= exposureMap.vauleMap[w, h];
            }
        }
    }

    
                
        

    
}
