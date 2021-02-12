using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class PlayerCameraController : NetworkBehaviour
{
    [Header("Camera")]
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private Camera cameraPlayer = null;
    [SerializeField] private float sensivity = 1.55f;

    [Header("References")]
    [SerializeField] private Health health = null;
    [SerializeField] private Image healthBarImage = null;

    private float rotationOnY;

    public override void OnStartAuthority()
    {
        cameraPlayer.gameObject.SetActive(true);
        enabled = true;
        // pseudoText.text = PlayerPrefs.GetString("Pseudo", "Anonyme");
    }

    private void OnEnable()
    {
        health.OnHealthChanged += HandleHealthChanged;
        sensivity = PlayerPrefs.GetFloat("Sensivity", 1.55f) * 25;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Cursor.visible = !Cursor.visible;
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else if (Cursor.lockState == CursorLockMode.None)
                Cursor.lockState = CursorLockMode.Locked;
        }

        rotationOnY -= mouseY;
        rotationOnY = Mathf.Clamp(rotationOnY, -89f, 89f);
        cameraPlayer.transform.localEulerAngles = new Vector3(rotationOnY, 0f, 0f);
        playerTransform.Rotate(0f, mouseX * sensivity * Time.deltaTime, 0f);
    }

    private void OnDisable()
    {
        health.OnHealthChanged -= HandleHealthChanged;
    }
    private void HandleHealthChanged(object sender, HealthChangedEventArgs e)
    {
        healthBarImage.fillAmount = (float)e.CurrentHealth / e.MaxHealth;
    }
}
