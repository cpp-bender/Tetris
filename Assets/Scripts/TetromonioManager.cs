using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetromonioManager : MonoBehaviour
{
    [SerializeField] GameObject centerPoint;
    private SpawnManager spawnManager;
    private readonly static float fallFactor = 0.05f;
    private readonly static int width = 20;
    private readonly static int height = 20;
    private bool canBeMoved = true;
    private static Transform[,] grid=new Transform[width, height];

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
            grid[Mathf.FloorToInt(block.position.x), Mathf.FloorToInt(block.position.y)]=block;
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
            if (grid[Mathf.FloorToInt(block.position.x), Mathf.FloorToInt(block.position.y)] != null)
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
            transform.position += new Vector3(0f, -fallFactor, 0f);
            if (!IsMovementValid())
            {
                transform.position -= new Vector3(0f, -fallFactor, 0f);
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

