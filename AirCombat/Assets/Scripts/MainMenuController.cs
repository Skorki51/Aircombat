using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Text yourScoreTextBox;
    [SerializeField] private Text highscoreTextBox;
    public void Start()
    {
        if(PlayerPrefs.GetInt("PlayerPointsSave") > PlayerPrefs.GetInt("PlayerHighScore", 0))
        {
            PlayerPrefs.SetInt("PlayerHighScore", PlayerPrefs.GetInt("PlayerPointsSave"));
            highscoreTextBox.text = PlayerPrefs.GetInt("PlayerPointsSave").ToString();
        }
        else
        {
            highscoreTextBox.text = PlayerPrefs.GetInt("PlayerHighScore").ToString();
        }
        
        if(yourScoreTextBox != null)
        {
            yourScoreTextBox.text = PlayerPrefs.GetInt("PlayerPointsSave").ToString();

        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ResetHighscore()
    {
        PlayerPrefs.DeleteAll();
        highscoreTextBox.text = PlayerPrefs.GetInt("PlayerHighScore").ToString();
    }
}
