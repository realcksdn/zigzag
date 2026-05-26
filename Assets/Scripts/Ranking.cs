using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Ranking : MonoBehaviour
{
    public List<RankData> rankData = new();
    public List<Transform> rankUIs = new();

    public InputField nameInput;
    public CanvasGroup inputGroup;

    private void Start()
    {
        Load();
        UpdateUI();
    }

    public void Save()
    {
        Sort();

        for (int i = 0; i < rankData.Count; i++)
        {
            PlayerPrefs.SetString($"RankingName{i}", rankData[i].name);
            PlayerPrefs.SetInt($"RankingTime{i}", rankData[i].time);
        }
    }

    public void Load()
    {
        rankData.Clear();

        for (int i = 0; i < 5; i++)
        {
            string name = PlayerPrefs.GetString($"RankingName{i}", "");
            int time = PlayerPrefs.GetInt($"RankingTime{i}", -1); // 기본값 -1로 해서 실제 등록 안된 경우 구분

            if (time >= 0)
            {
                rankData.Add(new RankData(name, time));
            }
        }
    }

    public void Sort()
    {
        rankData = rankData.OrderBy(a => a.time).ToList();
        rankData = rankData.GetRange(0, Mathf.Min(rankData.Count, 5));
    }

    public void Register(int score)
    {
        string name = nameInput.text.Trim();

        if (string.IsNullOrEmpty(name))
        {
            Debug.LogWarning("이름을 입력해주세요.");
            return;
        }

        rankData.Add(new RankData(name, score));
        Sort();
        Save();
        UpdateUI();

        inputGroup.interactable = false;
        nameInput.text = "";
    }

    public void UpdateUI()
    {
        for (int i = 0; i < 3; i++)
        {
            Text rank = rankUIs[i].GetChild(0).GetComponent<Text>(); // 순위
            Text name = rankUIs[i].GetChild(1).GetComponent<Text>(); // 이름
            Text score = rankUIs[i].GetChild(2).GetComponent<Text>(); // 점수 (오른쪽으로 이동)

            rank.text = $"{i + 1}";

            if (i < rankData.Count)
            {
                name.text = string.IsNullOrEmpty(rankData[i].name) ? "---" : rankData[i].name;
                score.text = $"{rankData[i].time}";
            }
            else
            {
                name.text = "---";
                score.text = "-";
            }
        }
    }
}

[System.Serializable]
public class RankData
{
    public string name;
    public int time;

    public RankData(string name, int time)
    {
        this.name = name;
        this.time = time;
    }
}