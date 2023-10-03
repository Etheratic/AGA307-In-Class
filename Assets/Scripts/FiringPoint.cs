using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringPoint : MonoBehaviour
{
    [Header("Rigidbody projectiles")]
    public GameObject projectileGreenOrb;
    public float projectileSpeed = 1000f;

    [Header("Raycast projectiles")]
    public GameObject hitSparks;
    public LineRenderer laser;


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireRigidbody();
        }

        if (Input.GetButtonDown("Fire2"))
            FireRayCast();
    }

    void FireRigidbody()
    {
        //create a reference to hold our instaciate object
        GameObject projectileInstance;
        //Instanciate our projectile at this objects position and rotation
        projectileInstance = Instantiate(projectileGreenOrb, transform.position, transform.rotation);
        //add force to the prjectile
        projectileInstance.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
    }

    void FireRayCast()
    {
        //create the ray
        Ray ray = new Ray(transform.position, transform.forward);
        //create a reference to hold the info on what we hit
        RaycastHit hit;

        if (Physics.Raycast(ray,out hit, Mathf.Infinity))
        {
            //Debug.Log("we hit" + hit.collider.name + "at point" + hit.point + "which was" + hit.distance + "units away");
            laser.SetPosition(0, transform.position);
            laser.SetPosition(1, hit.point);
            StopAllCoroutines();
            StartCoroutine(StopLaser());

            GameObject particles = Instantiate(hitSparks, hit.point, hit.transform.rotation);
            Destroy(particles, 3);

            if(hit.collider.CompareTag("Target"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }

    IEnumerator StopLaser()
    {
        laser.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        laser.gameObject.SetActive(false);
    }
}
