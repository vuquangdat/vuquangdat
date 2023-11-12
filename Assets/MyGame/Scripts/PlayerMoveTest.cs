using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveTest : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float jumfore;
    private float horizontalInput;
    private float verticalInput;

    private float yfore;


    private CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 movementDirection = new Vector3 (horizontalInput,0, verticalInput);
        print($"Vector Magnitude before normalize: {movementDirection.magnitude}");

        float magnitude = movementDirection.magnitude;
        magnitude = Mathf.Clamp01( magnitude );

        movementDirection.Normalize();
        print($"Vector Magnitude after normalize: {movementDirection.magnitude}");

        yfore = Physics.gravity.y-Time.deltaTime;

        if(characterController.isGrounded)
        {
            yfore = 0;
            if(Input.GetButtonDown("Jump"))
            {
                yfore = jumfore;
            }
        }

        Vector3 velocity = moveSpeed * movementDirection * magnitude;
        velocity.y = yfore;

        characterController.Move(velocity*Time.deltaTime);
        if(movementDirection != Vector3.zero )
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection,Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed*Time.deltaTime);
        }
    }
}
