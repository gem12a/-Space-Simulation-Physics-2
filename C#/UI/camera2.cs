using UnityEngine;

public class ArchitecturalCamera : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float turnSpeed = 300.0f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float xRotation = 0.0f;

    void Start()
    {
        // 초기 위치와 회전을 저장
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // 이동
        float hAxis = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float vAxis = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(hAxis, 0, vAxis);

        // 회전
        float mouseX = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * turnSpeed * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.Rotate(Vector3.up * mouseX);
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 초기 위치와 회전으로 리셋하는 키 (예: "R")
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCamera();
        }
    }

    void ResetCamera()
    {
        // 카메라를 초기 위치와 회전으로 리셋
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        xRotation = initialRotation.eulerAngles.x;
    }
}
