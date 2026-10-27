using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] GameObject gameScreen;
    [SerializeField] GameObject upgradeScreen;

    [SerializeField] Image healthBar;
    [SerializeField] Image shootCooldown;
    [SerializeField] Image dashCooldown;

    [SerializeField] GameObject WinScreen;
    [SerializeField] GameObject LoseScreen;

    void Awake()
    {
        if (Instance != null)
        {
            if (Instance != this) Destroy(Instance);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ResetUI()
    {
        
    }

    public void UpdateHealth(float fillAmount)
    {
        healthBar.fillAmount = fillAmount;
    }

    public void UpdateShootCD(float fillAmount)
    {
        shootCooldown.fillAmount = fillAmount;
    }

    public void UpdateDodgeCD(float fillAmount)
    {
        dashCooldown.fillAmount = fillAmount;
    }

    public void UpgradeScreen(bool active)
    {
        upgradeScreen.SetActive(active);
        gameScreen.SetActive(!active);
    }

    public void GameEnd(bool win)
    {
        if (win) WinScreen.SetActive(true);
        else LoseScreen.SetActive(false);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        Destroy(PlayerController.Instance.gameObject);
        Destroy(gameObject);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
