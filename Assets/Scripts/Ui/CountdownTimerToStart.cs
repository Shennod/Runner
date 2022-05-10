using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class CountdownTimerToStart : MonoBehaviour
{
    [SerializeField] private HelpScreen _helpScreen;
    [SerializeField] private int _countdownTime;
    [SerializeField] private TMP_Text _countdownText;

    private const string Go = "Go!!!";
    private const float WaitTimeSeconds = 1f;

    public event UnityAction Run;

    private void OnEnable()
    {
        _helpScreen.CountdownStart += OnRun;
    }

    private void OnDisable()
    {
        _helpScreen.CountdownStart -= OnRun;
    }

    private void OnRun()
    {
        Time.timeScale = 1;
        StartCoroutine(CountdownToStart());
    }

    private IEnumerator CountdownToStart()
    {
        _countdownText.gameObject.SetActive(true);

        while (_countdownTime > 0)
        {
            _countdownText.text = _countdownTime.ToString();

            yield return new WaitForSeconds(WaitTimeSeconds);

            _countdownTime--;
        }

        _countdownText.text = Go;

        yield return new WaitForSeconds(WaitTimeSeconds);

        Run?.Invoke();
        _countdownText.gameObject.SetActive(false);
    }
}