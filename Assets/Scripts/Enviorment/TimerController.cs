using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] EndGameController endGameController;
    private int minutero,segundero,second = 300;
    private bool IsTimeFrozen;

    public void Start() => StartCoroutine(Time());

    private void OnEnable() => gameController.OnFreezeTime += FrozeTime;
    private void OnDisable() => gameController.OnFreezeTime -= FrozeTime;


    public void FrozeTime() => IsTimeFrozen = true;

    public void EndTimer()
    {
        StopCoroutine(Time());
        second = 0;
    }
    private void Update()
    {
        minutero = second / 60;
        segundero = second % 60;
        
        string minutos, segundos;
        minutos = minutero.ToString();
        segundos = segundero.ToString();
        if (segundos.Length < 2 ) segundos = "0"+segundos;

        textMeshPro.text = $"{minutos}:{segundos}";

        if (minutero == 0 && segundero == 0) endGameController.Lose();
    }
    private IEnumerator Time()
    {
        yield return null;

        while (second > 0)
        {
            if (IsTimeFrozen)
            {
                yield return new WaitForSeconds(15);
                IsTimeFrozen = false;
            }
            yield return new WaitForSeconds(1);
            second--;
        }
    }
}
