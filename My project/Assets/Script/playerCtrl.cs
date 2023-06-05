using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCtrl : MonoBehaviour
{
    public float speed;
    Rigidbody rig;
    Vector3 moveTo;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveTo = new Vector3(Input.GetAxis("Horizontal"),transform.position.y,Input.GetAxis("Vertical"));
        rig.MovePosition(rig.position + moveTo * Time.deltaTime * speed);
    }
}
