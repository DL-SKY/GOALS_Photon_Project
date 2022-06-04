using GOALS.Windows.Enums;
using System;
using UnityEngine;

namespace GOALS.Windows
{
    [Serializable]
    public class WindowsLayer
    {
        public EnumWindowLayer key;
        public Transform value;

        public WindowsLayer(EnumWindowLayer key, Transform value)
        {
            this.key = key;
            this.value = value;
        }
    }
}
