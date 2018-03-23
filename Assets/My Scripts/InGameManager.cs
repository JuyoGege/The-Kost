using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class InGameManager : MonoBehaviour {
    public Button[] buttons;
    public Text gameendText;
    
        public void gameOver()
    {
        gameendText.text = "Game Over";
        Time.timeScale = 0;
        foreach(Button button in buttons)
        {
            button.gameObject.SetActive(true);
        }
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("menu");
    }
    public void exitGame()
    {
        Application.Quit();
    }

}

