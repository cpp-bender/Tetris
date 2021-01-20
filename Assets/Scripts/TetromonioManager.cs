using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetromonioManager : MonoBehaviour
{
    [SerializeField] GameObject centerPoint;
    private float fallFactor = 0.01f;
    private bool canBeMoved = true;


    // Update is called once per frame
    void Update()
    {
        Move(canBeMoved);
    }

    private void Move(bool canBeMoved)
    {
        if (!canBeMoved)
        {
            return;
        }
        transform.position += new Vector3(0f, -fallFactor, 0f);
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left;
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0f, -fallFactor * 5, 0f);
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            centerPoint.transform.eulerAngles -= new Vector3(0f, 0f, -90f);
        }
    }
}
