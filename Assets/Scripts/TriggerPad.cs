using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPad : MonoBehaviour
{
    public GameObject triggeredObject;
    private void OnTriggerEnter(Collider other)
    {
        //change the colour of the triggered object
        triggeredObject.GetComponent<Renderer>().material.color = Color.blue;
    }

    private void OnTriggerStay(Collider other)
    {
        //increase the sized of the triggered object by 0.01f
        triggeredObject.transform.localScale += Vector3.one * 0.01f;
        triggeredObject.transform.position += Vector3.one * 0.01f;
    }
    private void OnTriggerExit(Collider other)
    {
        //reset the size
        triggeredObject.transform.localScale = Vector3.one;
        //revert the colour
        triggeredObject.GetComponent<Renderer>().material.color = Color.white;
    }
}
