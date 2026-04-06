using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DanceBattle
{
    public class GameCore : MonoBehaviour

    {
        const int ADD_SCORE_VALUE = 10;
        const int SPECIAL_SCORE_VALUE = 50;
        
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private InputService _inputService;
        [SerializeField] private ActionZone[] _zones;
        [SerializeField] private GameObject _particles;
        
        private int _score;
        private bool _isSpecialPlaying;

        public event Action<int> ScoreChanged;

        private int Score
        {
            get => _score;
            set
            {
                _score = value;
                ScoreChanged?.Invoke(_score);
            }
        }

        private void OnEnable()
        {
            _inputService.SpecialClick += OnSpecialClick;
        }

        private void OnDisable()
        {
            _inputService.SpecialClick -= OnSpecialClick;
        }

        private void Start()
        {
            Score = 0;
            _particles.SetActive(false);

            for (int i = 0; i < _zones.Length; i++)
            {
                int zoneIndex = i;
                _zones[i].Init(() => HandleFail(zoneIndex));
            }

            StartCoroutine(SpawnZones());
        }

        private void Update()
        {
            bool isPlaying = _animator.IsPlayingSpecial();

            if (isPlaying && !_isSpecialPlaying)
            {
                _isSpecialPlaying = true;
                EnableParticles();
            }

            // если спецтанец закончился
            if (!isPlaying && _isSpecialPlaying)
            {
                _isSpecialPlaying = false;
                DisableParticles();
            }
        }

        private void EnableParticles()
        {
            _particles?.SetActive(true);
        }

        private void DisableParticles()
        {
            _particles?.SetActive(false);
        }

        private IEnumerator SpawnZones()
        {
            while (true)
            {
                int randomIndex = Random.Range(0, _zones.Length);

                _zones[randomIndex].PlayStartAnimation();

                yield return new WaitForSeconds(_zones[randomIndex].GetDuration());

                yield return new WaitForSeconds(0.3f);
            }
        }

        private void HandleFail(int index)
        {
            _zones[index].PlayFailedAnimation();
            Debug.Log($"failed key id: {index}");
        }


        private void OnSpecialClick(int number)
        {
            int index = number - 1;

            if (IsSucsesfulClick(number))
            {
                _zones[index].PlayPressedAnimaation();
                Score += ADD_SCORE_VALUE;

                if (Score % SPECIAL_SCORE_VALUE == 0)
                {
                    _particles.SetActive(true);
                    _animator.PlaySpecial();
                }
            }
            else
            {
                _zones[index].PlayFailedAnimation();
            }
        }

        private bool IsSucsesfulClick(int number)
        {
            int index = number - 1;
            return _zones[index].CheckReady();
        }
    }
}