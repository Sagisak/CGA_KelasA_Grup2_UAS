using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCamera;
    public float tiltAngle = 90f; // Angle to tilt towards the camera

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (mainCamera != null)
        {
            // Calculate the direction to the camera
            Vector3 direction = mainCamera.transform.position - transform.position;

            // Zero out the y component to keep the health bar upright
            direction.y = 0.0f;

            // Rotate the health bar to face the camera
            transform.rotation = Quaternion.LookRotation(direction);

            // Apply tilt
            transform.Rotate(Vector3.right, tiltAngle);
        }
    }
}