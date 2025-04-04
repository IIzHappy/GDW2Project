#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
    [SerializeField] public string mainMenu;
    [SerializeField] public string startGame;
    [SerializeField] public string credits;

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
    }

    public void StartGame()
    {
        StartCoroutine(Wait());
        SceneManager.LoadScene(startGame);
    }
    public void MainMenu()
    {
        StartCoroutine(Wait());
        SceneManager.LoadScene(mainMenu);
    }
    public void Credits()
    {
        StartCoroutine(Wait());
        SceneManager.LoadScene(credits);
    }
    public void QuitGame()
    {
        StartCoroutine(Wait());
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
