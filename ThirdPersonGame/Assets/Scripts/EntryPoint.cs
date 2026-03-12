using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private CanvasGroup _menu; 
    
    void Start()
    {
        Debug.Log("Start initialization");
        _menu.alpha = 0;
        _menu.blocksRaycasts = false;
        //set pause
        
        Debug.Log("Game initialized");
        _menu.alpha = 1;
        _menu.blocksRaycasts = true;
    }
}
