using UnityEngine;

namespace Quest
{
    public class Collector : MonoBehaviour
    {
        private int _coinsSum;
        private int _valuesSum;

        public void AddCoin(int value)
        {
            _coinsSum++;
            _valuesSum += value;
            
            Debug.Log($"You earned coin:\nCoins: {_coinsSum}\nValue: {_valuesSum}");
        }
        
        
        
    }
}