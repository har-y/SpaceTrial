using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private static int _score = 0;

    [SerializeField] private Text _scoreText;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText = GameObject.FindGameObjectWithTag("score text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTextUI();
    }

    public void AddScorePoints(int points)
    {
        _score += points;
    }

    public void ResetScore()
    {
        _score = 0;
    }

    private void UpdateTextUI()
    {
        _scoreText.text = PadZero(_score, 3);
    }

    private string PadZero(int n, int padDigits)
    {
        string nString = n.ToString();

        while (nString.Length < padDigits)
        {
            nString = "0" + nString;
        }

        return nString;
    }
}
