using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseplanet : MonoBehaviour
{
    public base1 @base;
    // Start is called before the first frame update
   
}
[System.Serializable]
public class base1{
    public float G = 6.674f;
    public string name;
    public float mass;
    public float rotatingspeed;
    public float speed;
    public float axisof;
    public float initialSpeed;
    public float density;
    public double temperature;
    public GameObject objcollisionplanet;
    public bool revolution;

    public float conversion = 5223.055885304283f;
    public float CalculateSchwarzschildRadius()
    {
        float c = 299792458; // 빛의 속도 (미터/초)
        return (2 * G * mass) / (c * c);
    }
}
