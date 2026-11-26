using NUnit.Framework;
using System;
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

    public static event Action ReturnToMainMenu;

    void Awake()
    {
        if (Instance != null)
        {
            if (Instance != this) Destroy(Instance);
        }
        Instance = this;
        PlayerController.UpdateHealth += UpdateHealth;
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        PlayerController.UpdateHealth += UpdateHealth;
    }

    private void OnDisable()
    {
        PlayerController.UpdateHealth -= UpdateHealth;
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
        else LoseScreen.SetActive(true);
    }
    public void MainMenu()
    {
        Destroy(gameObject);
        ReturnToMainMenu?.Invoke();
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
