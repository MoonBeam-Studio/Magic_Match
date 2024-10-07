using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public bool IsColored;
    public RuneCard runeCard;

    [SerializeField] Sprite frontCard, reverseCard;
    [SerializeField] GameController gameController;
    [SerializeField] EndGameController endGameController;
    [SerializeField] TextMeshProUGUI textMP;

    public Image image;

    // Start is called before the first frame update
    private void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(WaitShuffle());
        textMP.text = string.Empty;
    }


    private void OnEnable()
    {
        gameController.OnReverseCard += FlipReverseCard;
        gameController.OnDeactivateCards += Deacitvate;
        gameController.OnReactivateCards += Reacitvate;
    }

    private void OnDisable()
    {
        gameController.OnReverseCard -= FlipReverseCard;
        gameController.OnDeactivateCards -= Deacitvate;
        gameController.OnReactivateCards -= Reacitvate;
    }

    private void Update()
    {
        GetSprite();
    }

    public void GetSprite()
    {
        frontCard = gameController.GetFrontCard(runeCard, IsColored);
    }
    private void Deacitvate() { GetComponent<Button>().enabled = false; Debug.Log("Deactivate"); }
    private void Reacitvate() { GetComponent<Button>().enabled = true; Debug.Log("Reactivate"); }

    public void FlipCard()
    {
        gameController.GetComponent<EndGameController>().PlayCardFlipSFX();
        image.sprite = frontCard;
        GetComponent<Button>().enabled = false;
        gameController.OnCardCorrect += CardCorrect;
        gameController.FlipCard(runeCard);
    }

    public void CardCorrect()
    {
        gameController.OnReverseCard -= FlipReverseCard;
        GetComponent<Button>().enabled = false;
        gameController.gameObject.GetComponent<ShuffleCards>()._cardsRemaining.Remove(gameObject.GetComponent<CardController>());
        image.sprite = frontCard;
        if (IsColored)
        {
            switch (runeCard)
            {
                case RuneCard.Fire: FireRune(); break;
                case RuneCard.Wind: WindRune(); break;
                case RuneCard.Ice: IceRune(); break;
                case RuneCard.Water: WaterRune(); break;
                case RuneCard.Death: DeathRune(); break;
            }
        }

        endGameController.CompletePair();
        gameController.OnReactivateCards -= Reacitvate;
        gameController.OnCardCorrect -= CardCorrect;
    }

    public void FireRune() 
    {
        textMP.text = "Fire Rune activated!";
        StartCoroutine(ResetFeedback());

        gameController.GetComponent<EndGameController>().PlayCardFireSFX();

        gameController.DeactivateReactivateCards(.5f);

        List<CardController> cards = gameController.gameObject.GetComponent<ShuffleCards>().cardPairs;

        int card1 = Random.Range(0, cards.Count);
        int card2;
        if (card1 % 2 == 0) card2 = card1 +1;
        else card2 = card1 -1;

        cards[card1].CardCorrect();
        cards[card2].CardCorrect();

    }
    public void WindRune() 
    {
        textMP.text = "Wind Rune activated!";
        StartCoroutine(ResetFeedback());

        gameController.GetComponent<EndGameController>().PlayCardWindSFX();

        gameController.DeactivateReactivateCards(1.5f);
        StartCoroutine(WindRuneIE());

    }
    public void IceRune() 
    {
        textMP.text = "Ice Rune activated!";
        StartCoroutine(ResetFeedback());

        gameController.GetComponent<EndGameController>().PlayCardIceSFX();

        gameController.FreezeTimeEvent();
    }
    public void WaterRune() 
    {
        textMP.text = "Water Rune activated!";
        StartCoroutine(ResetFeedback());

        gameController.GetComponent<EndGameController>().PlayCardWaterSFX();

        gameController.DeactivateReactivateCards(.5f);
        StartCoroutine(gameController.GetComponent<ShuffleCards>().Shuffle());
    }
    public void DeathRune() 
    {
        textMP.text = "Death Rune activated!";
        StartCoroutine(ResetFeedback());

        gameController.gameObject.GetComponent<EndGameController>().Lose();
    }

    private void FlipReverseCard()
    {
        gameController.OnCardCorrect -= CardCorrect;
        GetComponent<Button>().enabled = true;
        image.sprite = reverseCard;
    }

    private IEnumerator WindRuneIE()
    {
        List<CardController> cards = gameController.GetComponent<ShuffleCards>()._cardsRemaining;

        int numberFlip = Random.Range(1, cards.Count);

        for (int i = 0; i < numberFlip; i++)
        {
            int n = Random.Range(0, cards.Count);
            cards[n].image.sprite = cards[n].frontCard;
            cards[n].GetComponent<Button>().enabled = false;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2f);
        foreach (var item in cards)
        {
            item.image.sprite = item.reverseCard;
            item.GetComponent<Button>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator ResetFeedback()
    {
        yield return new WaitForSeconds(1.5f);
        textMP.text = string.Empty;
    }

    IEnumerator WaitShuffle()
    {
        for (int i = 0; i < 3; i++) yield return null;
        GetSprite();
    }

}
