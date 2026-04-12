using System;
using UnityEngine;

namespace DanceBattle
{
    [CreateAssetMenu(menuName = "DanceBattle/ClipData")]
    public class ClipData : ScriptableObject
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField] private float[] _times;
        
        public AudioClip Clip => _clip;
        public float[] Times => _times;
    }
}