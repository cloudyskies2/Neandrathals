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
    public GameObject animationPrefab; // Prefab of the 3D animation to be played
     public Vector3 animationOffset = new Vector3(1, 0, 0); // Offset for the animation from the camera
    public float animationDistance = 3f;
    //private GameObject ferrisLights;
     public string objectText;


    

    // This method can be called when placing the object
    void Start()
    {
        litLight.SetActive(false);
    }
    
/*private void CheckForPickUp()
{
Ray ray = new Ray(playerCamera.position, playerCamera.forward);
RaycastHit hit;
// Perform raycast to detect objects
if (Physics.Raycast(ray, out hit, pickUpRange))
{
// Check if the object has the "PickUp" tag
if (hit.collider.CompareTag("PickUp"))
{
// Display the pick-up text
pickUpText.gameObject.SetActive(true);
pickUpText.text = hit.collider.gameObject.a;
}
else
{
// Hide the pick-up text if not looking at a "PickUp" object
pickUpText.gameObject.SetActive(false);
}
}
else
{
// Hide the text if not looking at any object
pickUpText.gameObject.SetActive(false);
}
}*/
    
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

                PlayAnimationAtOffset();
               
                }

                 else 
                {
                Debug.Log("Object is too far from the target position. Move closer to place.");
                }
        }
    }

        private void PlayAnimationAtOffset()
    {
         if (animationPrefab != null)
        {
            // Get the camera's transform
            Transform cameraTransform = Camera.main.transform;

            // Calculate the position in front of the camera by animationDistance units, with additional offset
            Vector3 animationPosition = cameraTransform.position + cameraTransform.forward * animationDistance + animationOffset;

            // Use the camera's rotation for the animation but optionally modify it
            Quaternion animationRotation = cameraTransform.rotation;

            // Instantiate the animation prefab at the calculated position and rotation
            GameObject animationInstance = Instantiate(animationPrefab, animationPosition, animationRotation);

            // Optionally, destroy the animation after its duration (assumed duration is 3 seconds here)
            Destroy(animationInstance, 2f); 
        }
        else
        {
            Debug.LogWarning("No animation prefab assigned.");
        }
    }

}

