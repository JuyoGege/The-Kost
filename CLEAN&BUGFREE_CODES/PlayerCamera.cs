using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    [SerializeField]
    Transform player;

    [Range(1f, 5f)]
    public float mouseSensitivity = 2f;

    private float xAxisClamp = 0f;

    private Vector3 targetRotCam;
    private Vector3 targetRotPlayer;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!FindObjectOfType<Player>().isDead)
        {
            RotateCamera();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotAmountX = mouseX * mouseSensitivity;
        float rotAmountY = mouseY * mouseSensitivity;

        xAxisClamp -= rotAmountY;

        targetRotCam = transform.rotation.eulerAngles;
        targetRotPlayer = player.rotation.eulerAngles;

        targetRotCam.x -= rotAmountY;
        targetRotCam.z = 0f;
        targetRotPlayer.y += rotAmountX;

        if (xAxisClamp > 90)
        {
            xAxisClamp = 90f;
            targetRotCam.x = 90f;
        }

        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90f;
            targetRotCam.x = 270f;
        }

        transform.rotation = Quaternion.Euler(targetRotCam);
        player.rotation = Quaternion.Euler(targetRotPlayer);
    }
}
