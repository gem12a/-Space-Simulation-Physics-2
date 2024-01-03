using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class select : MonoBehaviour
{
    public bool select1 = false;
    public GameObject gameObject1;
    GameObject obj;
    public GameObject gameObject2;
    select []selects;
    // Start is called before the first frame update
    public void click()
    {
        selects = FindObjectsOfType<select>();
        foreach(select select in selects)
        {
            if (select.select1 == true) return;
        }
        select1 = true;
        obj = Instantiate(gameObject1);
       
        arrangementCanvas.Instance.gameObject.SetActive(true);
        arrangementCanvas.Instance.basePlanet = obj.GetComponent<baseplanet>();
        FindObjectOfType<cameraMgr>(true).SwitchCamera();
        obj.GetComponent<Rigidbody>().isKinematic = true;
        crash scriptComponent = obj.GetComponent<crash>();
        
        // ��ũ��Ʈ�� ã������ ���� ��Ȱ��ȭ�մϴ�.
        if (scriptComponent != null)
        {
            scriptComponent.enabled = false; // ��ũ��Ʈ ��Ȱ��ȭ
        }
        Gravity scriptComponent1 = obj.GetComponent<Gravity>();

        // ��ũ��Ʈ�� ã������ ���� ��Ȱ��ȭ�մϴ�.
        if (scriptComponent1 != null)
        {
            scriptComponent1.enabled = false; // ��ũ��Ʈ ��Ȱ��ȭ
        }
        SelectedOutline scriptComponent2 = obj.GetComponent<SelectedOutline>();

        // ��ũ��Ʈ�� ã������ ���� ��Ȱ��ȭ�մϴ�.
        if (scriptComponent2 != null)
        {
            scriptComponent2.enabled = false; // ��ũ��Ʈ ��Ȱ��ȭ
        }

        obj.transform.Find("line1").gameObject.SetActive(true);

        obj.transform.Find("line").gameObject.SetActive(false);
        
    }
    private void Update()
    {
        if (select1)
        {
            
            
            obj.transform.position = GetVector3();
            

            // ������Ʈ�� ��ġ�� ���콺�� ��ġ�� ������Ʈ�մϴ�.
            
            if (Input.GetMouseButtonDown(0)&&!EventSystem.current.IsPointerOverGameObject())
            {
                FindObjectOfType<cameraMgr>(true).SwitchCamera();
                
                // ���콺 Ŭ�� ��ġ�� ��ũ�� ��ǥ���� ���� ��ǥ�� ��ȯ

                crash scriptComponent = obj.GetComponent<crash>();

                // ��ũ��Ʈ�� ã������ ���� ��Ȱ��ȭ�մϴ�.
                if (scriptComponent != null)
                {
                    scriptComponent.enabled = true; // ��ũ��Ʈ ��Ȱ��ȭ
                }
                Gravity scriptComponent1 = obj.GetComponent<Gravity>();

                // ��ũ��Ʈ�� ã������ ���� ��Ȱ��ȭ�մϴ�.
                if (scriptComponent1 != null)
                {
                    scriptComponent1.enabled = true; // ��ũ��Ʈ ��Ȱ��ȭ
                }
                SelectedOutline scriptComponent2 = obj.GetComponent<SelectedOutline>();

                // ��ũ��Ʈ�� ã������ ���� ��Ȱ��ȭ�մϴ�.
                if (scriptComponent2 != null)
                {
                    scriptComponent2.enabled = true; // ��ũ��Ʈ ��Ȱ��ȭ
                }
                obj.GetComponent<Rigidbody>().isKinematic = false;
                obj.transform.Find("line").gameObject.SetActive(true);
                obj.transform.Find("line1").gameObject.SetActive(false);
                arrangementCanvas.Instance.gameObject.SetActive(false);
                select1 = false;
                PlanetMgr.Instance.planets.Add(obj.GetComponent<Rigidbody>(), obj.GetComponent<baseplanet>().@base);

            }


        }
    }
    
    Vector3 GetVector3()
    {
        
        Ray ray = cameraMgr.Instance.secondaryCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("Touch")))
        {
            
            return hit.point;
        }
        return Vector3.zero;

    }
}
