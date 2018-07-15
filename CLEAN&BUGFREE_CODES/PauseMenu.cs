using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{


    public string mainMenu;
    public bool isPaused;

    public GameObject pauseMenuCanvas;

     // Update is called once per frame
    void Update()
    {   //buat Pause Game, Jika Pause Maka Ngefreeze pake timeScale
        if(isPaused)
        { 
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0;
        } else
        {
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 0;

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
    }   //untuk ngeload Scene
        public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
  
  
}
