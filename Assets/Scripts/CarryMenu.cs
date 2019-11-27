using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarryMenu : MonoBehaviour
{
    TextMeshProUGUI waveCount;
    TextMeshProUGUI killCount;

    void Start()
    {
        waveCount = transform.Find("Canvas/Panel/WaveCount").GetComponent<TextMeshProUGUI>();
        killCount = transform.Find("Canvas/Panel/KillCount").GetComponent<TextMeshProUGUI>();
    }

    public void RefreshWaveCount(int waveNumber)
    {
        waveCount.text = waveNumber.ToString();
    }

    public void RefreshKillCount(int numberOfKills)
    {
        killCount.text = numberOfKills.ToString();
    }
}
