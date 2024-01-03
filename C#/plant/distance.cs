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

        lineRenderer.positionCount = 2; // �� ���� �������� ���� ��
        
        sun();



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distanceToCamera = GetDistanceToCamera();
        float desiredLineWidth = Mathf.Clamp(distanceToCamera * 0.1f, 0.1f, 10f); // ���ϴ� ������ �ʺ� ����
        lineRenderer.startWidth = desiredLineWidth;
        lineRenderer.endWidth = desiredLineWidth;
        sun();

        if (bodies.Count>=1) // centralBody�� null�� �ƴ� ��쿡�� ����
        {
            
            lineRenderer.SetPosition(0, centralBody.transform.position); // ù ��° ������ ��ġ
            lineRenderer.SetPosition(1, transform.parent.transform.position); // �� ��° ������ ��ġ
        }

        
    }
    void sun()
    {
        masses.Clear();

        // �߷��� ������ Rigidbody ��ü���� ã���ϴ�
        bodies = new List<Rigidbody>(PlanetMgr.Instance.planets.Keys);

        // ������ 0 ������ ��ü�� ����
        bodies.RemoveAll(body => body.isKinematic);

        if (bodies.Count <= 0)
        {
            centralBody = rigidbody1;
            return;
        }

        // ���� ������ ū ��ü�� �߽� ü�� ����
        float maxMass = -100000000f;
        for(int i=0;i< bodies.Count; i++)
        {
            Rigidbody rbToAttract = bodies[i];
            Rigidbody rb = transform.parent.GetComponent<Rigidbody>();
            
            
            // ��ü�� ��ǥ������ ����
            Vector3 direction = rb.position - rbToAttract.position;
            float distance = direction.magnitude;
            arrangementCanvas.Instance.Load(baseplanet, distance);
            if (distance == 0f) continue; // 0���� ������ ����

            // �߷��� ũ�� ���
            
            
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
        return float.MaxValue; // ī�޶� ���� ��� �ſ� ū ���� ��ȯ
    }
}
