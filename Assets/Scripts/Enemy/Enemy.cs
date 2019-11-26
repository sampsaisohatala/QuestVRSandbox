using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health { private set; get; } = 10;
    public bool Alive { private set; get; } = true;

    [SerializeField] float m_MoveSpeed = 1f;

    Transform m_Target = null;

    void Awake()
    {
        SetupBodyParts();
    }

    void Start()
    {
        m_Target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_Target.position, m_MoveSpeed * Time.deltaTime);
    }

    void SetupBodyParts()
    {
        BodyPart[] bodyParts = GetComponentsInChildren<BodyPart>();

        foreach (BodyPart bodyPart in bodyParts)
            bodyPart.Setup(this);
    }

    public void TakeDamage(int damage)
    {
        if (!Alive)
            return;

        Health -= damage;

        if (Health <= 0)
            Kill();
    }

    public void Kill()
    {
        Alive = false;
        GameObject.Find("_GameManager").GetComponent<WaveSystem>().EnemyKilled();
        Destroy(gameObject);
    }

    /*
    [SerializeField] int m_MaxHealth = 2;
    [SerializeField] float m_MoveSpeed = 10f;

    GameObject mesh;

    int m_Healt;

    Transform m_Target = null;

    void Start()
    {
        Debug.Log("Enemy");
        mesh = transform.Find("Mesh").gameObject;
        m_Healt = m_MaxHealth;
        m_Target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        Move();
    }

    public void TakeDamage(Bodypart bodypart)
    {
        Debug.Log("TakeDamage");

        if (bodypart == Bodypart.Head)
            HeadShot();
        else if (bodypart == Bodypart.Body)
            BodyShot();
    }

    void HeadShot()
    {
        Debug.Log("Headhot");
        m_Healt -= 2;

        if(m_Healt < 1)
            Die();
    }

    void BodyShot()
    {
        Debug.Log("Bodyshot");
        m_Healt -= 1;

        if(m_Healt < 1)
            Die();
    }

    void Die()
    {
        // let waveSystem know that one enemies was killed
        GameObject.Find("_GameManager").GetComponent<WaveSystem>().EnemyKilled();
        Destroy(this);
    }

    void Respawn()
    {
        m_Healt = m_MaxHealth;
        mesh.SetActive(true);
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_Target.position, m_MoveSpeed * Time.deltaTime);
    }
    */
}
