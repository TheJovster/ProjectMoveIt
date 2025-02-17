using System;
using UnityEngine;

namespace WeaponSystem
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private SoundManager m_SoundManager;
        [SerializeField] private LevelManager m_LevelManager;

        [SerializeField] private Camera m_MainMenuCamera;

        public enum GameState
        {
            MainMenu,
            Loading,
            Game,
            Paused,
            GameOver
        }
        

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

            m_SoundManager = new SoundManager();
            m_LevelManager = new LevelManager();
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

        public void Transition()
        {
            HUDManager.Instance.SetFadeOut(true);
            HUDManager.Instance.FadeOut();
        }
    }

    [Serializable]
    public class SoundManager
    {
        //plan out sound management
        [SerializeField] private AudioSource m_AudioSource;

        #region Properties
        public AudioSource AudioSource => m_AudioSource;

        #endregion
    }

    [Serializable]
    public class LevelManager
    {
        [SerializeField] private GameObject[] m_LevelPrefabs;
        
        
        #region Properties

        public GameObject[] LevelPrefabs => m_LevelPrefabs;

        #endregion
    }
}

