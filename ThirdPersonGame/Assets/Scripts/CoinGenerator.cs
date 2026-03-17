using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinGenerator : MonoBehaviour
{
    [SerializeField] private BoxCollider _spawnArea;
    [SerializeField] private Coin _coinPrefab;

    private IEnumerator Start()
    {
        Debug.Log("Start spawn");

        var delay = new WaitForSeconds(0.05f);

        for (int i = 0; i < 50; i++)
        {
            var bounds = _spawnArea.bounds;
            var boundsMin = bounds.min;
            var boundsMax = bounds.max;
            Instantiate(_coinPrefab,
                new Vector3(
                    Random.Range(boundsMin.x, boundsMax.x),
                    Random.Range(boundsMin.y, boundsMax.y),
                    Random.Range(boundsMin.z, boundsMax.z)),
                Quaternion.identity,
                _spawnArea.transform);
            yield return delay;
            yield return StartCoroutine(TestCoroutine());
        }
        
        Debug.Log("All coins were spawned");
    }

    private IEnumerator TestCoroutine()
    {
        yield return null;
    }

    private void OnEnable()
    {
        
    }
}