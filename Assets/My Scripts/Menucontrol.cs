using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menucontrol : MonoBehaviour
{
    //control menu, untuk button ada ngeload scene
public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    //untuk button exit game(keluar dari game)
    public void QuitGame(string quitGame)
    {
    Debug.Log("Game Exited");
        Application.Quit();

    }
 }
  

