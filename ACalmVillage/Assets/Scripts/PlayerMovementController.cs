using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMovementController : NetworkBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private CharacterController controller = null;
    [SerializeField] private float jumpForce = 2.5f;
    private Dictionary<string, KeyCode> keybindings = new Dictionary<string, KeyCode>();
    private float verticalVelocity;

    private Vector2 previousInput;
    private Vector2 currentInput;

    private void initKeybindings()
    {
        keybindings.Clear();
        keybindings.Add("Up", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "Z")));
        keybindings.Add("Down", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "S")));
        keybindings.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "Q")));
        keybindings.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        keybindings.Add("Inventory", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Inventory", "E")));
        keybindings.Add("Interact", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "F")));
        keybindings.Add("Jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space")));
        keybindings.Add("Micro", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Micro", "V")));
        keybindings.Add("Crouch", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Crouch", "LeftControl")));
    }
    public override void OnStartAuthority()
    {
        enabled = true;
        initKeybindings();
    }

    [ClientCallback]
    private void Update()
    {
        Move();
        //Jump();
    }

    [Client]
    private void Jump()
    {
        if (Input.GetKeyDown(keybindings["Jump"]) && controller.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpForce * -2.0f * Physics.gravity.y);
        }
        else if (controller.isGrounded)
            verticalVelocity = 0f;
        else
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
    }
        
    [Client]
    private void Move()
    {
        currentInput.x = currentInput.y = 0;
        if (Input.GetKey(keybindings["Up"]))
        {
            currentInput.y = 1;
        }
        else if (Input.GetKey(keybindings["Down"]))
        {
            currentInput.y = -1;
        }
        if (Input.GetKey(keybindings["Left"]))
        {
            currentInput.x = -1;
        }
        else if (Input.GetKey(keybindings["Right"]))
        {
            currentInput.x = 1;
        }
        Vector3 right = controller.transform.right;
        Vector3 forward = controller.transform.forward;
        right.y = forward.y = 0f;
        Vector3 movement = right.normalized * currentInput.x + forward.normalized * currentInput.y;
        controller.Move(movement * movementSpeed * Time.deltaTime);
    }
}
