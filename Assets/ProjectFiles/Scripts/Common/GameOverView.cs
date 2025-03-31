using UnityEngine;

namespace ProjectFiles.Scripts.Common
{
    public sealed class GameOverView : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}