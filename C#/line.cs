using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line : MonoBehaviour
{
    LineRenderer lineRenderer;
    List<Vector3> positions = new List<Vector3>(); // ��ġ�� ������ ����Ʈ
    public int maxPositions = 1000000; // �ִ� ���� �� ����
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        maxPositions = 1500;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // ���� ������Ʈ�� ��ġ�� ����Ʈ�� �߰�
        time += Time.deltaTime;
        if (time > 0.1f)
        {
            positions.Add(transform.position);
            time = 0;
        }

        // ����Ʈ�� ũ�Ⱑ �ִ�ġ�� �Ѿ��ٸ� ���� ������ ��ġ�� ����
        if (positions.Count > maxPositions)
        {
            positions.RemoveAt(0);
        }

        // LineRenderer�� ��ġ�� ������Ʈ
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());

        // ī�޶���� �Ÿ��� ������� �ʺ� �������� ����
        float distanceToCamera = GetDistanceToCamera();
        float desiredLineWidth = Mathf.Clamp(distanceToCamera * 0.1f, 0.1f, 10f); // ���ϴ� ������ �ʺ� ����
        lineRenderer.startWidth = desiredLineWidth;
        lineRenderer.endWidth = desiredLineWidth;
    }
    float GetDistanceToCamera()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            return Vector3.Distance(transform.position, mainCamera.transform.position);
        }
        return float.MaxValue; // ī�޶� ���� ��� �ſ� ū ���� ��ȯ
    }

}
