using UnityEngine;
using UnityEngine.EventSystems;
public class SelectedOutline : MonoBehaviour
{
    public Material outlineMaterial; // ������ ��Ƽ����
    private Renderer renderer;
    private static GameObject currentSelected; // ���� ���õ� GameObject

    bool bl=false; 
    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (bl)
        {
            if(Input.GetKey(KeyCode.LeftControl)&& Input.GetKey(KeyCode.X))
            {
                planetpanel.Instance.gameObject.SetActive(false);
                PlanetMgr.Instance.planets.Remove(gameObject.GetComponent<Rigidbody>());
                Destroy(gameObject);
            }
        }
        // ���콺 ���� ��ư�� ������ ��
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ����ĳ��Ʈ�� � ��ü�� �������� ���ϸ�, �� �� ���� Ŭ��
            if (!Physics.Raycast(ray, out hit))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    if (currentSelected != null)
                    {
                        // ���� ���õ� ��ü�� ������ ����
                        currentSelected.GetComponent<SelectedOutline>().RemoveOutline();

                        
                        currentSelected = null;
                    }
                }
                
            }
            else
            {
                // Ŭ���� ��ü�� ���� ��ü�� ���
                if (hit.transform == transform)
                {


                    // ������ ���� �� ���� ���õ� ��ü ����
                    ApplyOutline();
                    planetpanel.Instance.gameObject.SetActive(true);
                    planetpanel.Instance.Load(gameObject.GetComponent<baseplanet>());
                    
                    currentSelected = gameObject;
                }
                else
                {
                    
                    RemoveOutline();
                }
            }

        }
    }

    private void ApplyOutline()
    {
        Material[] materials = renderer.materials;
        // ������ ��Ƽ���� �߰�
        renderer.materials = new Material[] { materials[0], outlineMaterial };
        bl = true;
    }

    public void RemoveOutline()
    {
        Material[] materials = renderer.materials;
        // ���� ��Ƽ����� ����
        renderer.materials = new Material[] { materials[0] };
        bl = false;
    }
}
