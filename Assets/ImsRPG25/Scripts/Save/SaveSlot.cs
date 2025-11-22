using UnityEngine;
using System;
using System.IO;
using System.Text;

namespace ImsGDK    // 'Immersive Studios' -> 'Game Development Kit (GDK)'.
{

    [System.Serializable]
    public abstract class SaveSlotSO : ScriptableObject
    {
        [Header("Save Slot Settings")]

        [Tooltip("OS /Path/File.ext after Application.dataPath value.\r\n\r\nMust start with a slash (/).")]
        [SerializeField] public string savePathFileExt = "/SaveSlot##.json";
        private string _saveSlotFqn;            // Fully Qualified Name (FQN).

//----------------------------------------------------------------------------
// - Unity ScriptableObject event hooks.

        // public virtual void Awake()
        // {
#if UNITY_EDITOR
        //     Debug.Log($"ImsGDK.SaveSlotSO as {name}.Awake() start.");
#endif
        // }

        // public virtual void OnEnable()
        // {
#if UNITY_EDITOR
        //     Debug.Log($"ImsGDK.SaveSlotSO as {name}.OnEnable() start.");
#endif
        // }

        // public virtual void OnDisable()
        // {
#if UNITY_EDITOR
        //     Debug.Log($"ImsGDK.SaveSlotSO as {name}.OnDisable() start.");
#endif
        // }

        // public virtual void OnDestroy()
        // {
#if UNITY_EDITOR
        //     Debug.Log($"ImsGDK.SaveSlotSO as {name}.OnDestroy() start.");
#endif
        // }

        // public virtual void OnValidate()
        // {
#if UNITY_EDITOR
        //     Debug.Log($"ImsGDK.SaveSlotSO as {name}.OnValidate() start.");
#endif
        // }

        public virtual void Reset()
        {
#if UNITY_EDITOR
            // Debug.Log($"ImsGDK.SaveSlotSO as {name}.Reset() start.");
#endif
            DoReset();
        }

//----------------------------------------------------------------------------

        public virtual void DoReset()
        {
            return;
        }

        public virtual bool DoSaveJson()
        {
            string jsonContent = JsonUtility.ToJson(this);
            try {
                File.WriteAllText(_saveSlotFqn, jsonContent, Encoding.ASCII);
            }
            catch (Exception e)
            {
                Debug.LogError($"ImsGDK.SaveSlotSO as {name}.DoSaveJson() with {e.ToString()}");
                // throw;     // Intentionally not re-throwing.
                return false;
            }
            return true;
        }

        public virtual bool DoLoadJson()
        {
            // Is there any file to actually load?
            if (!File.Exists(_saveSlotFqn))
            {
                return false;
            }

            // Read the text file from the file system.
            string jsonContent = "";
            try {
                jsonContent = File.ReadAllText(_saveSlotFqn, Encoding.ASCII);
            }
            catch (Exception e)
            {
                Debug.LogError($"ImsGDK.SaveSlotSO as {name}.DoLoadJson().ReadAllText() with {e.ToString()}");
                // throw;     // Intentionally not re-throwing.
                return false;
            }

            // Transpose the json text content into a runtime class instance.
            DoReset();
            try {
                JsonUtility.FromJsonOverwrite(jsonContent, this);
            }
            catch (Exception e)
            {
                Debug.LogError($"ImsGDK.SaveSlotSO as {name}.DoLoadJson().FromJsonOverwrite() with {e.ToString()}");
                // throw;     // Intentionally not re-throwing.
                return false;
            }
            return true;
        }

//----------------------------------------------------------------------------

    }
}
