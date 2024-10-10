using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;
public class Testing : MonoBehaviour
{

    private Grid<int> grid;
    private Grid<bool> bgrid;
    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid<int>(4, 2, 10f, new Vector3(20,0), (Grid<int> g, int x, int y) => 0);     
        bgrid = new Grid<bool>(2, 2, 10f, new Vector3(0,0), (Grid<bool> g, int x, int y) => false);   
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.SetValue(UtilsClass.GetMouseWorldPosition(), 56);
            bgrid.SetValue(UtilsClass.GetMouseWorldPosition(), true);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
            Debug.Log(bgrid.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
    }

}
