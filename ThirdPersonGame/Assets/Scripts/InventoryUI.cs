using System;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI _coinText;

    private void Start()
    {
        _coinText =  GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCoinText(PlayerInventory playerInventory)
    {
        _coinText.text = playerInventory.NumberOfCoins.ToString();
    }
}
