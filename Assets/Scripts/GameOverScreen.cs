using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Text fallsText;
    [SerializeField] private Text message;
    [SerializeField] private GameController gameController;

    public void SetUp(int fallsCount)
    {
        fallsText.text = fallsCount + " FALLS";
        if(fallsCount == 0)
        {
            message.text = "No falls, amazing!";
        }
        else
        {
            message.text = "Try again without any falls!";
        }

        Cursor.lockState = CursorLockMode.None;
        FindObjectOfType<ThirdPersonMovement>().SetActive(false);
        gameObject.SetActive(true);
    }

    public void PlayAgainButton()
    {
        gameController.Reset();
        SceneManager.LoadScene("LevelOne");
        gameObject.SetActive(false);
    }
}
