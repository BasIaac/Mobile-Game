using UnityEngine;

namespace Addressables.Components
{
    public class JoystickComponents : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private GameObject _parentGo;
        [SerializeField] private RectTransform _parentTr;
        [SerializeField] private RectTransform _joystickTr;

        public GameObject parentGo => _parentGo;
        public RectTransform parentTr => _parentTr;
        public RectTransform joystickTr => _joystickTr;
    }
}
