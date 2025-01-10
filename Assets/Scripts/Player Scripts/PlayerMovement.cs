using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed, gravity = -9.81f;
    private float xMovementInput, yMovementInput;
    private Camera playerCam;
    private CharacterController cc;


    void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
        playerCam = gameObject.GetComponentInChildren<Camera>();
    }

    void Update()
    {
        // Calls from Player Input to retrieve player's movement axes.
        PlayerMove();
    }

    // Translates the player's movement input axes into relative camera movement
    private void PlayerMove()
    {
        xMovementInput = Input.GetAxisRaw("Horizontal");
        yMovementInput = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = playerCam.transform.forward * yMovementInput + playerCam.transform.right * xMovementInput;
        Vector3 gravityForce = new Vector3(0, gravity, 0);
        cc.Move(moveDirection * playerSpeed * Time.deltaTime);
        cc.Move(gravityForce * Time.deltaTime);

    }
}
