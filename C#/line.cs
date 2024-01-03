using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line : MonoBehaviour
{
    LineRenderer lineRenderer;
    List<Vector3> positions = new List<Vector3>(); // 위치를 저장할 리스트
    public int maxPositions = 1000000; // 최대 저장 점 개수
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

        // 현재 오브젝트의 위치를 리스트에 추가
        time += Time.deltaTime;
        if (time > 0.1f)
        {
            positions.Add(transform.position);
            time = 0;
        }

        // 리스트의 크기가 최대치를 넘었다면 가장 오래된 위치를 제거
        if (positions.Count > maxPositions)
        {
            positions.RemoveAt(0);
        }

        // LineRenderer에 위치를 업데이트
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());

        // 카메라와의 거리를 기반으로 너비를 동적으로 조절
        float distanceToCamera = GetDistanceToCamera();
        float desiredLineWidth = Mathf.Clamp(distanceToCamera * 0.1f, 0.1f, 10f); // 원하는 비율로 너비 조절
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
        return float.MaxValue; // 카메라가 없을 경우 매우 큰 값을 반환
    }

}
