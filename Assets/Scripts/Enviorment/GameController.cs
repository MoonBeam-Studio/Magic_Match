using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<Sprite> FrontCards = new List<Sprite>();

    public string FlippedCard1 = "", FlippedCard2 = "";


    public delegate void ReverseCard();
    public event ReverseCard OnReverseCard;
    public delegate void CardCorrect();
    public event CardCorrect OnCardCorrect;
    public delegate void FreezeTime();
    public event FreezeTime OnFreezeTime;
    public delegate void DeactivateCards();
    public event DeactivateCards OnDeactivateCards;
    public event DeactivateCards OnReactivateCards;


    public void FlipCard(RuneCard card)
    {
        if (FlippedCard1 != "") FlippedCard2 = card.ToString();
        else FlippedCard1 = card.ToString();

        if (FlippedCard1 != "" && FlippedCard2 != "" )
        {
            CheckCards();
        }
    }
    public void DeactivateReactivateCards(float time)
    {
        OnDeactivateCards?.Invoke();
        StartCoroutine(ReactivateInvoke(time));
    }

    public Sprite GetFrontCard(RuneCard runeCard, bool isColored)
    {
        Sprite sprite = null;
        switch (runeCard)
        {
            case RuneCard.Wind:
                if (isColored) sprite = FrontCards[5];
                else sprite = FrontCards[0];
                break;

            case RuneCard.Ice:
                if (isColored) sprite = FrontCards[6];
                else sprite = FrontCards[1];
                break;

            case RuneCard.Fire:
                if (isColored) sprite = FrontCards[7];
                else sprite = FrontCards[2];
                break;

            case RuneCard.Water:
                if (isColored) sprite = FrontCards[8];
                else sprite = FrontCards[3];
                break;

            case RuneCard.Death:
                if (isColored) sprite = FrontCards[9];
                else sprite = FrontCards[4];
                break;
        }
        return sprite;
    }

    public void FreezeTimeEvent() => OnFreezeTime?.Invoke();

    private void CheckCards()
    {
        OnDeactivateCards?.Invoke();
        if (FlippedCard1 == FlippedCard2)
        {
            OnCardCorrect?.Invoke();
            FlippedCard1 = "";
            FlippedCard2 = "";
            StartCoroutine(ReactivateInvoke());
        }
        else
        {
            StartCoroutine(ReverseCards());
            FlippedCard1 = "";
            FlippedCard2 = "";
            StartCoroutine(ReactivateInvoke());
        }
    }

    private IEnumerator ReactivateInvoke()
    {
        yield return new WaitForSeconds(1f);
        yield return null;
        OnReactivateCards?.Invoke();
    }

    private IEnumerator ReactivateInvoke(float time)
    {
        yield return new WaitForSeconds(time);
        yield return null;
        OnReactivateCards?.Invoke();
    }
    private IEnumerator ReverseCards()
    {
        yield return new WaitForSeconds(1);
        OnReverseCard?.Invoke();
    }
}
