using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Temperature : MonoBehaviour
{
    double pi = Mathf.PI;
    private double stefanBoltzmannConstant = 0.0000000567f;  // 스테판-볼츠만 상수
    baseplanet basePlanet;
    public double timeStep = 0.1f;  // 시간 단계
    public double specificHeatCapacity = 4.18f;  // 특정 열 용량
    Rigidbody rigidbody;
    public Renderer rend;

    public double tf=0.30;
    void Start()
    {
        rend = GetComponent<Renderer>();
        // baseplanet 및 rigidbody를 초기화합니다.
        basePlanet = GetComponent<baseplanet>();
        rigidbody = GetComponent<Rigidbody>();
        rend.material.EnableKeyword("_EMISSION");
    }

    void FixedUpdate()
    {
        // 핵융합 함수를 호출합니다.
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

        // 모든 행성에 대한 반복문을 수행합니다.
        foreach (Rigidbody data in PlanetMgr.Instance.planets.Keys)
        {
            if (basePlanet.@base.temperature >= 1000)
            {
                // 항성으로부터의 복사열 수신
                if (data == rigidbody)
                    continue;
                double starTemperature = basePlanet.@base.temperature; // 가정: 항성의 온도 (K)
                float starRadius = gameObject.transform.localPosition.x; // 가정: 항성의 반경 (km)
                double distanceToStar = Mathf.Max(10f,(data.transform.position- rigidbody.transform.position).magnitude); // 항성까지의 거리
                
                double receivedRadiation = stefanBoltzmannConstant * 4 * pi * Mathf.Pow(starRadius, 2) * Math.Pow(starTemperature, 3) / (4 * pi * Math.Pow(distanceToStar, 2));
                



                // 순열 전달 계산
                double netRadiation = receivedRadiation ; // 순열 전달: 받은 복사열 - 방출된 복사열
                double temperatureChange = netRadiation / (data.mass * specificHeatCapacity); // 온도 변화 계산


                // 온도 업데이트
                data.GetComponent<baseplanet>().@base.temperature += Mathf.Min(5000000000000f, (float)(temperatureChange * timeStep)); // 온도 업데이트

            }

        }
        double planetTemperature1 = basePlanet.@base.temperature; // 행성의 현재 온도
        
        SphereCollider collider1 = gameObject.GetComponent<SphereCollider>();
        double planetRadius1 = collider1.radius * gameObject.transform.localScale.x;
        
        double emittedRadiation1 =  stefanBoltzmannConstant * 4 * pi * Math.Pow(planetRadius1, 2) * Math.Pow(planetTemperature1, 3);
        
        // 시간 단위로 온도 감소 계산
        double temperatureDecrease = emittedRadiation1 / (basePlanet.@base.mass * specificHeatCapacity);
        // 온도 업데이트 (온도 감소)
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
        // 물체의 질량이 800 이상인 경우 핵융합을 발생시킵니다.
        if (basePlanet.@base.mass > 8000)
        {
            double density = basePlanet.@base.density;
            double massChange = (double)(basePlanet.@base.mass / Math.Pow(10, 15));
            double fusionEnergy = massChange * Mathf.Pow(3.0e8f, 2);
            double temperatureIncrease = fusionEnergy / (basePlanet.@base.mass * specificHeatCapacity * density);

             // 온도 증가량을 로그로 출력합니다.

            // 핵융합 에너지를 현재 온도에 추가합니다 (양수일 때만).
            if (temperatureIncrease > 0)
            {
                basePlanet.@base.temperature += temperatureIncrease* 0.3;
            }
        }
    }
}
