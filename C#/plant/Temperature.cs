using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Temperature : MonoBehaviour
{
    double pi = Mathf.PI;
    private double stefanBoltzmannConstant = 0.0000000567f;  // ������-������ ���
    baseplanet basePlanet;
    public double timeStep = 0.1f;  // �ð� �ܰ�
    public double specificHeatCapacity = 4.18f;  // Ư�� �� �뷮
    Rigidbody rigidbody;
    public Renderer rend;

    public double tf=0.30;
    void Start()
    {
        rend = GetComponent<Renderer>();
        // baseplanet �� rigidbody�� �ʱ�ȭ�մϴ�.
        basePlanet = GetComponent<baseplanet>();
        rigidbody = GetComponent<Rigidbody>();
        rend.material.EnableKeyword("_EMISSION");
    }

    void FixedUpdate()
    {
        // ������ �Լ��� ȣ���մϴ�.
        NuclearFusion();
        if (gameObject.tag == "black hold")
        {
            return;
            this.basePlanet.@base.temperature = 0;
        }
        if (rend != null && rend.material != null)
        {
            // Get the current material's emission color
            Color currentColor = rend.material.GetColor("_EmissionColor");

            // Calculate the new emission intensity based on the base planet temperature
            float emissionIntensity = Mathf.Min(10f, (float)basePlanet.@base.temperature / 500);
            
            // Calculate the new emission color with the desired intensity
            Color newEmissionColor = Color.white * emissionIntensity;

            // Set the new emission color to the material
            rend.material.SetColor("_EmissionColor", newEmissionColor);
        }

        // ��� �༺�� ���� �ݺ����� �����մϴ�.
        foreach (Rigidbody data in PlanetMgr.Instance.planets.Keys)
        {
            if (basePlanet.@base.temperature >= 1000)
            {
                // �׼����κ����� ���翭 ����
                if (data == rigidbody)
                    continue;
                double starTemperature = basePlanet.@base.temperature; // ����: �׼��� �µ� (K)
                float starRadius = gameObject.transform.localPosition.x; // ����: �׼��� �ݰ� (km)
                double distanceToStar = Mathf.Max(10f,(data.transform.position- rigidbody.transform.position).magnitude); // �׼������� �Ÿ�
                
                double receivedRadiation = stefanBoltzmannConstant * 4 * pi * Mathf.Pow(starRadius, 2) * Math.Pow(starTemperature, 3) / (4 * pi * Math.Pow(distanceToStar, 2));
                



                // ���� ���� ���
                double netRadiation = receivedRadiation ; // ���� ����: ���� ���翭 - ����� ���翭
                double temperatureChange = netRadiation / (data.mass * specificHeatCapacity); // �µ� ��ȭ ���


                // �µ� ������Ʈ
                data.GetComponent<baseplanet>().@base.temperature += Mathf.Min(5000000000000f, (float)(temperatureChange * timeStep)); // �µ� ������Ʈ

            }

        }
        double planetTemperature1 = basePlanet.@base.temperature; // �༺�� ���� �µ�
        
        SphereCollider collider1 = gameObject.GetComponent<SphereCollider>();
        double planetRadius1 = collider1.radius * gameObject.transform.localScale.x;
        
        double emittedRadiation1 =  stefanBoltzmannConstant * 4 * pi * Math.Pow(planetRadius1, 2) * Math.Pow(planetTemperature1, 3);
        
        // �ð� ������ �µ� ���� ���
        double temperatureDecrease = emittedRadiation1 / (basePlanet.@base.mass * specificHeatCapacity);
        // �µ� ������Ʈ (�µ� ����)
        if ((basePlanet.@base.temperature - temperatureDecrease * timeStep )< 0)
        {
            basePlanet.@base.temperature = 0;
        }
        else
        {
            basePlanet.@base.temperature -= temperatureDecrease * timeStep;
        }
        
        
    }

    void NuclearFusion()
    {
        if(gameObject.tag=="black hold")
        {
            return;
            this.basePlanet.@base.temperature = 0;
        }
        // ��ü�� ������ 800 �̻��� ��� �������� �߻���ŵ�ϴ�.
        if (basePlanet.@base.mass > 8000)
        {
            double density = basePlanet.@base.density;
            double massChange = (double)(basePlanet.@base.mass / Math.Pow(10, 15));
            double fusionEnergy = massChange * Mathf.Pow(3.0e8f, 2);
            double temperatureIncrease = fusionEnergy / (basePlanet.@base.mass * specificHeatCapacity * density);

             // �µ� �������� �α׷� ����մϴ�.

            // ������ �������� ���� �µ��� �߰��մϴ� (����� ����).
            if (temperatureIncrease > 0)
            {
                basePlanet.@base.temperature += temperatureIncrease* 0.3;
            }
        }
    }
}
