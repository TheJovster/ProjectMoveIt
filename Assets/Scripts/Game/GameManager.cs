using System;
using UnityEngine;

namespace WeaponSystem
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private Camera m_MainMenuCamera;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            
        }

        private void Start()
        {
            InitializeGame();
        }

        public void InitializeGame()
        {
            HUDManager.Instance.SetFadeIn(true);
            Debug.Log("Game Initialized");
        }
    }
}

