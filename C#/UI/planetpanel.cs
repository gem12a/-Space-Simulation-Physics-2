using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class planetpanel : MonoBehaviour
{
    public TMP_InputField[] inputFields;
    base1 @base;
    public static planetpanel Instance;
    baseplanet planetbaseplanet;
    bool correction=false;
    private void Awake()
    {
        Instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (correction)
        {

        }
        else
        {
            Load(planetbaseplanet);
        }
        
    }
    public void Correction()
    {
        correction = true;
    }
    public void noCorrection()
    {
        correction = false;
    }
    public void Load(baseplanet baseplanet)
    {
        if (!gameObject.activeSelf) // Check if the GameObject is not active
        {
            correction = false;  // Set correction to false
            gameObject.SetActive(true); // Activate the GameObject
        }
        if (baseplanet == null)
        {
            gameObject.SetActive(false);
        }
        planetbaseplanet = baseplanet;
        base1 base1 = baseplanet.@base;
        @base = base1;
       
        inputFields[0].text = base1.name;
        inputFields[1].text = (base1.mass/10).ToString();
        inputFields[2].text = base1.rotatingspeed.ToString();
        inputFields[3].text = base1.axisof.ToString();
        inputFields[4].text = base1.speed.ToString();
        inputFields[5].text = base1.density.ToString();
        inputFields[6].text = base1.temperature.ToString();



    }
    public void remove()
    {
        planetpanel.Instance.gameObject.SetActive(false);
    }
    public void save()
    {
        crash crash = planetbaseplanet.GetComponent<crash>();
        Gravity gravity =planetbaseplanet.GetComponent<Gravity>();
        @base.name = inputFields[0].text;
        
        float massValue;
        if (float.TryParse(inputFields[1].text, out massValue))
        {
            @base.mass = massValue*10;
            crash.originalMass = massValue;
        }
        else
        {
            // 부동 소수점으로 변환할 수 없는 경우에 대한 처리
            Debug.LogError("Invalid input for mass: " + inputFields[1].text);
        }

        float rotatingSpeedValue;
        if (float.TryParse(inputFields[2].text, out rotatingSpeedValue))
        {
            @base.rotatingspeed = rotatingSpeedValue;
        }
        else
        {
            // 부동 소수점으로 변환할 수 없는 경우에 대한 처리
            Debug.LogError("Invalid input for rotating speed: " + inputFields[2].text);
        }

        float axisOfValue;
        if (float.TryParse(inputFields[3].text, out axisOfValue))
        {
            @base.axisof = axisOfValue;
        }
        else
        {
            // 부동 소수점으로 변환할 수 없는 경우에 대한 처리
            Debug.LogError("Invalid input for axis of: " + inputFields[3].text);
        }

        float initialSpeedValue;
        if (float.TryParse(inputFields[4].text, out initialSpeedValue))
        {
            planetbaseplanet.GetComponent<Rigidbody>().velocity = planetbaseplanet.gameObject.transform.forward* initialSpeedValue;
        }
        else
        {
            // 부동 소수점으로 변환할 수 없는 경우에 대한 처리
            Debug.LogError("Invalid input for initial speed: " + inputFields[4].text);
        }

        float densityValue;
        if (float.TryParse(inputFields[5].text, out densityValue))
        {
            @base.density = densityValue;
        }
        else
        {
            // 부동 소수점으로 변환할 수 없는 경우에 대한 처리
            Debug.LogError("Invalid input for density: " + inputFields[5].text);
        }
        if (float.TryParse(inputFields[6].text, out densityValue))
        {
            @base.temperature = densityValue;
        }
        else
        {
            // 부동 소수점으로 변환할 수 없는 경우에 대한 처리
            Debug.LogError("Invalid input for density: " + inputFields[5].text);
        }
        gravity.CalculateVolumeAndRadius();
        crash.minSizeThreshold = transform.localScale.x * 0.1f;
        correction = false;
    }


}
