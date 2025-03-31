// Author: Egor Geisik

using UnityEngine;

namespace ProjectFiles.Scripts.Player.Weapon
{
    public sealed class StraightBulletMovementStrategy : IBulletMovementStrategy
    {
        public void Move(Rigidbody2D rb, Vector2 direction, float speed)
        {
            rb.linearVelocity = direction * speed;
        }
    }
}