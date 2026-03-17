using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour

{
    private const int _maxLevel = 1;
    private static CancellationTokenSource _cts = new();

    public static bool IsLevelCorrect(int level)
    {
        return level > 0 && level <= _maxLevel;
        
    }

    public static void StopLoading()
    {
        _cts.Cancel();
    }

    public static async Task<bool> LoadAsync(int level)
    {
        Debug.Log($"Start laoding level {level}");
        await Task.Delay(3000);

        if (_cts.IsCancellationRequested)
        {
            _cts = new CancellationTokenSource();
            return false;
        }

        await SceneManager.LoadSceneAsync(level);
        Debug.Log($"Laoded level {level}");
        return true;
    }

    public static IEnumerator LoadByCoroutine(int level)
    {
        Debug.Log($"Start laoding level {level}");
        yield return new WaitForSeconds(5);
        yield return SceneManager.LoadSceneAsync(level);
        Debug.Log($"Laoded level {level}");
    }
}