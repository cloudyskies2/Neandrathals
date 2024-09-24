using System.Runtime.CompilerServices;
using UnityEngine;

public class FirstPersonControls : MonoBehaviour
{
    [Header("MOVEMENT SETTINGS")]
    [Space(5)]
    // Public variables to set movement and look speed, and the player camera
    public float moveSpeed; // Speed at which the player moves
    public float lookSpeed; // Sensitivity of the camera movement
    public float gravity = -9.81f; // Gravity value
    public float jumpHeight = 1.0f; // Height of the jump
    public Transform playerCamera; // Reference to the player's camera
                                   // Private variables to store input values and the character controller

    private Vector2 moveInput; // Stores the movement input from the player
    private Vector2 lookInput; // Stores the look input from the player
    private float verticalLookRotation = 0f; // Keeps track of vertical camera rotation for clamping
    private Vector3 velocity; // Velocity of the player
    private CharacterController characterController; // Reference to the CharacterController component

    [Header("SHOOTING SETTINGS")]
    [Space(5)]
    public GameObject projectilePrefab; // Projectile prefab for shooting
    public Transform firePoint; // Point from which the projectile is fired
    public float projectileSpeed = 20f; // Speed at which the projectile is fired

    [Header("PICKING UP SETTINGS")]
    [Space(5)]
    public Transform holdPosition; // Position where the picked-up object will be held
    private GameObject heldObject; // Reference to the currently held object
    public float pickUpRange = 3f; // Range within which objects can be picked up
    private bool holdingGun = false;
     private FixableObject fixableObjectScript;

    [Header("CROUCH SETTINGS")]
    [Space(5)]
    public float crouchHeight = 1f; // Height of player when crouching
    public float standingHeight = 2f; // Height of player when standing
    public float crouchSpeed = 1.5f; // Speed at which player moves when crouching
    private bool isCrouching = false; // Whether the player is currently crouching

//<<<<<<< HEAD
//=======
<<<<<<< HEAD
        [Header("THROWING SETTINGS")] //The ENTER key is pressed to charge and throw a held object
=======
      /*  [Header("THROWING SETTINGS")] //The ENTER key is pressed to charge and throw a held object
>>>>>>> main
    [Space(5)]
    public float minThrowForce = 2f; // Minimum force applied when throwing
    public float maxThrowForce = 15f; // Maximum force applied when throwing
    private float currentThrowForce; // Force to apply when throwing
    private bool isChargingThrow = false; // Whether the player is charging the throw
<<<<<<< HEAD
    private GameObject balloon; //The player uses the small yellow balls to shoot at the spheres on the wall
=======
    private GameObject balloon; //The player uses the small yellow balls to shoot at the spheres on the wall*/
>>>>>>> main
//>>>>>>> Sisanda-New-Branch

    [Header("CLIMB SETTINGS")]
    [Space(5)]
    public float climbHeight = 1f; // Height of clmbing intervals
    public float climbSpeed = 1f; // Speed at which player moves when climbing
    public float climbingRange = 1f; // Range whithin a player can climb
    private bool isClimbing = false; // Whether the player is currently climbing
    public GameObject climbObject; // Reference to the object to be climbed
    public Transform climbPosition; // Position to where the climbing will be attached
    //public Vector3 verticalVelocity;
<<<<<<< HEAD
=======

    
>>>>>>> main

