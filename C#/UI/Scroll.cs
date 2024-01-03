using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Scroll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ����ĳ��Ʈ�� � ��ü�� �������� ���ϸ�, �� �� ���� Ŭ��
            if (!Physics.Raycast(ray, out hit))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    gameObject.SetActive(false);
                }

            }
        }
        
    }
    public void Load()
    {
        gameObject.SetActive(true);
    }
}
