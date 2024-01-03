using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distance : MonoBehaviour
{
    LineRenderer lineRenderer;
    Rigidbody rigidbody1;
    public List<float> masses = new List<float>();
    public Rigidbody centralBody;
    public List<Rigidbody> bodies = new List<Rigidbody>();

    baseplanet baseplanet;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        
        baseplanet = transform.parent.GetComponent<baseplanet>();
        rigidbody1 = this.GetComponent<Rigidbody>();

        lineRenderer.positionCount = 2; // 두 개의 꼭지점을 가진 선
        
        sun();



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distanceToCamera = GetDistanceToCamera();
        float desiredLineWidth = Mathf.Clamp(distanceToCamera * 0.1f, 0.1f, 10f); // 원하는 비율로 너비 조절
        lineRenderer.startWidth = desiredLineWidth;
        lineRenderer.endWidth = desiredLineWidth;
        sun();

        if (bodies.Count>=1) // centralBody가 null이 아닌 경우에만 접근
        {
            
            lineRenderer.SetPosition(0, centralBody.transform.position); // 첫 번째 꼭지점 위치
            lineRenderer.SetPosition(1, transform.parent.transform.position); // 두 번째 꼭지점 위치
        }

        
    }
    void sun()
    {
        masses.Clear();

        // 중력을 적용할 Rigidbody 객체들을 찾습니다
        bodies = new List<Rigidbody>(PlanetMgr.Instance.planets.Keys);

        // 질량이 0 이하인 객체를 제거
        bodies.RemoveAll(body => body.isKinematic);

        if (bodies.Count <= 0)
        {
            centralBody = rigidbody1;
            return;
        }

        // 가장 질량이 큰 객체를 중심 체로 설정
        float maxMass = -100000000f;
        for(int i=0;i< bodies.Count; i++)
        {
            Rigidbody rbToAttract = bodies[i];
            Rigidbody rb = transform.parent.GetComponent<Rigidbody>();
            
            
            // 객체와 목표까지의 방향
            Vector3 direction = rb.position - rbToAttract.position;
            float distance = direction.magnitude;
            arrangementCanvas.Instance.Load(baseplanet, distance);
            if (distance == 0f) continue; // 0으로 나누기 방지

            // 중력의 크기 계산
            
            
            float forceMagnitude = baseplanet.@base.G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
            
            if (forceMagnitude > maxMass)
            {
                masses.Add(bodies[i].mass);
                maxMass = forceMagnitude;
                centralBody = bodies[i];
            }
        }
            
        
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
