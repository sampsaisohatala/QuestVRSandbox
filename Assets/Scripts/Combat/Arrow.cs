using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float m_Speed = 2000f;
    public Transform m_Tip = null;

    Rigidbody m_Rigidbody = null;
    bool m_IsStoppped = true;
    Vector3 m_LastPositon = Vector3.zero;
    GameObject m_Trail = null;

    [SerializeField] int damage = 5;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        transform.localPosition = new Vector3(0, 0, 0.425f);
        transform.localEulerAngles = Vector3.zero;
        m_Trail = transform.Find("Trail").gameObject;
        m_Trail.SetActive(false);
    }

    void Start()
    {
        //m_LastPositon = transform.position;
    }

    void FixedUpdate()
    {
        if (m_IsStoppped)        
            return;

        if (m_Rigidbody.velocity != Vector3.zero)
             m_Rigidbody.MoveRotation(Quaternion.LookRotation(m_Rigidbody.velocity, transform.up));

        RaycastHit hit;
        if (Physics.Linecast(m_LastPositon, m_Tip.position, out hit))
            Stop(hit.collider.gameObject);

        m_LastPositon = m_Tip.position;
    }

    void Stop(GameObject hitObject)
    {
        Debug.Log("Stop with: " + hitObject.name);
        m_IsStoppped = true;
        m_Trail.SetActive(false);

        transform.parent = hitObject.transform;

        m_Rigidbody.isKinematic = true;
        m_Rigidbody.useGravity = false;

        CheckForDamage(hitObject);
    }

    void CheckForDamage(GameObject hitObject)
    {
        MonoBehaviour[] behaviours = hitObject.GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour behaviour in behaviours)
        {
            if(behaviour is IDamageable)
            {
                IDamageable damageable = (IDamageable)behaviour;
                damageable.Damage(damage);
                break;
            }
        }
    }

    public void Fire(float pullValue)
    {
        m_LastPositon = m_Tip.position;
        m_IsStoppped = false;
        transform.parent = null;
        m_Rigidbody.isKinematic = false;
        m_Rigidbody.useGravity = true;
        m_Rigidbody.AddForce(transform.forward * (pullValue * m_Speed));
        m_Trail.SetActive(true);
        Destroy(gameObject, 5f);
    }
}
