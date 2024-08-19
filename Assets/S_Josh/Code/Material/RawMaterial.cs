using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMaterial : MonoBehaviour
{
    public MaterialType MaterialType;
    public int AmountLeft;
    public int Cost;
    public string Name;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
