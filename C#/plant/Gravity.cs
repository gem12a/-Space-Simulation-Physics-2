using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    // Public Fields
    public Rigidbody centralBody;
    public List<Rigidbody> bodies = new List<Rigidbody>();

    // Private Serialized Fields
    [SerializeField] private baseplanet basePlanet; // Assign in Inspector

    // Private Fields
    private Rigidbody _rigidbody;
    private const float Pi = Mathf.PI; // Cache PI value

    private void Awake()
    {
        basePlanet = GetComponent<baseplanet>();
        _rigidbody = GetComponent<Rigidbody>();
        
        CalculateVolumeAndRadius();
        FindAndAssignCentralBody();
    }

    private void Start()
    {
        Vector3 initialVelocity;
        if (basePlanet.@base.revolution)
        {
           initialVelocity = CalculateInitialVelocity();
        }
        else
        {
            initialVelocity = norevolution();

        }
        
        _rigidbody.velocity = initialVelocity;
    }

    void FixedUpdate()
    {
        
        FindAndAssignCentralBody();
        basePlanet.@base.speed = _rigidbody.velocity.magnitude;



        // Apply gravity to all bodies
        if (!(_rigidbody.mass < 0.01f))
        {
            foreach (Rigidbody body in bodies)
            {
                if (body != null && body != _rigidbody && body.GetComponent<Collider>().isTrigger == false && gameObject.GetComponent<Collider>().isTrigger == false)
                {
                    if (!body.GetComponent<Collider>().isTrigger)
                        Attract(body);
                }
            }

        }

        ApplyOrbitRotation();
    }
    
    public void CalculateVolumeAndRadius()
    {
        _rigidbody.mass = basePlanet.@base.mass;

        // Calculate the volume and radius of the sphere
        float volume = (4f / 3f) * Pi * (basePlanet.@base.mass / basePlanet.@base.density);
        float radius = Mathf.Pow((3f * volume) / (4f * Pi), 1f / 3f);

        if (transform.localScale.x != radius)
            transform.localScale = new Vector3(radius, radius, radius);
    }

    void FindAndAssignCentralBody()
    {
        bodies.Clear();
        bodies.AddRange(PlanetMgr.Instance.planets.Keys);
        bodies.RemoveAll(body => body == null);
        bodies.RemoveAll(body => body.isKinematic);

        if (bodies.Count <= 0) return;

        Rigidbody heaviestBody = null;
        float maxMass = float.MinValue;

        foreach (Rigidbody body in bodies)
        {
            float distance = (body.position - _rigidbody.position).magnitude;

            if (distance > 0) // Ignore self or zero distance
            {
                float forceMagnitude = basePlanet.@base.G * (_rigidbody.mass * body.mass) / (distance * distance);
                if (forceMagnitude / (basePlanet.@base.mass) > 2000 && basePlanet.@base.objcollisionplanet != null)
                {
                    collapse(Mathf.Min(10,(int)(forceMagnitude / (basePlanet.@base.mass / 10))));
                }
                
                if (forceMagnitude > maxMass)
                {
                    maxMass = forceMagnitude;
                    heaviestBody = body;
                }
            }
        }

        centralBody = heaviestBody;
    }
    void collapse(int collapsemass)
    {
        float countmass = 0;
        for (int i = 0; i < collapsemass ; i++)
        {

            // 충돌 지점을 계산하고 새로운 충돌 행성을 생성
            
            
 

            GameObject collisionplanet = Instantiate(basePlanet.@base.objcollisionplanet);
            collisionplanet.GetComponent<Rigidbody>().isKinematic = false;
            PlanetMgr.Instance.planets.Add(collisionplanet.GetComponent<Rigidbody>(), collisionplanet.GetComponent<baseplanet>().@base);
            Debug.Log(PlanetMgr.Instance.planets.Count);


            float massLimit = (basePlanet.@base.mass);
            collisionplanet.gameObject.transform.position = gameObject.transform.position;
            float collisionplanetmass = Mathf.Min(massLimit, Random.Range(0.001f, basePlanet.@base.mass/collapsemass));
           
            gameObject.GetComponent<Collider>().isTrigger = false;
            
            collisionplanet.GetComponent<baseplanet>().@base.mass = collisionplanetmass;
            
            
            collisionplanet.gameObject.transform.forward = _rigidbody.velocity;

            collisionplanet.GetComponent<baseplanet>().@base.initialSpeed = basePlanet.@base.speed*Random.Range(0.5f,0.7f);

            collisionplanet.GetComponent<Rigidbody>().mass = collisionplanetmass;
            collisionplanet.GetComponent<baseplanet>().@base.mass = collisionplanetmass;
            countmass += collisionplanetmass;
            
            


        }
        basePlanet.@base.mass -= countmass;
        if (basePlanet.@base.mass <= 0)
        {
            Destroy(gameObject);
        }
        Instantiate(gameObject.GetComponent<crash>().collisionParticles, transform.position, transform.rotation);
    }
    void Attract(Rigidbody bodyToAttract)
    {
        Vector3 direction = _rigidbody.position - bodyToAttract.position;
        
        float distance = direction.magnitude;
        

        if (distance <= 0f) return;
        float safeDistance = Mathf.Max(distance, 1f); // 1f는 예시로 사용한 최소 거리입니다. 실제 사용할 때는 적절한 값으로 조정하세요.
        float forceMagnitude = basePlanet.@base.G * (_rigidbody.mass * bodyToAttract.mass) / (safeDistance * safeDistance);


        Vector3 force = direction.normalized * forceMagnitude;

        bodyToAttract.AddForce(force);
    }
    Vector3 norevolution()
    {
        return transform.forward * basePlanet.@base.initialSpeed;
    }
    Vector3 CalculateInitialVelocity()
    {
        if (centralBody == null) return Vector3.zero;

        Vector3 directionToCentral = (centralBody.position - transform.position).normalized;
        Vector3 arbitraryAxis = Vector3.up;

        if (Vector3.Angle(directionToCentral, arbitraryAxis) < 1f || Vector3.Angle(directionToCentral, arbitraryAxis) > 179f)
        {
            arbitraryAxis = Vector3.right;
        }

        Vector3 perpendicularDirection = Vector3.Cross(directionToCentral, arbitraryAxis).normalized;
        return perpendicularDirection * basePlanet.@base.initialSpeed;
    }

    void ApplyOrbitRotation()
    {
        Quaternion tilt = Quaternion.Euler(0, 0, basePlanet.@base.axisof);
        Vector3 axisOfRotation = tilt * Vector3.down;
        transform.Rotate(axisOfRotation, basePlanet.@base.rotatingspeed * Time.deltaTime);
    }
   
}


