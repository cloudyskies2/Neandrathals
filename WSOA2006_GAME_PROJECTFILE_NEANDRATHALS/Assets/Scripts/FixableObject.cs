using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixableObject : MonoBehaviour
{
    public GameObject targetPosition; // The position where this object should be placed
    public GameObject dullLight;
    public GameObject litLight;
    public Transform lightSwitch;
    public Transform newLightSwitchTransform;    
    public float placementDistance = 3f; // The required distance to place the object
    public GameObject particles;
    //private GameObject ferrisLights;


    

    // This method can be called when placing the object
    void Start()
    {
        litLight.SetActive(false);
    }
    
    public void PlaceAtTarget()
    {
        
        if (targetPosition.transform != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition.transform.position);

            if (distanceToTarget <= placementDistance)
                {
                transform.position = targetPosition.transform.position;
                transform.rotation = targetPosition.transform.rotation;
                gameObject.transform.parent = targetPosition.transform;

                 lightSwitch.position = newLightSwitchTransform.position;
                lightSwitch.rotation = newLightSwitchTransform.rotation;

               
                //dullLight.SetActive(false);
                litLight.SetActive(true);
                dullLight.SetActive(false);
                particles.SetActive(false);
                }

                 else
                {
                Debug.Log("Object is too far from the target position. Move closer to place.");
                }
        }
    }
}
