using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text ballsText;
    [SerializeField] private int expectedBalls = 5;
    private int ballsCount;

    [SerializeField] private Text fallsText;
    private static int fallsCount = 0;
    
    public static GameController instance = null;
    [SerializeField] private KeyCode menuKey;
    private bool gameOver = false;

    [SerializeField] private MenuScreen menuScreen;
    [SerializeField] private GameOverScreen gameOverScreen;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {   
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        ballsCount = 0;
        ballsText.text = "BALLS: " + ballsCount + "/" + expectedBalls;
        fallsText.text = "FALLS: " + fallsCount;
    }

    void Update()
    {
        if (Input.GetKeyDown(menuKey) && !gameOver)
        {
            menuScreen.SetUp();
        }
    }

    public void Reset()
    {
        ballsCount = 0;
        ballsText.text = "BALLS: " + ballsCount + "/" + expectedBalls;
        fallsCount = 0;
        fallsText.text = "FALLS: " + fallsCount;
        gameOver = false;
    }

    public void CollectBall()
    {
        ballsCount++;
        ballsText.text = "BALLS: " + ballsCount + "/" + expectedBalls;
        if(ballsCount >= expectedBalls)
        {
            if (SceneManager.GetActiveScene().name == "LevelOne")
            {
                NextLevel();
            }
            else
            {
                GameOver();
            }
        }
    }

    private void NextLevel()
    {
        ballsCount = 0;
        ballsText.text = "BALLS: " + ballsCount + "/" + expectedBalls;
        SceneManager.LoadScene("LevelTwo");
    }

    public void PlayerFall()
    {
        fallsCount++;
        fallsText.text = "FALLS: " + fallsCount;
        Invoke("PlayerReappear", 1f);
    }

    private void PlayerReappear()
    {
        FindObjectOfType<ThirdPersonMovement>().Reappear();
    }

    private void GameOver()
    {
        gameOver = true;
        gameOverScreen.SetUp(fallsCount);
        //fallCounts = 0;
    }
}
