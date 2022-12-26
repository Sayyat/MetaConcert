using UnityEngine;

namespace UI
{
    public class TestStarter : MonoBehaviour
    {
        [SerializeField] private GameObject userUIPrefab;

        private UserUIView _userUIView;

        private void Awake()
        {
            _userUIView = Instantiate(userUIPrefab).GetComponent<UserUIView>();
        }
    }
}