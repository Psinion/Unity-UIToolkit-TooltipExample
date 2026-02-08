using System;
using UIToolkit.Tooltip.Example.Core;
using UnityEngine;

namespace UIToolkit.Tooltip.Example.Gameplay
{
    [DefaultExecutionOrder(-1000)]
    public class GameResourcesService : SingletonMono<GameResourcesService>
    {
        [SerializeField]
        private float moneyAddInterval = 1f;
        private float moneyAddIntervalCurrent;
        
        public event Action<int> OnMoneyChanged;

        [SerializeField]
        private int moneyCurrent;
        public int MoneyCurrent
        {
            get => moneyCurrent;
            private set
            {
                moneyCurrent = value;
                OnMoneyChanged?.Invoke(value);
            }
        }
        
        public void SetMoney(int amount)
        {
            MoneyCurrent = amount;
        }
    
        public void AddMoney(int amount)
        {
            MoneyCurrent += amount;
        }

        private void Update()
        {
            if (moneyAddIntervalCurrent < moneyAddInterval)
            {
                moneyAddIntervalCurrent += Time.deltaTime;
                return;
            }
            moneyAddIntervalCurrent = 0;
            
            AddMoney(25);
        }
    }
}