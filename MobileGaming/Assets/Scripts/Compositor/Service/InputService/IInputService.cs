using UnityEngine;

namespace Service
{
    public interface IInputService : IService
    {
        public PlayerControls controls { get; }
        public Vector2 cursorPosition { get; }
        public Vector2 deltaPosition { get; }
    }
}


