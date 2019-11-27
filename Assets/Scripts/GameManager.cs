using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    WaveSystem m_WaveSystem;
    public WaveSystem WaveSystem
    {
        get
        {
            if (m_WaveSystem == null)
                m_WaveSystem = GetComponent<WaveSystem>();

            return m_WaveSystem;
        }
    }

    [SerializeField]
    Player m_Player; 
    public Player Player
    {
        get
        {
            return m_Player;
        }
    }

    [SerializeField]
    CarryMenu m_CarryMenu;
    public CarryMenu CarryMenu
    {
        get
        {
            return m_CarryMenu;
        }
    }
}
