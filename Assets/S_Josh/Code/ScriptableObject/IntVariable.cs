using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntVariable : ScriptableObject
{
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "Int for each UI";
#endif
        public int Value;
        public int MaxValue;
        public bool IsLimit = false;
        public void SetValue(int value)
        {
           
            if(IsLimit)
            {
                if(value > MaxValue)
                {
                    Value = MaxValue;
                }
            }
            else
            {
                Value = value;
            }
            
        }

        public void SetValue(IntVariable value)
        {
            if(IsLimit)
            {
                if(value.Value > MaxValue)
                {
                    Value = MaxValue;
                }
            }
            else
            {
                Value = value.Value;
            }
        }

        public void AddValue(int value)
        {
            if(IsLimit)
            {
                if(Value + value > MaxValue)
                {
                    Value = MaxValue;
                }
                else
                {
                    Value += value;
                }
            }
            else
            {
                Value += value;
            }
        }
}