    private void Awake()
    {
        // Get and store the CharacterController component attached to this GameObject
        characterController = GetComponent<CharacterController>();
    }
    private void OnEnable()
    {
        // Create a new instance of the input actions
        var playerInput = new Controls();
        // Enable the input actions
        playerInput.Player.Enable();
        // Subscribe to the movement input events
<<<<<<< HEAD
        playerInput.Player.Movement.performed += ctx => moveInput =
        ctx.ReadValue<Vector2>(); // Update moveInput when movement input is performed

        playerInput.Player.Movement.canceled += ctx => moveInput =
        Vector2.zero; // Reset moveInput when movement input is canceled
=======
        playerInput.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>(); // Update moveInput when movement input is performed
        playerInput.Player.Movement.canceled += ctx => moveInput = Vector2.zero; // Reset moveInput when movement input is canceled
>>>>>>> main
                      // Subscribe to the look input events

        playerInput.Player.LookAround.performed += ctx => lookInput =
        ctx.ReadValue<Vector2>(); // Update lookInput when look input is performed
        playerInput.Player.LookAround.canceled += ctx => lookInput =
        Vector2.zero; // Reset lookInput when look input is canceled
                      // Subscribe to the jump input event

        playerInput.Player.Jump.performed += ctx => Jump(); // Call the Jump method when jump input is performed

        // Subscribe to the shoot input event
        playerInput.Player.Shoot.performed += ctx => Shoot(); // Call the Shoot method when shoot input is performed

        // Subscribe to the pick-up input event
        playerInput.Player.PickUp.performed += ctx => PickUpObject(); // Call the PickUpObject method when pick-up input is performed

<<<<<<< HEAD
        playerInput.Player.Crouch.performed += ctx => ToggleCrouch(); // Call the ToggleCrouch method when crouch input is performed

=======
        playerInput.Player.Fix.performed += ctx => FixObject();

        playerInput.Player.Drop.performed += ctx => DropObject();

        playerInput.Player.Crouch.performed += ctx => ToggleCrouch(); // Call the ToggleCrouch method when crouch input is performed

>>>>>>> main
        //playerInput.Player.Climb.performed += ctx => Climb(); // Call the Climb method when climb input is performed
    }
    private void Update()
    {
        // Call Move and LookAround methods every frame to handle player movement and camera rotation
        Move();
        LookAround();
        ApplyGravity();
//<<<<<<< HEAD
        //Climb();
//=======

        // Update the throw charge if charging
<<<<<<< HEAD
        if (isChargingThrow)
        {
            ChargeThrow();
        }
=======
        /*if (isChargingThrow)
        {
            ChargeThrow();
        }*/
>>>>>>> main
//>>>>>>> Sisanda-New-Branch
    }
    public void Move()
    {
        // Create a movement vector based on the input
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

        // Transform direction from local to world space
        move = transform.TransformDirection(move);

        // Move the character controller based on the movement vector and speed
        characterController.Move(move * moveSpeed * Time.deltaTime);

        float currentSpeed;

        if (isCrouching)
        {
            currentSpeed = crouchSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }

    }
    public void LookAround()
    {
        // Get horizontal and vertical look inputs and adjust based on sensitivity
        float LookX = lookInput.x * lookSpeed;
        float LookY = lookInput.y * lookSpeed;

        // Horizontal rotation: Rotate the player object around the y-axis
        transform.Rotate(0, LookX, 0);

        // Vertical rotation: Adjust the vertical look rotation and clamp it to prevent flipping
        verticalLookRotation -= LookY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f,
        90f);

