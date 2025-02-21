using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text winText;
    public TMP_Text loseText;
    private bool gameWin = false;
    public static GameManager instance;
    private int lives = 3;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void DecreaseLives()
    {
        lives--;
    }

    public int GetLives()
    {
        return lives;
    }
    
    public void Reset() //for when player presses play again
    {
        lives = 3;
        gameWin = false;
    }

    public bool Win()
    {
        gameWin = true;
        return gameWin;
    }

    public void WinLose()
    {
        SceneManager.LoadScene(1);
        Invoke(nameof(LoadSceneText), 0.025f); //needed to wait otherwise the code tried to go before the scene was fully activated
    }

    private void LoadSceneText()
    {
        TMP_Text winText = GameObject.Find("WinText").GetComponent<TMP_Text>();
        TMP_Text loseText = GameObject.Find("LoseText").GetComponent<TMP_Text>();
        if (gameWin)
        {
            winText.gameObject.SetActive(true);
            loseText.gameObject.SetActive(false);
        }
        else
        {
            winText.gameObject.SetActive(false);
            loseText.gameObject.SetActive(true);
        }
    }
    
}