using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackhold : MonoBehaviour
{
    // 물리 상수 정의
    const float G = 6.67430e-11f; // 중력 상수 (m^3 kg^-1 s^-2)
    const float c = 299792458f; // 빛의 속도 (m/s)

    // Start is called before the first frame update
    baseplanet baseplanet;
    void Start()
    {
        baseplanet=gameObject.GetComponent<baseplanet>();
        // 예제 블랙홀 질량: 태양질량의 10배
        float blackHoleMass = baseplanet.@base.mass;
        CalculateAverageDensity();
        gameObject.GetComponent<Gravity>().CalculateVolumeAndRadius();
        // 평균 밀도 계산


    }
    private void FixedUpdate()
    {
        CalculateAverageDensity();
        
    }

    // 슈바르츠실트 반지름을 계산하는 함수
    float SchwarzschildRadius(float mass)
    {
        return 2 * G * mass * 5.972e23f / (c * c);
    }

    // 블랙홀의 부피를 계산하는 함수
    float BlackHoleVolume(float radius)
    {
        return 4f / 3f * Mathf.PI * Mathf.Pow(radius, 3);
    }

    // 블랙홀의 평균 밀도를 계산하는 함수
    void CalculateAverageDensity() {


        float mass = this.baseplanet.@base.mass;
        float radius = SchwarzschildRadius(mass ) / 1000;
        Debug.Log("   "+radius);
        float volume = BlackHoleVolume(radius );
        Debug.Log(volume);
        baseplanet.@base.density= (mass / volume);
    }
}