        // Apply the clamped vertical rotation to the player camera
        playerCamera.localEulerAngles = new Vector3(verticalLookRotation,
        0, 0);
    }
    public void ApplyGravity()
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -0.5f; // Small value to keep the player grounded
        }

        velocity.y += gravity * Time.deltaTime; // Apply gravity to the velocity
        characterController.Move(velocity * Time.deltaTime); // Apply the velocity to the character
    }

    public void Jump()
    {
        if (characterController.isGrounded)
        {
            // Calculate the jump velocity
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    public void Shoot()
    {
        if (holdingGun == true)
        {
            // Instantiate the projectile at the fire point
            GameObject projectile = Instantiate(projectilePrefab,
            firePoint.position, firePoint.rotation);

            // Get the Rigidbody component of the projectile and set its velocity
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * projectileSpeed;

            // Destroy the projectile after 3 seconds
            Destroy(projectile, 3f);
        }
    }
    
    public void PickUpObject()
    {
        // Check if we are already holding an object
        if (heldObject != null)
        {
            heldObject.GetComponent<Rigidbody>().isKinematic = false; // Enable physics
            heldObject.transform.parent = null;
            holdingGun = false;
            fixableObjectScript = null;
            return;
        }

        // Perform a raycast from the camera's position forward
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        // Debugging: Draw the ray in the Scene view
        Debug.DrawRay(playerCamera.position, playerCamera.forward * pickUpRange, Color.red, 3f);

        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            // Check if the hit object has the tag "PickUp"
            if (hit.collider.CompareTag("PickUp"))
            {
                // Pick up the object
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics
                fixableObjectScript = heldObject.GetComponent<FixableObject>();
               
                if (fixableObjectScript != null)
                {
                    heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics
                    heldObject.transform.position = holdPosition.position;
                    heldObject.transform.rotation = holdPosition.rotation;
                    heldObject.transform.parent = holdPosition;
                }

                // Attach the object to the hold position
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.rotation = holdPosition.rotation;
                heldObject.transform.parent = holdPosition;
            }
            else if (hit.collider.CompareTag("Gun"))
            {
                // Pick up the object
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;

                // Disable physics
                // Attach the object to the hold position
                heldObject.transform.position = holdPosition.position;
                heldObject.transform.rotation = holdPosition.rotation;
                heldObject.transform.parent = holdPosition;
                holdingGun = true;
            }
        }
    }

<<<<<<< HEAD
//<<<<<<< HEAD
//=======
    void OnCollisionEnter(Collision collision)
=======
     public void FixObject()
    {
        if (heldObject != null && fixableObjectScript != null)
        {
            // Place the object at its target position
            fixableObjectScript.PlaceAtTarget();

            // Reset the state
            heldObject.GetComponent<Rigidbody>().isKinematic = true;
            heldObject.transform.parent = null;
            heldObject = null;
            fixableObjectScript = null;
        }
    }

//<<<<<<< HEAD
//=======
    /*void OnCollisionEnter(Collision collision)
>>>>>>> main
    {

    if(collision.gameObject.CompareTag("Balloon"))
    {
      balloon = collision.gameObject;
      Destroy(balloon);
    }
    }

      public void StartChargingThrow()
    {
        if (heldObject != null)
        {
            isChargingThrow = true;
            currentThrowForce = minThrowForce; // Start with the minimum throw force
        }
    }

      public void ChargeThrow() //Hold down the ENTER key to charge the objects force
    {
        // Increase the throw force over time, clamping to the maximum value
        currentThrowForce += Time.deltaTime * (maxThrowForce - minThrowForce);
        currentThrowForce = Mathf.Clamp(currentThrowForce, minThrowForce, maxThrowForce);
    }

       public void ThrowObject() //Release ENTER key to throw the object
    {
        if (heldObject != null)
        {
            // Enable physics and detach the object
            heldObject.GetComponent<Rigidbody>().isKinematic = false; // Enable physics
            heldObject.transform.parent = null; // Detach from hold position

            // Apply the charged force to "throw" the object
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            rb.AddForce(playerCamera.forward * currentThrowForce, ForceMode.Impulse);

            // Clear the reference to the held object
            heldObject = null;
            holdingGun = false; // Reset holdingGun flag if the dropped object is a gun

            isChargingThrow = false; // Stop charging
        }
<<<<<<< HEAD
    }
=======
    }*/
>>>>>>> main

    //Use the R key to drop the held object
     public void DropObject()
    {
        if (heldObject != null)
        {
            // Enable physics and detach the object
            heldObject.GetComponent<Rigidbody>().isKinematic = false; // Enable physics
            heldObject.transform.parent = null; // Detach from hold position

            // Clear the reference to the held object
            heldObject = null;
            holdingGun = false; // Reset holdingGun flag if the dropped object is a gun
        }
    }

//>>>>>>> Sisanda-New-Branch
    public void ToggleCrouch()
    {
        if (isCrouching)
        {
            characterController.height = standingHeight;
            isCrouching = false;
        }
        else
        {
            characterController.height = crouchHeight;
            isCrouching = true;
        }
    }

    public void Climb()
    {
        // Create a movement vector based on the input
        Vector3 climb = new Vector3(moveInput.x, 0, moveInput.y);

        // Transform direction from local to world space
        climb = transform.TransformDirection(climb);

        // Move the character controller based on the movement vector and speed
        characterController.Move(climb * climbSpeed * Time.deltaTime);

        // Perform a raycast from the camera's position forward
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        /*Title: How to Climb Ladders (First Person, Third Person, Unity Tutorial)
         *Author: Code Monkey
         *Date: 15 August 2024
         *Code version: 2021.3.0f1
         *Availability: https://youtu.be/44qVzrdvm04?si=tQpFxbd5uvkX0ChE
        */


        //if (isClimbing)
        //{

        //}

        Vector3 climbingDirection = Quaternion.Euler(0.0f, climbHeight, 0.0f) * Vector3.forward;

        if (Physics.Raycast(transform.position + Vector3.up, climbingDirection, out RaycastHit raycastHit))
        {
            if (raycastHit.transform.TryGetComponent(out climbObject))
            {
                //GrabClimb();
                climbingDirection.x = 0f;
                climbingDirection.y = climbingDirection.z;
                climbingDirection.z = 0f;
                climbSpeed = 0f;
            }
        }

        characterController.Move(climbingDirection.normalized * (climbSpeed * Time.deltaTime) + new Vector3(0.0f, climbSpeed, 0.0f) * Time.deltaTime);

        // Debugging: Draw the ray in the Scene view
        Debug.DrawRay(playerCamera.position, playerCamera.forward * climbingRange, Color.black, 1f);

        if (Physics.Raycast(ray, out hit, climbingRange))
        {
            if (hit.collider.CompareTag("Climb"))
            {
                // Climb the object
                climbObject = hit.collider.gameObject;
                climbObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics

                // Attach the object to the hold position
                characterController.transform.position = holdPosition.position;
                characterController.transform.rotation = holdPosition.rotation;
                characterController.transform.parent = holdPosition;

                // Pick up the object
                climbObject = hit.collider.gameObject;
                climbObject.GetComponent<Rigidbody>().isKinematic = true;

                // Disable physics
                // Attach the object to the hold position
                climbObject.transform.position = holdPosition.position;
                climbObject.transform.rotation = holdPosition.rotation;
                climbObject.transform.parent = holdPosition;
                isClimbing = true;
            }

            // Check if the hit object has the tag "Ground"
            if (hit.collider.CompareTag("Ground"))
            {
                // Stop climbing the object
                climbObject = hit.collider.gameObject;
                //climbHeight.GetComponent<Rigidbody>().isKinematic = true; // Disable physics

                //characterController.Move(moveInput);
                Move();

            }
        }

        float currentSpeed;

        if (isClimbing)
        {
            currentSpeed = climbSpeed;
            characterController.height = climbHeight;
            isClimbing = true;
        }
        else
        {
            currentSpeed = moveSpeed;
            characterController.height = standingHeight;
            isClimbing = false;
        }

        climbObject.transform.parent = null; // Detach from hold position
        climbObject = null;
    }

    /*private void GrabClimb()
    {
        isClimbing = true;
    }
    private void DropClimb()
    {

    }*/

}