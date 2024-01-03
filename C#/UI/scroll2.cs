using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.EventSystems;
public class scroll2 : MonoBehaviour
{
    // Declare a List of Rigidbody objects
    List<Rigidbody> bodies;
    List<GameObject> bodies1;
    public GameObject gameObject1;
    public Transform parent;
    void Start()
    {
        gameObject.SetActive(false);
        bodies = new List<Rigidbody>(); // Initialize the List
        bodies1 = new List<GameObject>();

    }

    public void Load()
    {
        gameObject.SetActive(true);

       
        // Clear the List
        bodies.Clear();
        for (int i = 0; i < bodies1.Count; i++)
        {
            Destroy(bodies1[i]);
        }
        // Clear the List
        bodies1.Clear();
        // Find all Rigidbody components and add them to the List if they are not kinematic
        bodies.AddRange(FindObjectsOfType<Rigidbody>().Where(body => !body.isKinematic));
        for(int i = 0; i < bodies.Count; i++)
        {
            GameObject game= Instantiate(gameObject1, parent);
            bodies1.Add(game);
            game.GetComponentInChildren<TMP_Text>().text = bodies[i].transform.GetComponent<baseplanet>().@base.name;
            game.GetComponent<Button>().rigidbody1 = bodies[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 레이캐스트가 어떤 객체도 감지하지 못하면, 즉 빈 공간 클릭
            if (!Physics.Raycast(ray, out hit))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    gameObject.SetActive(false);
                }

            }
        }
    }
}
