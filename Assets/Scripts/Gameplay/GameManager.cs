using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] List<LevelDataSO> _levels = new List<LevelDataSO>();
    int _curLevel = 0;

    void Awake()
    {
        if (Instance != null)
        {
            if (Instance != this) Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    void Start()
    {
        
    }

    public void StartGame()
    {
        _curLevel = 0;
        LoadLevel();
    }

    public void LoadLevel()
    {
        if (_curLevel >= _levels.Count)
        {
            //End game
            return;
        }

        enemyManager.Instance.LoadLevel(_levels[_curLevel]._enemySpawns);
        _curLevel++;
    }
}
