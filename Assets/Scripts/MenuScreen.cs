using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    public void SetUp()
    {
        if (!gameObject.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            FindObjectOfType<ThirdPersonMovement>().SetActive(false);
            gameObject.SetActive(true);
        }
    }

    public void RestartButton()
    {
        gameController.Reset();
        SceneManager.LoadScene("LevelOne");
        gameObject.SetActive(false);
    }

    public void ResumeButton()
    {  
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        FindObjectOfType<ThirdPersonMovement>().SetActive(true);
    }
}
