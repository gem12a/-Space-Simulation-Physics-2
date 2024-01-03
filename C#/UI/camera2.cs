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
        // �ʱ� ��ġ�� ȸ���� ����
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // �̵�
        float hAxis = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float vAxis = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(hAxis, 0, vAxis);

        // ȸ��
        float mouseX = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * turnSpeed * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.Rotate(Vector3.up * mouseX);
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // �ʱ� ��ġ�� ȸ������ �����ϴ� Ű (��: "R")
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCamera();
        }
    }

    void ResetCamera()
    {
        // ī�޶� �ʱ� ��ġ�� ȸ������ ����
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        xRotation = initialRotation.eulerAngles.x;
    }
}
