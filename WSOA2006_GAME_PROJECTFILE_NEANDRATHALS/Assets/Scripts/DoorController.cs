using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private float triggerDistance = 3f;
    public Transform player;
    public Animator doorAnimator;

    public void OpenDoor()
    {
        // Check the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If the player is within the trigger distance, open the door
        if (distanceToPlayer <= triggerDistance)
        {
            // Trigger the door animation (ensure "Open" trigger is set in Animator)
            doorAnimator.SetTrigger("Open");
        }
    }
}
