using System.Collections;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [Header("Assets")]
    public GameObject m_ArrowPrefab = null;

    [Header("Bow")]
    public float m_GrabTreshold = .15f;

    public Transform m_Start = null;
    public Transform m_End = null;
    public Transform m_Socket = null;

    Transform m_PullingHand = null;
    Arrow m_CurrentArrow = null;
    Animator m_Animator = null;

    float m_PullValue = 0.0f;
    Rigidbody m_Rigidbody = null;

    float arrowCreationDelay = 0.25f;

    //public Transform leftHand;
    //public OVRInput.Controller m_Controller = OVRInput.Controller.None;

    OVRGrabbable m_OVRGrabbable = null;

    //bool inUse = false;

    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_OVRGrabbable = GetComponent<OVRGrabbable>();
        StartCoroutine(CreateArrow(1f));
    }

    void Update()
    {
        if (!m_OVRGrabbable.isGrabbed)
            return;

        if (!m_PullingHand || !m_CurrentArrow)
            return;

        //RotateBow();

        m_PullValue = CalculatePull(m_PullingHand);
        m_PullValue = Mathf.Clamp(m_PullValue, 0.0f, 1.0f);
        m_Animator.SetFloat("Blend", m_PullValue);
        
    }

    void RotateBow()
    {
        //Vector3 test = new Vector3(m_PullingHand.position.x, m_PullingHand.position.y, m_PullingHand.position.z);
        transform.LookAt(m_PullingHand.position, transform.parent.right);
        transform.rotation *= Quaternion.Euler(new Vector3(0, 180, 90));
        //Vector3 relativePos = -m_PullingHand.position - transform.position;

        // the second argument, upwards, defaults to Vector3.up
        //Quaternion rotation = Quaternion.LookRotation(relativePos/*, rotationHelper.up*/);
        //transform.rotation = rotation;

        /*
        Quaternion q = Quaternion.LookRotation(-m_PullingHand.position, rotationHelper.up);
        Vector3 rot = q.eulerAngles;
        transform.rotation = Quaternion.Euler(rot);
        */
    }

    float CalculatePull(Transform pullHand)
    {
        Vector3 direction = m_End.position - m_Start.position;
        float magnitude = direction.magnitude;
        direction.Normalize();
        Vector3 difference = pullHand.position - m_Start.position;

        return Vector3.Dot(difference, direction) / magnitude;
    }

    IEnumerator CreateArrow(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject arrowObject = Instantiate(m_ArrowPrefab, m_Socket);
        arrowObject.transform.localPosition = new Vector3(0, 0, 0.425f);
        arrowObject.transform.localEulerAngles = Vector3.zero;
        m_CurrentArrow = arrowObject.GetComponent<Arrow>();
    }

    void FireArrow()
    {
        m_CurrentArrow.Fire(m_PullValue);
        m_CurrentArrow = null;
        
    }

    public void Pull(Transform hand)
    {
        float distance = Vector3.Distance(hand.position, m_Start.position);

        if (distance > m_GrabTreshold)
            return;

        m_PullingHand = hand;
    }

    public void Release()
    {
        if (m_PullValue > 0.25f)
            FireArrow();

        m_PullingHand = null;
        m_PullValue = 0.0f;
        m_Animator.SetFloat("Blend", 0);

        
        if (!m_CurrentArrow)
            StartCoroutine(CreateArrow(arrowCreationDelay));
    }
}
