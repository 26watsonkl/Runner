using UnityEngine;

namespace GameNamespace
{
    public class PlayerStats : MonoBehaviour
    {
        public float CurrentHealth { get; private set; } = 100f; // Current health of the player
        public float Stamina { get; private set; } = 100f; // Player's stamina

        // Method to set health
        public void SetHealth(float health)
        {
            CurrentHealth = Mathf.Clamp(health, 0f, 100f); // Cap health between 0 and 100
        }

        // Method to set stamina
        public void SetStamina(float stamina)
        {
            Stamina = Mathf.Clamp(stamina, 0f, 100f); // Cap stamina between 0 and 100
        }
    }
} 