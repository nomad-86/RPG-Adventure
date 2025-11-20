using UnityEngine;

namespace ImsGDK    // 'Immersive Studios' -> 'Game Development Kit (GDK)'.
{

    [CreateAssetMenu(fileName = "SaveSession##", menuName = "Immersive Studios (GDK)/Assets > Save/Save Session Slot (ScriptableObject)")]
    [System.Serializable]
    public class SaveSession : SaveSlotSO
    {

//----------------------------------------------------------------------------

        public override void DoReset()
        {
            base.DoReset();

            // Reset session data. Like Options, Player, Interactions, etc.
            DoReset_Options();
            DoReset_Player();
            DoReset_Interactions();
            return;
        }

//----------------------------------------------------------------------------

        // [John] Add 'Player Attributes' here.
        // [Observation] Yes, this could be organised better for scalability.
        //               But, for now just get it working.

        // Define 'Player Attributes' persisted in a SaveSession file.
        public float playerHealthAmount;
        public float playerHealthPotion;
        public float playerStaminaAmount;
        public float playerStaminaPotion;
        public float playerPowerAmount;
        public float playerPowerPotion;
        public float playerFocusAmount;
        public float playerFocusPotion;
        public int playerExperienceAmount;
        public int playerExperienceLevel;

        public void DoReset_Player()
        {
            // Reset 'Player Attributes' in this SaveSession class.
            playerHealthAmount = 0;
            playerHealthPotion = 0;
            playerStaminaAmount = 0;
            playerStaminaPotion = 0;
            playerPowerAmount = 0;
            playerPowerPotion = 0;
            playerFocusAmount = 0;
            playerFocusPotion = 0;
            playerExperienceAmount = 0;
            playerExperienceLevel = 0;
        }

        public virtual void GetRuntime_Player(GameObject inPlayerGO)
        {
            // Get 'Player Attributes' runtime into SaveSession class.
            // [Do] Assign data member here ...
            // [Do] Assign data member here ...
            // [Do] Assign data member here ...
        }

        public virtual void SetRuntime_Player(GameObject inPlayerGO)
        {
            // Set SaveSession class into 'Player Attributes' runtime.
            // [Do] Assign data member here ...
            // [Do] Assign data member here ...
            // [Do] Assign data member here ...
        }

//----------------------------------------------------------------------------

        // [April] Add 'Options' here ... similar to Player section (just above).
        // [Observation] Yes, this could be organised better for scalability.
        //               But, for now just get it working.

        // Define 'Options' persisted in a SaveSession file.
        // [Do] Define data member here ...
        // [Do] Define data member here ...
        // [Do] Define data member here ...

        public void DoReset_Options()
        {
            // Reset 'Options' in this SaveSession class.
            // [Do] Assign data member default value here ...
            // [Do] Assign data member default value here ...
            // [Do] Assign data member default value here ...
        }

        public virtual void GetRuntime_Options(GameObject inOptionsGO)
        {
            // Get 'Options' runtime into SaveSession class.
            // [Do] Assign data member here ...
            // [Do] Assign data member here ...
            // [Do] Assign data member here ...
        }

        public virtual void SetRuntime_Options(GameObject inOptionsGO)
        {
            // Set SaveSession class into 'Options' runtime.
            // [Do] Assign data member here ...
            // [Do] Assign data member here ...
            // [Do] Assign data member here ...
        }

//----------------------------------------------------------------------------

        // [Tyler] Add 'Interaction' here ... similar to Player section (just above).
        // [Observation] Yes, this could be organised better for scalability.
        //               But, for now just get it working.

        // Define 'Interactions' persisted in a SaveSession file.
        // [Do] Define data member here ...
        // [Do] Define data member here ...
        // [Do] Define data member here ...

        public void DoReset_Interactions()
        {
            // Reset 'Interactions' in this SaveSession class.
            // [Do] Assign data member default value here ...
            // [Do] Assign data member default value here ...
            // [Do] Assign data member default value here ...
        }

        public virtual void GetRuntime_Interactions(GameObject inInteractionsGO)
        {
            // Get 'Interactions' runtime into SaveSession class.
            // [Do] Assign data member here ...
            // [Do] Assign data member here ...
            // [Do] Assign data member here ...
        }

        public virtual void SetRuntime_Interactions(GameObject inInteractionsGO)
        {
            // Set SaveSession class into 'Interactions' runtime.
            // [Do] Assign data member here ...
            // [Do] Assign data member here ...
            // [Do] Assign data member here ...
        }

//----------------------------------------------------------------------------

    }
}
