using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class arrangementCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text text;
    public TMP_InputField _InputField;
    public GameObject revolutionButton;

    public static arrangementCanvas Instance;
    public baseplanet basePlanet;
    private void Awake()
    {
        Instance = this;

    }
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void Load(baseplanet baseplanet, float d)
    {
       
        gameObject.SetActive(true);
        
        if (baseplanet.@base.revolution)
        {
            revolutionButton.GetComponent<Image>().color = Color.red;
        }
        else
        {
            revolutionButton.GetComponent<Image>().color = Color.white;
        }
        if (d * baseplanet.@base.conversion > 150000000)
        {
            text.text = $"distance:{(d * baseplanet.@base.conversion/ 150000000f).ToString("N5")}AU";
        }
        else
        {
            text.text = $"distance:{(d * baseplanet.@base.conversion).ToString("N0")}km";
        }
        
        float initialSpeedValue;
        if (float.TryParse(_InputField.text, out initialSpeedValue))
        {
            baseplanet.@base.initialSpeed = initialSpeedValue;
        }
    }
    public void revolutionButtonchage()
    {
        if (basePlanet.@base.revolution)
        {
            basePlanet.@base.revolution = false;
            revolutionButton.GetComponent<Image>().color = Color.white;
        }
        else
        {
            basePlanet.@base.revolution = true;
            revolutionButton.GetComponent<Image>().color = Color.red;
        }
    }

}
