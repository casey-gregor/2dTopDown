// Author: Egor Geisik

using UnityEngine;
using Zenject;

namespace ProjectFiles.Scripts
{
    [RequireComponent(typeof(BoxCollider2D))]
    public sealed class LevelBoundsChecker : MonoBehaviour
    {
        private int _bulletLayer;

        [Inject]
        private void Construct(int bulletLayerMask)
        {
            _bulletLayer = bulletLayerMask;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            int objectLayer = 1<<other.gameObject.layer;
            if ((objectLayer & _bulletLayer) != 0 && other.gameObject.activeSelf)
            {
                Bullet bullet = other.gameObject.GetComponent<Bullet>();
                bullet.DestroyBullet();
            }
        }
    }
}