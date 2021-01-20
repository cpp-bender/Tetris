using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetromonioManager : MonoBehaviour
{
    [SerializeField] GameObject centerPoint;
    private float fallFactor=0.01f;
    public bool canBeSpawned { get;  set; }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private bool IsMovementValid()
    {
        foreach (Transform block in centerPoint.transform)
        {
            if (block.position.x<=-9  || block.position.x>=12 || Mathf.Floor(block.position.y)<=-2)
            {
                return false;
            }
        }
        return true;
    }

    private void Move()
    {
        if (IsMovementValid())
        {
            transform.position += new Vector3(0f, -fallFactor, 0f);
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += Vector3.right;
                if (!IsMovementValid())
                {
                    transform.position -= Vector3.right;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += Vector3.left;
                if (!IsMovementValid())
                {
                    transform.position -= Vector3.left;
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                centerPoint.transform.eulerAngles -= new Vector3(0f, 0f, 90f);
                if (!IsMovementValid())
                {
                    centerPoint.transform.eulerAngles += new Vector3(0f, 0f, 90f);
                }
            }
        }
    }
}
