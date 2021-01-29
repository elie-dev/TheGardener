using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListEnnemies : MonoBehaviour
{

    public List<GameObject> ennemies;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addEnnemy(GameObject obj)
    {
        ennemies.Add(obj);
    }

    public void removeEnnemy(GameObject obj)
    {
        ennemies.Remove(obj);
    }
}
