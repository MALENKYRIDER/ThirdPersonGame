using System;
using TMPro;
using UnityEngine;

namespace DanceBattle
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _score;
        [SerializeField] private GameCore _gameCore;

        private void OnEnable()
        {
            _gameCore.ScoreChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            _gameCore.ScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(int value)
        {
            _score.text = value.ToString();
        }
    }
}