using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
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

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private ClipData[] _clipData;

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
            TextException();

            Score = 0;
            _particles.SetActive(false);

            for (int i = 0; i < _zones.Length; i++)
            {
                int zoneIndex = i;
                _zones[i].Init(() => HandleFail(zoneIndex));
            }

            StartCoroutine(SpawnZones());
        }

        private async void TextException()
        {
            int index = 3;

            // var thirdPartService = new ThirdPartService();
            using (var thirdPartService = new ThirdPartService())
            {
                try
                {
                    await thirdPartService.LoadScene(3);
                }
                catch (IndexOutOfRangeException e)
                {
                    string data = "";

                    foreach (object key in e.Data.Keys)
                    {
                        data += $"[{key}]:{e.Data[key]}\n";
                    }

                    Debug.LogError($"Index = {data}\n" +
                                   $"StackTrace {e.StackTrace}");
                }
                catch (Exception e)
                {
                    Debug.LogError($"Exception '{e}' was thrown\nMessage: {e.Message}");
                    // throw;
                }
                finally
                {
                    thirdPartService.StopLoading();
                    // thirdPartService.Dispose();
                }

                thirdPartService.DoAction2();
            }
        }

        private void Update()
        {
            bool isPlaying = _animator.IsPlayingSpecial();

            if (isPlaying && !_isSpecialPlaying)
            {
                _isSpecialPlaying = true;
                EnableParticles();
            }

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
            var clipData = _clipData[Random.Range(0, _clipData.Length)];
            _audioSource.clip = clipData.Clip;
            _audioSource.Play();

            float elapsedTime = 0;
            int index = 0;

            float clipDuration = clipData.Clip.length;


            while (elapsedTime <= clipDuration)
            {
                if (index < clipData.Times.Length)
                {
                    float timeMarker = clipData.Times[index];

                    if (elapsedTime >= timeMarker)
                    {
                        Debug.Log($"{index}: {clipData.Times[index]}");
                        index++;

                        int randomIndex = Random.Range(0, _zones.Length);

                        _zones[randomIndex].PlayStartAnimation();

                        yield return new WaitForSeconds(_zones[randomIndex].GetDuration());

                        yield return new WaitForSeconds(0.3f);
                    }
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            Debug.Log($"Finish");
        }

        private void HandleFail(int index)
        {
            _zones[index].PlayFailedAnimation();
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