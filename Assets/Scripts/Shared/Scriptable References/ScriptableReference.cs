using Sirenix.OdinInspector;
using UnityEngine;

namespace Shared.Scriptable_References
{
    public abstract class ScriptableReference<T> : ScriptableObject
    {
        [SerializeField] private bool _hasStartValue;

        [SerializeField, ShowIf("_hasStartValue")]
        private T _startValue;

        [SerializeField] private T _value;

        public delegate void ValueEvent();

        public event ValueEvent OnValueChanged;


        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChanged?.Invoke();
            }
        }

        public void Reset()
        {
            if (_hasStartValue)
                _value = _startValue;
        }
    }
}