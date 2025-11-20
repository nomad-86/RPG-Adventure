using UnityEngine;
using System.IO;


namespace ImsGDK    // 'Immersive Studios' -> 'Game Development Kit (GDK)'.
{

    [System.Serializable]
    public class SavePlayerDO : ISaveDO
    {

//----------------------------------------------------------------------------

        // [John] Add 'Player Attributes' to this SavePlayer DataObject (DO).

        // Define 'Player Attributes' persisted in a SaveSession file.
        public int experienceAmount;
        public int experienceLevel;
        public float healthAmount;
        public float healthPotion;
        public float staminaAmount;
        public float staminaPotion;
        public float powerAmount;
        public float powerPotion;
        public float focusAmount;
        public float focusPotion;

        public void DoReset()
        {
            // Reset 'Player Attributes' in this SavePlayerDO class.
            experienceAmount = 0;
            experienceLevel = 0;
            healthAmount = 0;
            healthPotion = 0;
            staminaAmount = 0;
            staminaPotion = 0;
            powerAmount = 0;
            powerPotion = 0;
            focusAmount = 0;
            focusPotion = 0;
        }

        public virtual void GetRuntime(GameObject inPlayerGO)
        {
            // Get 'Player Attributes' runtime into SavePlayerDO class.
            // experienceAmount = ...;
            // experienceLevel = ...;
            // healthAmount = ...;
            // healthPotion = ...;
            // staminaAmount = ...;
            // staminaPotion = ...;
            // powerAmount = ...;
            // powerPotion = ...;
            // focusAmount = ...;
            // focusPotion = ...;
        }

        public virtual void SetRuntime(GameObject inPlayerGO)
        {
            // Set SavePlayerDO class into 'Player Attributes' runtime.
            // ... = experienceAmount;
            // ... = experienceLevel;
            // ... = healthAmount;
            // ... = healthPotion;
            // ... = staminaAmount;
            // ... = staminaPotion;
            // ... = powerAmount;
            // ... = powerPotion;
            // ... = focusAmount;
            // ... = focusPotion;
        }

//----------------------------------------------------------------------------
        
    }
}
