using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Quest
{
    public class Collector : MonoBehaviour
    {
        private int _coinsSum;
        private int _valuesSum;

        public event Action<int, int> CoinGrabbed;
        public void AddCoin(int value)
        {
            _coinsSum++;
            _valuesSum += value;
            CoinGrabbed?.Invoke(_coinsSum, _valuesSum);
            Debug.Log($"You earned coin:\nCoins: {_coinsSum}\nValue: {_valuesSum}");
        }
    }
}