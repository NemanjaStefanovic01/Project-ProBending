using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [Header("Variables")]
    [Range(0.5f, 10f)]
    public float mouseSensitivity;
    float sensMultiplayer = 100;

    [Header("Referances")]
    public Transform playerOrientation;
    public GameObject playerObject;

    float xRot;
    float yRot;

    private void Start()
    {
        //Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //Get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSensitivity*sensMultiplayer;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseSensitivity*sensMultiplayer;

        yRot += mouseX; //Left right
        xRot -= mouseY; //Up down

        //Clamp x rotation (up and down) to 90 deg
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        //Rotate cam and rotation
        this.transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        playerOrientation.rotation = Quaternion.Euler(0, yRot, 0);

        //Rotate the player object
        playerObject.transform.rotation = Quaternion.Euler(0, yRot, 0);
    }
}
