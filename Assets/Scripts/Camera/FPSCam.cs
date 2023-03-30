using UnityEngine;
public class FPSCam : MonoBehaviour
{
    public float mouseSensitivityX = 1f;
    public float mouseSensitivityY = 1f;

    public Transform orientation;

    float xRotation = 0f;
    float yRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update(){
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 45f);

        yRotation += mouseX;

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}