using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackCollide : MonoBehaviour
{
    public List<Collider2D> TriggerList = new List<Collider2D>();
    public string layer;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layer))
        {
            TriggerList.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layer))
        {
            TriggerList.Remove(other);
        }
    }

    private void Update()
    {
        //Debug.Log(TriggerList);
    }
}
