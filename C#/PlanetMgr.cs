using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlanetMgr : MonoBehaviour
{
    public static PlanetMgr Instance;
    private void Awake()
    {
        Instance = this;

    }
    public Dictionary<Rigidbody, base1> planets = new Dictionary<Rigidbody, base1>();
    
    public List<PlanetEntry> planetList = new List<PlanetEntry>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        List<Rigidbody> keysToRemove = new List<Rigidbody>();
        foreach (var kvp in planets)
        {
            if (kvp.Key == null || kvp.Value == null)
            {
                keysToRemove.Add(kvp.Key);
            }
        }

        foreach (var key in keysToRemove)
        {
            planets.Remove(key);
        }

        // 리스트에서 null 엔트리 제거
        planetList.RemoveAll(item => item.key == null || item.value == null);
    }
}
[System.Serializable]
public class PlanetEntry
{
    public Rigidbody key; // 딕셔너리의 키인 Rigidbody 참조
    public base1 value; // 딕셔너리의 값인 base1 객체
}
