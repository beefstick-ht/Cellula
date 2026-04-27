using UnityEngine;

    public interface IDamageable
    {
        bool Invincible { get; set; }
        void TakeDamage(float amount);
    }

