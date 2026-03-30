using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _collectDurationSpeed = 1f;
    
    private bool _isCollected = false;
    
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

    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

        if (playerInventory != null)
        {
            _isCollected = true;
            playerInventory.CoinsCollected();

            CollectAnimation();
        }
        
        
    }

    private void CollectAnimation()
    {
        GetComponent<Collider>().enabled = false;
        
        transform.DORotate(new Vector3(0, 360 * 3, 0), _collectDurationSpeed, RotateMode.FastBeyond360)
            .SetEase(Ease.OutCubic);

        transform.DOScale(Vector3.zero, _collectDurationSpeed)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                Destroy(gameObject);
                Debug.Log("Coin collected");
            });
    }
}
