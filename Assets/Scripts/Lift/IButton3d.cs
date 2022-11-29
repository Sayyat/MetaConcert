using System;
using UnityEngine;

namespace Lift
{
    public interface IButton3d
    {
        public event Action<string, int> ButtonClicked;
     
    }
}