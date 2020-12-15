using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyAttack : MonoBehaviour
{
    public GameObject coneAngle;
    public GameObject coneAttack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeAngleAttack(int angle)
    {
        var rotationVector = coneAngle.transform.rotation.eulerAngles;
        rotationVector.z = angle;
        coneAngle.transform.rotation = Quaternion.Euler(rotationVector);
    }
}
