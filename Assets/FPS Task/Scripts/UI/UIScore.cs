using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] IntEvent enemyDeath;

    float _score = 0;


    private void OnEnable()
    {
        enemyDeath.Subscribe(UpdateScore);
    }

    private void Start()
    {
        scoreText.text = _score.ToString();
    }

    private void OnDisable()
    {
        enemyDeath.Unsubscribe(UpdateScore);

    }

    public void UpdateScore(int value)
    {
        _score += value;
        scoreText.text = _score.ToString();
    }
}
