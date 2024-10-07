using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject LevelsBtns;
    [SerializeField] GameController gameController;

    public void PlayBtn()
    {
        LevelsBtns.SetActive(!LevelsBtns.activeSelf);
        gameController.GetComponent<EndGameController>().PlayUISFX();
    }
    public void HideLevels() => LevelsBtns.SetActive(false);

    public void Easy()
    {
        SceneManager.LoadScene("Easy");
        gameController.GetComponent<EndGameController>().PlayUISFX();
    }
    public void Medium()
    {
        SceneManager.LoadScene("Medium");
        gameController.GetComponent<EndGameController>().PlayUISFX();
    }
    public void Hard()
    {
        SceneManager.LoadScene("Hard");
        gameController.GetComponent<EndGameController>().PlayUISFX();
    }
}
