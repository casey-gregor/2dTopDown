using UnityEngine;

namespace ProjectFiles.Scripts.Player.Weapon
{
    public interface IBulletMovementStrategy
    {
        void Move(Rigidbody2D rb, Vector2 direction, float speed);
    }
}