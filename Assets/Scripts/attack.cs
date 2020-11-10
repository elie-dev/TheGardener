using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    public float angleAttack;
    public GameObject coneAngle;
    public GameObject coneAttack;

    public int damage;

    public List<Collider2D> ennemiesHits;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAttack()
    {
        Collider2D[] hitEnnemies = coneAttack.GetComponent<attackCollide>().TriggerList.ToArray();
        foreach (Collider2D ennemy in hitEnnemies)
        {
            ennemy.gameObject.GetComponent<units>().takeDamage(damage);
        }
    }

    public void changeAngleAttack(int angle)
    {
        var rotationVector = coneAngle.transform.rotation.eulerAngles;
        rotationVector.z = angle;
        coneAngle.transform.rotation = Quaternion.Euler(rotationVector);
    }
}
