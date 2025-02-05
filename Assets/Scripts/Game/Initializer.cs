using System;
using Unity.Mathematics;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] private GameObject m_GameManager;
    [SerializeField] private GameObject m_HUDManager;
    [SerializeField] private GameObject m_SoundManager;
    [SerializeField] private GameObject m_LevelManager;
    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        GameObject gameinstance = Instantiate(m_GameManager, transform.position, quaternion.identity);
        GameObject hudInstance = Instantiate(m_HUDManager, transform.position, quaternion.identity);
        if (gameinstance && hudInstance)
        {
            Destroy(this.gameObject);
        }
    }
}
