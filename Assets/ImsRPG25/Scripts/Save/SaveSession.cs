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

            // Reset 'Options' runtime DataObject (DO).
            // [Maybe?] if (null != saveOptionsDO) {
            // [Maybe?]     saveOptionsDO.DoReset();
            // [Maybe?] }

            // Reset 'Player Attributes' runtime DataObject (DO).
            if (null != savePlayerDO) {
                savePlayerDO.DoReset();
            }

            // Reset 'Interactions' runtime DataObject (DO).
            // [Maybe?] if (null != saveInteractionsDO) {
            // [Maybe?]     saveInteractionsDO.DoReset();
            // [Maybe?] }

            return;
        }

//----------------------------------------------------------------------------

        // [John] Add 'Player Attributes' here.

        // Define 'Player Attributes' persisted in a SaveSession file.
        public SavePlayerDO savePlayerDO;

//----------------------------------------------------------------------------

        // [April] Add 'Options' here.

        // Define 'Options' persisted in a SaveSession file.
        // [Maybe?] public saveOptionsDO saveOptionsDO;

//----------------------------------------------------------------------------

        // [Tyler] Add 'Interaction' here.

        // Define 'Interactions' persisted in a SaveSession file.
        // [Maybe?] public saveInteractionsDO saveInteractionsDO;

//----------------------------------------------------------------------------

    }
}
