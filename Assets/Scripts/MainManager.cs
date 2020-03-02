using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField] Text _timerText;
    [SerializeField] Color _timerAlertColor;
    [SerializeField] float _startingTime;
    private float _timer;

    public static MainManager Instance;

    protected void Awake()
    {
        Instance = this;
    }

    protected IEnumerator Start()
    {
        for (var timer = _startingTime; timer >= 0; timer -= Time.deltaTime)
        {
            _timerText.text = timer.ToString("0.0");
            if (timer < _startingTime / 4)
            {
                _timerText.color = _timerAlertColor;
            }
            yield return null;
        }

        LoseGame();
    }

    public void WinGame()
    {
        Debug.Log("You Win!");
    }

    public void LoseGame()
    {
        Debug.Log("You Lose!");
    }
}
