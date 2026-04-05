using UnityEngine;

namespace DanceBattle
{
    [CreateAssetMenu(menuName = nameof(DanceBattle) + "/" + nameof(ActionZoneSettings),
        fileName = nameof(ActionZoneSettings))]
    public class ActionZoneSettings : ScriptableObject
    {
        [SerializeField] private float _playDuration = 2f;
        [SerializeField] private float _playThreshhold = 1.2f;
        [SerializeField] private Vector2 _playScale = new(1.75f, 0.8f);
        [SerializeField] private Color _failColor = new Color(1f, 0f, 0f, 0f);
        [SerializeField] private float _failDuration = 0.3f;
        
        public float PlayDuration => _playDuration;
        public float PlayThreshhold => _playThreshhold;
        public Vector2 PlayScale => _playScale;
        public Color FailColor => _failColor;
        public float FailDuration => _failDuration;
    }
}