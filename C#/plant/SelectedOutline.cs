using UnityEngine;
using UnityEngine.EventSystems;
public class SelectedOutline : MonoBehaviour
{
    public Material outlineMaterial; // 윤곽선 머티리얼
    private Renderer renderer;
    private static GameObject currentSelected; // 현재 선택된 GameObject

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
        // 마우스 왼쪽 버튼이 눌렸을 때
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 레이캐스트가 어떤 객체도 감지하지 못하면, 즉 빈 공간 클릭
            if (!Physics.Raycast(ray, out hit))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    if (currentSelected != null)
                    {
                        // 현재 선택된 객체의 윤곽선 제거
                        currentSelected.GetComponent<SelectedOutline>().RemoveOutline();

                        
                        currentSelected = null;
                    }
                }
                
            }
            else
            {
                // 클릭된 객체가 현재 객체일 경우
                if (hit.transform == transform)
                {


                    // 윤곽선 적용 및 현재 선택된 객체 갱신
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
        // 윤곽선 머티리얼 추가
        renderer.materials = new Material[] { materials[0], outlineMaterial };
        bl = true;
    }

    public void RemoveOutline()
    {
        Material[] materials = renderer.materials;
        // 원래 머티리얼로 복구
        renderer.materials = new Material[] { materials[0] };
        bl = false;
    }
}
