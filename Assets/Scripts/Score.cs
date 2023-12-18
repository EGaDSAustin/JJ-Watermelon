using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public Transform player;
    public float scorePerUnit = 10f;
    public int score;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float playerY = player.position.y;

        score = Mathf.RoundToInt(Mathf.Clamp((0 - playerY) * scorePerUnit, 0, Mathf.Infinity));
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
