using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField] Text _timerText;
    [SerializeField] Color _timerAlertColor;
    [SerializeField] float _startingTime;
    private float _timer;

    [SerializeField] GameObject _winScreen;
    [SerializeField] GameObject _loseScreen;
    [SerializeField] GameObject _retryPrompt;
    [SerializeField] float _retrySeconds;
    private bool _canRetry;
    [SerializeField] GameObject _loadingScreen;

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

    protected void Update()
    {
        if (_canRetry && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LoadingCoroutine());
        }
    }

    public void WinGame()
    {
        Debug.Log("You Win!");
        StartCoroutine(EndCoroutine(_winScreen));
    }

    public void LoseGame()
    {
        Debug.Log("You Lose!");
        StartCoroutine(EndCoroutine(_loseScreen));
    }

    private IEnumerator EndCoroutine(GameObject screen)
    {
        screen.SetActive(true);
        yield return new WaitForSeconds(_retrySeconds);
        _retryPrompt.SetActive(true);
        _canRetry = true;
    }

    private IEnumerator LoadingCoroutine()
    {
        _loadingScreen.SetActive(true);
        yield return new WaitForSeconds(_retrySeconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
