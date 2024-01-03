using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class crash : MonoBehaviour
{
    Rigidbody rigidbody1;
    baseplanet rigidbody2;


    float time = 0;
    public float minSizeThreshold; // 파괴되기 전 최소 크기
    public float originalMass; // 최초 질량

    public double specificHeatCapacity = 4.18f;
    Vector3 point;

    public GameObject collisionParticles;
    public Dictionary<baseplanet, (Collision, bool)> crashedList = new Dictionary<baseplanet, (Collision, bool)>();
    public List<GameObject> crashedLists = new List<GameObject>();

    baseplanet baseplanet;
    private void Awake()
    {
        
        baseplanet = gameObject.GetComponent<baseplanet>();

    }
    void Start()
    {
        rigidbody1 = GetComponent<Rigidbody>();
        originalMass = baseplanet.@base.mass; // 초기 질량 저장
        gameObject.GetComponent<baseplanet>().@base.mass = baseplanet.@base.mass;
        minSizeThreshold = transform.localScale.x * 0.7f;
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        rigidbody2 = collision.gameObject.GetComponent<baseplanet>();
        
        if (!crashedList.ContainsKey(rigidbody2)&& rigidbody2.GetComponent<Rigidbody>().isKinematic==false) // Check if the key doesn't exist
        {
            
            crashedLists.Add(rigidbody2.gameObject);
            crashedList.Add(rigidbody2, (collision, false));
        }
        else
        {
            Debug.Log(baseplanet.@base.name+"Collision already in the list!"+ crashedList.Count);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        
        rigidbody2 = collision.gameObject.GetComponent<baseplanet>();
        if (crashedList.ContainsKey(rigidbody2)) // Check if the key doesn't exist
        {

            crashedLists.Remove(rigidbody2.gameObject);
            crashedList.Remove(rigidbody2);
        }









    }

    void FixedUpdate()
    {
        time += Time.deltaTime;
        List<baseplanet> toRemove = new List<baseplanet>();
        List<KeyValuePair<baseplanet, (Collision, bool)>> toAdd = new List<KeyValuePair<baseplanet, (Collision, bool)>>();
        // 충돌 리스트에 항목이 있고, 현재 오브젝트가 물리 시뮬레이션에 영향을 주지 않는 상태이며,
        // 기본 행성이 충돌 오브젝트를 가지고 있는 경우

        if (crashedList.Count <= 0&& gameObject.GetComponent<Collider>().isTrigger&& time>0.5f)
        {
            gameObject.GetComponent<Collider>().isTrigger = false;
        }

        if (crashedList.Count > 0)
        {
            // 충돌 리스트에서 무효화된 항목 제거
            nullremove();
            crashedLists.RemoveAll(body => body == null);




            foreach (var kvp in crashedList)
            {
                if (kvp.Value.Item1.collider != null)
                {
                    if ((kvp.Value.Item1.collider.isTrigger == false && gameObject.GetComponent<Collider>().isTrigger == false))
                    {
                        if (kvp.Key.@base.objcollisionplanet != null && baseplanet.@base.objcollisionplanet != null&&baseplanet.@base.mass / kvp.Key.@base.mass<1000)
                        {

                            baseplanet collisionObject = kvp.Key;

                            // 두 오브젝트 사이의 질량 비율을 무작위로 설정
                            float baseplanetchage = Random.Range(baseplanet.@base.mass / (baseplanet.@base.mass + collisionObject.@base.mass), 1f);
                            float collisionObjectcollisionObjectchage = Random.Range(collisionObject.@base.mass / (baseplanet.@base.mass + collisionObject.@base.mass), 1f);

                            float flo = baseplanet.@base.mass * baseplanetchage;
                            float countmass = 0;
                            int count = Random.Range(5,10);

                            baseplanet.@base.temperature += Mathf.Min(5000f,(1f / 2f) *  collisionObject.GetComponent<Rigidbody>().velocity.magnitude * collisionObject.GetComponent<Rigidbody>().velocity.magnitude) / (specificHeatCapacity);
                            
                            
                            for (int i=0;i<count;i++)
                            {
                                
                                // 충돌 지점을 계산하고 새로운 충돌 행성을 생성
                                if (OnCollision(kvp.Value.Item1) != Vector3.zero)
                                {
                                    point = OnCollision(kvp.Value.Item1);
                                }

                                GameObject collisionplanet = Instantiate(baseplanet.@base.objcollisionplanet);
                                collisionplanet.GetComponent<Rigidbody>().isKinematic = false;
                                PlanetMgr.Instance.planets.Add(collisionplanet.GetComponent<Rigidbody>(), collisionplanet.GetComponent<baseplanet>().@base);



                                float massLimit = (collisionObject.@base.mass / baseplanet.@base.mass)*10000;
                                collisionplanet.gameObject.transform.position = point;
                                float collisionplanetmass = Mathf.Min(massLimit,Random.Range(0.001f, (baseplanet.@base.mass - flo)/count));

                                collisionplanet.GetComponent<baseplanet>().@base.mass = collisionplanetmass;
                                float randomAngle = Random.Range(0f, 360f);
                                float randomAngle1 = Random.Range(180f, 360f);
                                Vector3 randomDirection = Quaternion.Euler(randomAngle, randomAngle1, 0f) * Vector3.forward;
                                collisionplanet.gameObject.transform.forward = randomDirection;
                                
                                collisionplanet.GetComponent<baseplanet>().@base.initialSpeed = Random.Range(baseplanet.@base.speed*2, baseplanet.@base.speed*3);
                                
                                collisionplanet.GetComponent<Rigidbody>().mass = collisionplanetmass;
                                collisionplanet.GetComponent<baseplanet>().@base.mass = collisionplanetmass;
                                countmass += collisionplanetmass;


                            }
                            if (collisionParticles != null)
                                Instantiate(collisionParticles, point, transform.rotation);

                            
                            // 기본 행성의 질량을 업데이트하고 중력을 다시 계산
                            baseplanet.@base.mass = baseplanet.@base.mass - countmass;
                           
                            
                            gameObject.GetComponent<Gravity>().CalculateVolumeAndRadius();

                            toRemove.Add(kvp.Key); // 제거할 항목 추가
                            toAdd.Add(new KeyValuePair<baseplanet, (Collision, bool)>(kvp.Key, (kvp.Value.Item1, true)));
                        }
                        else
                        {

                            baseplanet collisionObject = kvp.Key;

                            if (collisionObject.@base.mass < baseplanet.@base.mass && collisionObject.GetComponent<Collider>().isTrigger == false)
                            {
                                if (collisionObject != null && collisionObject.gameObject != null)
                                {
                                    PlanetMgr.Instance.planets.Remove(collisionObject.GetComponent<Rigidbody>());
                                    
                                    baseplanet.@base.mass += collisionObject.@base.mass;
                                    gameObject.GetComponent<Gravity>().CalculateVolumeAndRadius();
                                    Destroy(collisionObject.gameObject);

                                }
                            }
                            else
                            {
                                if (collisionObject != null && collisionObject.gameObject != null)
                                {
                                    PlanetMgr.Instance.planets.Remove(baseplanet.GetComponent<Rigidbody>());
                                    
                                    collisionObject.GetComponent<baseplanet>().@base.mass += baseplanet.@base.mass;
                                    collisionObject.GetComponent<Gravity>().CalculateVolumeAndRadius();
                                    Destroy(gameObject);

                                }
                            }
                            
                        }


                    }
                }
                
                    
                
                
                
            }

            
            foreach (var item in toAdd)
            {
                crashedList[item.Key] = item.Value;
            }
            // 충돌 파티클 생성
            

            // 크기가 일정 이하로 줄어들면 파괴
            if (baseplanet.@base.objcollisionplanet != null)
            {
                if (transform.localScale.x < minSizeThreshold || transform.localScale.y < minSizeThreshold || transform.localScale.z < minSizeThreshold)
                {
                    // 폭발 효과 생성

                    if (crashedLists.Count <= 0)
                    {

                    }
                    else
                    {
                        base1 maxKey = FindMaxMassRigidbody();
                        maxKey.mass += gameObject.GetComponent<Rigidbody>().mass;
                        PlanetMgr.Instance.planets.Remove(gameObject.GetComponent<Rigidbody>());

                        foreach (var kvp in crashedList)
                        {
                            kvp.Key.GetComponent<crash>().crashedList.Remove(kvp.Key);
                        }
                        Destroy(gameObject);
                    }
                    // 게임 오브젝트 파괴
                    
                }
            }
            
        }
    }

    Vector3 OnCollision(Collision col)
    {
        // 충돌 지점이 존재하는지 확인
        if (col.contacts.Length > 0)
        {
            ContactPoint contact = col.contacts[0];
            Vector3 pos = contact.point;
            return pos;
        }
        else
        {
            // 충돌 지점이 없을 경우 원하는 값 또는 예외 처리를 수행
            return Vector3.zero; // 또는 다른 적절한 처리
        }
    }

    base1 FindMaxMassRigidbody()
    {
        // PlanetMgr.Instance.planets가 접근 가능하고 데이터가 채워져 있다고 가정
        float mass = -10000;
        int k=0;
        for(int i=0;i< crashedLists.Count; i++)
        {
            GameObject planets = crashedLists[i];
            if(mass< planets.GetComponent<baseplanet>().@base.mass)
            {
                mass = planets.GetComponent<baseplanet>().@base.mass;
                k = i;
            }
        }





        return crashedLists[k].GetComponent<baseplanet>().@base;
        // 필요한 곳에서 maxKey 사용하기
    }
    void nullremove()
    {
        List<baseplanet> nullKeys = new List<baseplanet>();

        // crashedList 딕셔너리를 순회하며 null 키를 찾습니다.
        foreach (var pair in crashedList)
        {
            if (pair.Key == null) // 키 값이 null이면
            {
                nullKeys.Add(pair.Key); // null 키 리스트에 추가
            }
        }

        // null 키에 해당하는 항목을 crashedList에서 제거
        foreach (var key in nullKeys)
        {
            crashedList.Remove(key);
        }
    }
}
