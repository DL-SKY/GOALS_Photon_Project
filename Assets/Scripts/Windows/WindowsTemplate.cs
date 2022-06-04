using System;
using UnityEngine;

namespace GOALS.Windows
{
    public abstract class WindowsTemplate : MonoBehaviour
    {
        public event Action OnInitialize;
        public event Action<WindowsTemplate, bool> OnClose;

        [Header("Main Settings")]
        [SerializeField] protected bool _canUseEsc;

        public bool IsInit { get; private set; }


        public virtual void Initialize(object data)
        {
            SetInitialize(true);
        }

        public void Close(bool result = false)
        {
            OnClose?.Invoke(this, result);
            
            SetInitialize(false);
            CustomClose(result);

            Destroy(gameObject);
        }

        public void OnClickEsc()
        {
            if (_canUseEsc)
                Close();

            CustomOnClickEsc();
        }


        protected void SetInitialize(bool state)
        {
            IsInit = state;

            if (IsInit)
                OnInitialize?.Invoke();
        }

        protected virtual void CustomClose(bool result) { }
        protected virtual void CustomOnClickEsc() { }
    }
}
