using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu()]
public class PlacedObjectTypeSO : ScriptableObject
{
    public static Dir GetNextDir(Dir dir)
    {
        return dir switch
        {
            Dir.Up => Dir.Right,
            Dir.Right => Dir.Down,
            Dir.Down => Dir.Left,
            Dir.Left => Dir.Up,
            _ => Dir.Up
        };
    }

    public enum Dir
    {
        Up,
        Right,
        Down,
        Left
    }
    public string ObjectName;
    public Transform Prefab;
    public Transform Visual;
    public int Width;
    public int Height;

    public int GetRoatationAngle(Dir dir)
    {
        return dir switch
        {
            Dir.Up => 0,
            Dir.Right => 90,
            Dir.Down => 180,
            Dir.Left => 270,
            _ => 0
        };
    }
    public Vector2Int GetRotationOffset(Dir dir)
    {
        return dir switch
        {
            Dir.Up => new Vector2Int(0, 0),
            Dir.Right => new Vector2Int(Height, 0),
            Dir.Down => new Vector2Int(Width, Height),
            Dir.Left => new Vector2Int(0, Width),
            _ => new Vector2Int(0, 0)
        };
    }

    public List<Vector2Int> GetGridPositionList(Vector2Int origin, Dir dir)
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>();
        switch(dir) {
            default:
            case Dir.Down:
            case Dir.Up:
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        gridPositionList.Add(origin + new Vector2Int(x, y));
                    }
                }
                break;
            case Dir.Left:
            case Dir.Right:
                for (int x = 0; x < Height; x++)
                {
                    for (int y = 0; y < Width; y++)
                    {
                        gridPositionList.Add(origin + new Vector2Int(x, y));
                    }
                }
                break;
        }
        return gridPositionList;
        
        
    }
}
