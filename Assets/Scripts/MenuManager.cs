#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] public string mainMenu;
    [SerializeField] public string startGame;
    [SerializeField] public string credits;

    public void StartGame()
    {
        SceneManager.LoadScene(startGame);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
    public void Credits()
    {
        SceneManager.LoadScene(credits);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
