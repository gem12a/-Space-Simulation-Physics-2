using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rigidbody1;
    public void pos()
    {
        Camera.main.GetComponent<canera>().Approach(rigidbody1);
    }
    public void pospanal()
    {
        if (rigidbody1.GetComponent<baseplanet>() == null)
            return;
        FindObjectOfType<planetpanel>(true).Load(rigidbody1.GetComponent<baseplanet>());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
