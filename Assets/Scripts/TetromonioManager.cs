using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetromonioManager : MonoBehaviour
{
    #region FIELDS
    [SerializeField] GameObject centerPoint;
    private SpawnManager spawnManager;
    private bool canBeMoved = true;
    #endregion

    private void Awake()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    void Update()
    {
        Move();
    }

    private void Register()
    {
        foreach (Transform block in centerPoint.transform)
        {
            GameManager.Grid[Mathf.FloorToInt(block.position.x), Mathf.FloorToInt(block.position.y)]=block;
        }
    }

    private bool IsMovementValid()
    {
        foreach (Transform block in centerPoint.transform)
        {
            if (block.position.x<=0.5  || block.position.x>=19.5 || Mathf.FloorToInt(block.position.y)<=0.5)
            {
                return false;
            }
            if (GameManager.Grid[Mathf.FloorToInt(block.position.x), Mathf.FloorToInt(block.position.y)] != null)
            {
                return false;
            }
        }
        return true;
    }

    private void Move()
    {
        if (canBeMoved)
        {
            //The Fall
            transform.position += new Vector3(0f, -GameManager.FallFactor, 0f);
            if (!IsMovementValid())
            {
                transform.position -= new Vector3(0f, -GameManager.FallFactor, 0f);
                canBeMoved = false;
                Register();
                spawnManager.Spawn();
                return;
            }

            //The Right Movement
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += Vector3.right;
                if (!IsMovementValid())
                {
                    transform.position -= Vector3.right;
                }
            }

            //The Left Movement
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += Vector3.left;
                if (!IsMovementValid())
                {
                    transform.position -= Vector3.left;
                }
            }

            //The Rotation Movement
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

