using System;
using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 10f;
    private void Start()
    {
        StartCoroutine(DoRotate());
    }

    private IEnumerator DoRotate()
    {
        while (true)
        {
            transform.rotation *= Quaternion.Euler(0, _rotationSpeed * Time.deltaTime, 0);
            yield return null;
        }
    }
}
