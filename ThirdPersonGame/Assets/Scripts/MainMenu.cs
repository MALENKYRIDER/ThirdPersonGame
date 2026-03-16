using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _loadLevelButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private TMP_InputField _levelInput;
    [SerializeField] private Image _incorrectLevelIndicator;
    [SerializeField] private TMP_Text _incorrectInputText;

    private int _selectedLevel;

    private void OnEnable()
    {
        _loadLevelButton.onClick.AddListener(OnLoadLevelClick);
        _quitButton.onClick.AddListener(OnQuitClick);
        _levelInput.onValueChanged.AddListener(OnLevelSelected);
    }

    private void OnDisable()
    {
        _loadLevelButton.onClick.RemoveListener(OnLoadLevelClick);
        _quitButton.onClick.RemoveListener(OnQuitClick);
        _levelInput.onValueChanged.RemoveListener(OnLevelSelected);
    }

    private void Start()
    {
        _incorrectInputText.enabled = false;
        _incorrectLevelIndicator.enabled = false;
    }

    private void OnLoadLevelClick()
    {
        if (LevelManager.IsLevelCorrect(_selectedLevel))
        {
            LoadLevel(_selectedLevel);
        }
        else
        {
            _incorrectInputText.enabled = true;
            _incorrectLevelIndicator.enabled = true;
            Debug.Log($"There is no level {_selectedLevel}");
        }
    }

    private void OnQuitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        return;
#endif
        Application.Quit();
    }

    private void OnLevelSelected(string value)
    {
        _selectedLevel = int.Parse(value);

        if (LevelManager.IsLevelCorrect(_selectedLevel))
        {
            _incorrectInputText.enabled = false;
            _incorrectLevelIndicator.enabled = false;
        }
    }

    private void LoadLevel(int level)
    {
        LevelManager.Load(level);
    }
}