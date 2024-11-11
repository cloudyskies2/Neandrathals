using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixableObject : MonoBehaviour
{

    public enum InteractionType { Fix, Observe, TurnOnLights }
    public InteractionType interactionType;
    public GameObject targetPosition; // The position where this object should be placed
    public GameObject dullLight;
    public GameObject litLight;
    public Transform lightSwitch;
    public Transform newLightSwitchTransform;    
    public float placementDistance = 6f; // The required distance to place the object
    public GameObject particles;
    public GameObject animationPrefab; // Prefab of the 3D animation to be played
     public Vector3 animationOffset = new Vector3(0, 0, 0); // Offset for the animation from the camera
    public float animationDistance = 3f;
    //private GameObject ferrisLights;
     public string objectText;

    public GameObject player;         // The player object
    public GameObject popUpObject;    // The object that pops up (e.g., a UI or visual cue)
    public float activationDistance = 9f;  // Distance at which the pop-up appears
    private bool isPickedUp = false; 
    private FirstPersonControls firstPersonControlsScript;



    

    // This method can be called when placing the object
    void Start()
    {
        litLight.SetActive(false);

    }
    
    private void Update()
    {
        // Check distance between player and the object that holds this script
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        // If the player is within activation distance and the object has not been picked up
        if (distanceToPlayer <= activationDistance && !isPickedUp)
        {
            popUpObject.SetActive(true);  // Show the pop-up object
        }
        else
        {
            popUpObject.SetActive(false); // Hide the pop-up object if the player is far away or if the object is picked up
        }
    }

    public void PerformInteraction (GameObject heldObject = null)
    {
       switch (interactionType)
    {
        case InteractionType.Fix:
            if (heldObject != null)
            {
                PlaceAtTarget(heldObject);
            }
            else
            {
                Debug.LogWarning("Fix interaction requires a held object.");
            }
            break;
        case InteractionType.Observe:
            if (heldObject != null)
            {
                ObserveObject(heldObject);
            }
            else
            {
                Debug.LogWarning("Observe interaction requires a held object.");
            }
            break;
        case InteractionType.TurnOnLights:
            TurnOnLights();
            break;
    }
    }
    
   /* public void PlaceAtTarget()
    {
        
        if (targetPosition.transform != null )
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
    }*/

     public void PlaceAtTarget(GameObject heldObject)
    {
        
        if (targetPosition.transform != null )
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
             
                {
       
                } 
                litLight.SetActive(true);
                //dullLight.SetActive(false);
                particles.SetActive(false);

                PlayAnimationAtOffset();
                
               
                }

                 else 
                {
                Debug.Log("Object is too far from the target position. Move closer to place.");
                }
        }
    }


    public void ObserveObject(GameObject heldObject)
    {
        firstPersonControlsScript = heldObject.GetComponent<FirstPersonControls>();
        firstPersonControlsScript.ToggleInspectionMode();
    }

    public void TurnOnLights()

    {
        if (targetPosition.transform != null )
        {
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition.transform.position);
            

            if (distanceToTarget <= placementDistance)
                {
                litLight.SetActive(true);

                lightSwitch.position = newLightSwitchTransform.position;
                lightSwitch.rotation = newLightSwitchTransform.rotation;

                
               
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

        // Set the rotation so that the front of the picture frame faces the camera
        Quaternion animationRotation = Quaternion.LookRotation(cameraTransform.position - animationPosition, Vector3.up);

        // Instantiate the animation prefab at the calculated position and rotation
        GameObject animationInstance = Instantiate(animationPrefab, animationPosition, animationRotation);

        animationInstance.transform.localScale *= 0.2f; // Scale it down (0.5f means half the size)

        // Optionally, destroy the animation after its duration (assumed duration is 2 seconds here)
        Destroy(animationInstance, 2f);
    }
    else
    {
        Debug.LogWarning("No animation prefab assigned.");
    }
    }

     // Call this function from the pickup script when the object is picked up
    public void OnPickedUp()
    {
        isPickedUp = true;          // Mark the object as picked up
        popUpObject.SetActive(false); // Permanently hide the pop-up object
    }

}

