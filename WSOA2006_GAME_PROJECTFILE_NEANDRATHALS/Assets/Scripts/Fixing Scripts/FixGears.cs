using UnityEngine;

public class FixGears : MonoBehaviour
{
    // References to objects in the scene
    public Transform trainPartPosition;  // Position where broken part needs to go
    public Transform brokenPart;         // The broken part object
    public Transform lightSwitch;        // The light switch object
    public Light lampLight;              // The lamp light object

    // Distance threshold to snap the part to the correct position
    public float snapDistance = 0.5f;

    // Desired rotations for the broken part and the light switch
    public Vector3 brokenPartFixedRotation = new Vector3(0, 0, 0);  // Adjust as needed
    public Vector3 lightSwitchOnRotation = new Vector3(0, 90, 0);   // Adjust as needed

    // Flag to track whether the part is in place
    private bool isPartInPlace = false;

    void Update()
    {
        // Check distance between the broken part and the target position on the train
        float distanceToTrainPart = Vector3.Distance(brokenPart.position, trainPartPosition.position);

        if (!isPartInPlace && distanceToTrainPart <= snapDistance)
        {
            // Snap the broken part into place
            brokenPart.position = trainPartPosition.position;
            brokenPart.rotation = Quaternion.Euler(brokenPartFixedRotation);
            isPartInPlace = true;

            // Rotate the light switch to indicate it's turned on
            lightSwitch.rotation = Quaternion.Euler(lightSwitchOnRotation);

            // Turn on the lamp light
            lampLight.enabled = true;
        }
    }
}
