using UnityEngine;
using TMPro;

public class PluginTesting : MonoBehaviour
{
    public GameObject gameOverUI;
    public TMP_Text resultText;
    private bool finished;

     void Start()
    {
        MyPlugin.ResetRun();
        finished = false;
    }

    void Update()
    {
      if (finished)
        {
            return;
        }  

      if (gameOverUI != null && gameOverUI.activeInHierarchy)
        {
            MyPlugin.EndRun();
            finished = true;
            float time = MyPlugin.GetSurvivalTime();
            int rating = MyPlugin.GetSurvivalRating();
            string playerRating = rating == 2 ? "Gold" :
                                   rating == 1 ? "Silver" :
                                    "Bronze";

            int seconds = Mathf.FloorToInt(time);
            string printTime = (seconds / 60).ToString("00") + ":" + (seconds % 60).ToString("00");
            if (resultText != null)
            {
                resultText.text = "Survival Time: " + printTime + "\nRating: " + playerRating;
                return;

            }
            //MyPlugin.UpdateRun(Time.deltaTime);
        }
        MyPlugin.UpdateRun(Time.deltaTime);
    }
}
