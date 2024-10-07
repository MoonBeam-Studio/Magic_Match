using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleCards : MonoBehaviour
{
    public List<CardController> _cards = new List<CardController>();
    public List<CardController> _cardsRemaining = new List<CardController>();
    public List<CardController> cardPairs = new List<CardController>();

    [SerializeField] private List<CardController> _cardsToShuffle = new List<CardController>();
    [SerializeField]private List<int> cardsAlreadyShuffle = new List<int>();
    //[SerializeField] private TimerController timerController;
    // Start is called before the first frame update

    private int LastPair;
    void Start()
    {
        foreach (var item in _cards)
        {
            
            _cardsRemaining.Add(item);
        }
        StartCoroutine(Shuffle());
    }

    private RuneCard SetRune()
    {
        int n = Random.Range(0, 5);
        while (n == LastPair) n = Random.Range(0, 5);

        LastPair = n;
        switch (n)
        {
            case 0:
                return RuneCard.Ice;
            case 1:
                return RuneCard.Wind;
            case 2:
                return RuneCard.Fire;
            case 3:
                return RuneCard.Death;
            case 4:
                return RuneCard.Water;

            default: return RuneCard.Ice;
        }

    }

    private bool SetColor()
    {
        int n = Random.Range(0, 4);

        if (n == 0) return true;
        else return false;
    }

    public IEnumerator Shuffle()
    {
        yield return null;
        _cardsToShuffle.Clear();
        cardsAlreadyShuffle.Clear();

        foreach (var item in _cardsRemaining)
        {
            _cardsToShuffle.Add(item);
        }
        yield return null;
        while (cardPairs.Count != _cardsToShuffle.Count)
        {
            int card1, card2;
            card1 = Random.Range(0, _cardsToShuffle.Count);
            card2 = Random.Range(0, _cardsToShuffle.Count);

            while (cardsAlreadyShuffle.IndexOf(card1) != -1) card1 = Random.Range(0, _cardsToShuffle.Count);
            while (cardsAlreadyShuffle.IndexOf(card2) != -1 || card2 == card1) card2 = Random.Range(0, _cardsToShuffle.Count);
            cardsAlreadyShuffle.Add(card1);
            cardsAlreadyShuffle.Add(card2);

            yield return null;
            RuneCard rune = SetRune();

            _cardsToShuffle[card1].runeCard = rune;
            _cardsToShuffle[card2].runeCard = rune;

            _cardsToShuffle[card1].IsColored = SetColor();
            _cardsToShuffle[card2].IsColored = SetColor();

            if (_cardsToShuffle[card1].IsColored && _cardsToShuffle[card1].runeCard == RuneCard.Water) _cardsToShuffle[card1].IsColored = false;
            if (_cardsToShuffle[card2].IsColored && _cardsToShuffle[card2].runeCard == RuneCard.Water) _cardsToShuffle[card2].IsColored = false;

            yield return null;
            cardPairs.Add(_cardsToShuffle[card1]);
            cardPairs.Add(_cardsToShuffle[card2]);

        }
    }
}
