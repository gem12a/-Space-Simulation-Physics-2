using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class timestop : MonoBehaviour
{
    public Sprite sprite;
    public Sprite sprite1;
    public Image image;
    public int i = 0;
    public TMP_InputField timeInputField;
    // Start is called before the first frame update
    public float time=1;
    void Start()
    {

        Time.timeScale = time;
    }

    // Update is called once per frame
    public void time1()
    {
        if (i == 0)
        {
            image.sprite = sprite;
            Time.timeScale = 0f;
            i = 1;
        }
        else
        {
            image.sprite = sprite1;
            Time.timeScale = time;
            i = 0;
        }
        
    }
    public void intime()
    {
        if (float.TryParse(timeInputField.text, out time))
        {
            Time.timeScale = time;
        }
        else
        {
            // 올바르지 않은 입력 처리 또는 오류 메시지 표시
            Debug.LogError("Invalid time input: " + timeInputField.text);
        }

    }
    
}
