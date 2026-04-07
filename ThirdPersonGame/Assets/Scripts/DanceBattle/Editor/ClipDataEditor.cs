#if UNITY_EDITOR
using DanceBattle;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ClipData))]
public class ClipDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ClipData data = (ClipData)target;

        if (GUILayout.Button("Generate Beats"))
        {
            var clip = data.Clip;
            var beats = BeatDetector.DetectBeats(clip, 0.2f);

            typeof(ClipData)
                .GetField("_times", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(data, beats);

            EditorUtility.SetDirty(data);
        }
    }
}
#endif