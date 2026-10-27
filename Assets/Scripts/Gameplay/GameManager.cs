using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] List<LevelDataSO> _levels = new List<LevelDataSO>();
    int _curLevel = 0;
    int _curScene = 1;

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

    public void StartGame()
    {
        _curLevel = 0;
        _curScene = 1;
        LoadLevel();
        Time.timeScale = 1;
        enemyManager.Instance._spawning = true;
    }

    public void LoadLevel()
    {
        if (_curLevel >= _levels.Count)
        {
            GameWon();
            return;
        }

        //change scenes
        if (_levels[_curLevel]._scene != _curScene)
        {
            _curScene = _levels[_curLevel]._scene;
            SceneManager.LoadScene(_curScene);
            PlayerController.Instance.UpdateLevel(_curScene - 1);
        }

        enemyManager.Instance.LoadLevel(_levels[_curLevel]._enemySpawns);
        _curLevel++;
    }

    public void GameLoss()
    {
        Time.timeScale = 0;
        UIManager.Instance.GameEnd(false);
        enemyManager.Instance._spawning = false;
    }

    public void GameWon()
    {
        UIManager.Instance.GameEnd(false);
        Time.timeScale = 0;
        enemyManager.Instance._spawning = false;
    }
}