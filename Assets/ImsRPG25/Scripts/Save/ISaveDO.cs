using UnityEngine;

namespace ImsGDK    // 'Immersive Studios' -> 'Game Development Kit (GDK)'.
{

    interface ISaveDO
    {
        public void DoReset();
        public void GetRuntime(GameObject inRootGO);
        public void SetRuntime(GameObject inRootGO);
    }
}
