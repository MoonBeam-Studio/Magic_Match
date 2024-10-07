using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameController : MonoBehaviour
{
    [SerializeField] int NumberOfCards, completedPairs;
    [SerializeField] GameObject WinObj, LoseObj;
    [SerializeField] AudioClip[] SFX;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        LoseObj.SetActive(false);
        WinObj.SetActive(false);
    }
    

    private void Update()
    {
        if (completedPairs >= NumberOfCards) Win();
        if (completedPairs % 2 != 0) completedPairs++;
    }

    public void CompletePair()
    {
        completedPairs++;
    }
    public void Win()
    {
        PlayCardWinSFX();
        WinObj.SetActive(true);
        LoseObj.SetActive(false);
    }

    public void Lose()
    {
        if (completedPairs == NumberOfCards)
        {
            Win();
            return;
        }
        PlayCardLoseSFX();
        List<Button> cardsBtn = new List<Button>();
        foreach (var item in GetComponent<ShuffleCards>()._cards)
        {
           item.GetComponent<Button>().enabled =false;
        }
        GameObject.Find("Timer").GetComponent<TimerController>().EndTimer();
        LoseObj.SetActive(true);
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayUISFX();
    }

    public void GoBack()
    {
        SceneManager.LoadScene("MainMenu");
        PlayUISFX();
    }

    public void PlayUISFX()
    {
        audioSource.clip = SFX[0];
        audioSource.Play();
    }

    public void PlayCardFlipSFX()
    {
        audioSource.clip = SFX[1];
        audioSource.Play();
    }

    public void PlayCardFireSFX()
    {
        audioSource.clip = SFX[2];
        audioSource.Play();
    }

    public void PlayCardWindSFX()
    {
        audioSource.clip = SFX[3];
        audioSource.Play();
    }

    public void PlayCardIceSFX()
    {
        audioSource.clip = SFX[4];
        audioSource.Play();
    }
    public void PlayCardWaterSFX()
    {
        audioSource.clip = SFX[5];
        audioSource.Play();
    }

    public void PlayCardLoseSFX()
    {
        audioSource.clip = SFX[6];
        audioSource.Play();
    }
    public void PlayCardWinSFX()
    {
        audioSource.clip = SFX[7];
        audioSource.Play();
    }
}
