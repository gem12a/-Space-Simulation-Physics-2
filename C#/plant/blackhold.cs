using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackhold : MonoBehaviour
{
    // ���� ��� ����
    const float G = 6.67430e-11f; // �߷� ��� (m^3 kg^-1 s^-2)
    const float c = 299792458f; // ���� �ӵ� (m/s)

    // Start is called before the first frame update
    baseplanet baseplanet;
    void Start()
    {
        baseplanet=gameObject.GetComponent<baseplanet>();
        // ���� ��Ȧ ����: �¾������� 10��
        float blackHoleMass = baseplanet.@base.mass;
        CalculateAverageDensity();
        gameObject.GetComponent<Gravity>().CalculateVolumeAndRadius();
        // ��� �е� ���


    }
    private void FixedUpdate()
    {
        CalculateAverageDensity();
        
    }

    // ���ٸ�����Ʈ �������� ����ϴ� �Լ�
    float SchwarzschildRadius(float mass)
    {
        return 2 * G * mass * 5.972e23f / (c * c);
    }

    // ��Ȧ�� ���Ǹ� ����ϴ� �Լ�
    float BlackHoleVolume(float radius)
    {
        return 4f / 3f * Mathf.PI * Mathf.Pow(radius, 3);
    }

    // ��Ȧ�� ��� �е��� ����ϴ� �Լ�
    void CalculateAverageDensity() {


        float mass = this.baseplanet.@base.mass;
        float radius = SchwarzschildRadius(mass ) / 1000;
        Debug.Log("   "+radius);
        float volume = BlackHoleVolume(radius );
        Debug.Log(volume);
        baseplanet.@base.density= (mass / volume);
    }
}
