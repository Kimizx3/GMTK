using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RawMaterial : ScriptableObject
{
    public MaterialType MaterialType;
    public int AmountLeft;
    public int Cost;
    public string Name;
}

public enum MaterialType
{
    Water,
    CoffeeBean,
    Sugar,
    Milk,
    Ice,
    Cup,

}
