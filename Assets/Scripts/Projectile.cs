using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public int damage = 20;
    void Start()
    {
        Destroy(this.gameObject,5);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //check if we hit the object tagged target
        if(collision.gameObject.CompareTag("Target"))
        {
            if(collision.gameObject.GetComponent<Target>() != null)
            {
                collision.gameObject.GetComponent<Target>().Hit();
            }
            //change the colour of the target
            //collision.gameObject.GetComponent<Renderer>().material.color = Color.green;
            //destroy target after 1 second
            //Destroy(collision.gameObject, 1);
            //destroy this object
           // Destroy(this.gameObject);
           
        }
        
    }



}
