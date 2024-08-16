using UnityEngine;

public class LadderScript : MonoBehaviour
{
    private bool isOnLadder = false;
    public float ladderClimbSpeed = 3.2f;
    private FirstPersonControls playerControls;
    private CharacterController characterController;

    void Start()
    {
        // This Gets the FirstPersonControls and CharacterController components
        playerControls = GetComponent<FirstPersonControls>();
        characterController = GetComponent<CharacterController>();

      
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            playerControls.enabled = false; // This Disables player controls for ladder climbing
            isOnLadder = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            playerControls.enabled = true; // This is to Re-enable player controls after leaving the ladder
            isOnLadder = false;
        }
    }

    void Update()
    {
        if (isOnLadder)
        {
            Vector3 moveDirection = Vector3.zero;

            if (Input.GetKey("w"))
            {
                // Multiply ladderClimbSpeed by Time.deltaTime first, then multiply the result by Vector3.up
                moveDirection = Vector3.up * (ladderClimbSpeed * Time.deltaTime);
            }
            else if (Input.GetKey("s"))
            {
                // Multiply ladderClimbSpeed by Time.deltaTime first, then multiply the result by Vector3.down
                moveDirection = Vector3.down * (ladderClimbSpeed * Time.deltaTime);
            }

            if (moveDirection != Vector3.zero)
            {
                characterController.Move(moveDirection);
            }
        }
    }
}

