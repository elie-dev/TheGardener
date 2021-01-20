using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OrderLayerPosition : MonoBehaviour
{

    public Transform target;
    public float y;
    public int order1;
    public int order2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target.position.y < y)
        {
            GetComponent<TilemapRenderer>().sortingOrder = order2;
        } else
        {
            GetComponent<TilemapRenderer>().sortingOrder = order1;
        }
    }
}
