using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject RankPanel;

    [Header("GameStart UI")]
    [SerializeField]
    private FadeEffect[] fadeGameStart;
    [SerializeField]
    private GameObject panelGameStart;
    [SerializeField]
    private TextMeshProUGUI TextGameStartBestScore; // 5-4

    [Header("InGame UI")]
    [SerializeField]
    private TextMeshProUGUI textInGameScore; // 점수 출력

    [Header("GameStart UI")]
    [SerializeField]
    private GameObject panelGameOver;
    [SerializeField]
    private TextMeshProUGUI textGameOverScore;
    [SerializeField]
    private TextMeshProUGUI textGameOverBestScore;//5-4
    [SerializeField]
    private float timeStopTime;

    [Header("Fever Settings")]//피버타임 기능 추가
    [SerializeField] private FeverManager feverManager;
    [SerializeField] private Slider feverGaugeSlider; 

    private int currentScore = 0;

    [SerializeField] private Ranking ranking;
    public bool IsGameStart { private set; get; } = false;
    public bool IsGameOver { private set; get; } = false;

     

    private IEnumerator Start()
    {
        Time.timeScale = 1;

        int bestScore = PlayerPrefs.GetInt("BestScore");
        TextGameStartBestScore.text = bestScore.ToString();

        for (int i = 0; i < fadeGameStart.Length; ++i)
        {
            fadeGameStart[i].FadeIn();
        }
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameStart();

                IsGameStart = true;

                yield break;
            }
            yield return null;
        }
    }

    public void GameStart()
    {
        panelGameStart.SetActive(false);
        textInGameScore.gameObject.SetActive(true);

        feverGaugeSlider.gameObject.SetActive(true); // 피버 UI 활성화
    }

    public void IncreaseScore(int score = 1)
    {
        int finalScore = Mathf.RoundToInt(score * feverManager.scoreMultiplier); // 고친거임
        currentScore += finalScore;
        textInGameScore.text = currentScore.ToString();
    }

    public void GameOver()
    {
        IsGameOver = true;
        textGameOverScore.text = currentScore.ToString();
        textInGameScore.gameObject.SetActive(false);
        panelGameOver.SetActive(true);
        feverGaugeSlider.gameObject.SetActive(false);

        int bestScore = PlayerPrefs.GetInt("BestScore");
        if (currentScore > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
        }
        textGameOverBestScore.text = bestScore.ToString();

        //  랭킹 시스템 연동
        ranking.inputGroup.interactable = true;
        ranking.Register(currentScore); // 점수 전달

        StartCoroutine(nameof(SlowAndStopTime));
    }

    private IEnumerator SlowAndStopTime()
    {
        float current = 0;
        float percent = 0;

        Time.timeScale = 0.5f;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / timeStopTime;

            yield return null;
        }

        Time.timeScale = 0;
    }


    private void Update()
    {
        if (IsGameStart && !IsGameOver)
        {
            // 피버 게이지 UI 업데이트
            feverGaugeSlider.value = feverManager.currentGauge;
        }
    }

}