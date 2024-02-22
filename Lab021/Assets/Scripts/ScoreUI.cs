using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI _scoreField;
    private TextMeshProUGUI _levelField;
    private TextMeshProUGUI _timeField;

    private int _score = 0;
    private int _level ;
    private float[] _levelTimes = new float[3];

    private void Awake()
    {
        _scoreField = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        _levelField = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
        _timeField = GameObject.Find("Time").GetComponent<TextMeshProUGUI>();

    }
    private void Start()
    {
        _level = SceneManager.GetActiveScene().buildIndex + 1;
        _scoreField.text = _score.ToString();
        _levelField.text = "Level " +_level.ToString();
        if(GetBestTime(_level - 1)!=0)
            _timeField.text = "Best Time: " + GetBestTime(_level - 1).ToString("F2"); // Выводим лучшее время для текущего уровня


    }

    public void IncreaseScore()
    {
        _score += 1;
        _scoreField.text = _score.ToString();    
    }
    void Update()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        _levelTimes[currentLevelIndex] += Time.deltaTime; // Увеличиваем время прохождения текущего уровня

        switch (currentLevelIndex)
        {
            case 0:
                if (_score >=3)
                {
                    _score = 0;
                    SetNewTime(currentLevelIndex);
                    _levelTimes[0] = 0f; // Сбрасываем время прохождения первого уровня
                    SceneManager.LoadScene(currentLevelIndex + 1);
                }
                break;

            case 1:
                if (_score >= 5)
                {
                    _score = 0;
                    SetNewTime(currentLevelIndex);
                    _levelTimes[1] = 0f; // Сбрасываем время прохождения второго уровня
                    SceneManager.LoadScene(currentLevelIndex + 1);
                }
                break;

            case 2:
                if (_score >= 7)
                {
                    _score = 0;
                    SetNewTime(currentLevelIndex);
                    _levelTimes[2] = 0f; // Сбрасываем время прохождения третьего уровня
                    SceneManager.LoadScene(0);
                }
                break;
        }

        


        

    }
    void SetNewTime(int currentLevelIndex)
    {
        if (_levelTimes[currentLevelIndex] < GetBestTime(currentLevelIndex))
        {
            SetBestTime(currentLevelIndex, _levelTimes[currentLevelIndex]);
        }
        _levelTimes[currentLevelIndex] = 0f; // Сбрасываем время прохождения текущего уровня
    }
    float GetBestTime(int levelIndex)
    {
        return PlayerPrefs.GetFloat("BestTime" + levelIndex, Mathf.Infinity);
    }

    void SetBestTime(int levelIndex, float time)
    {
        PlayerPrefs.SetFloat("BestTime" + levelIndex, time);
    }
}
