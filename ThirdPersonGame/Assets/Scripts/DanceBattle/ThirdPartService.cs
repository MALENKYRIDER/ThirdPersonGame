using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace DanceBattle
{
    public class ThirdPartService : IDisposable
    {
        private GameObject _go;
        private bool[] _bools = new bool[2];

        public ThirdPartService()
        {
             _go = new GameObject();
        }

        public async Task<bool> LoadScene(int index)
        {
            throw new MyException("LoadScene");
            await SceneManager.LoadSceneAsync(index);
            return true;
        }

        public void DoAction2()
        {
            Debug.Log("DoAction2");
            Debug.Log($"_go is null: [{_go == null}]");
        }

        public void StopLoading()
        {
            Debug.Log("StopLoading");
        }

        public void Dispose()
        {
            Debug.Log("Dispose");
            Object.DestroyImmediate(_go);
        }
    }

    public class MyException : Exception
    {
        public MyException(string message) : base(message)
        {
        }
    }
}