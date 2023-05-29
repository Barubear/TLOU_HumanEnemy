using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellColliderCheck : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isWall = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "wall")
        {
            isWall = true;
            Debug.Log("iswall");
        }
        else isWall = false;
    }
    
}
