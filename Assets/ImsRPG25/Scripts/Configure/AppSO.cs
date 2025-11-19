using UnityEngine;
using UnityEngine.SceneManagement;

namespace ImsGDK    // 'Immersive Studios' -> 'Game Development Kit (GDK)'.
{
    [CreateAssetMenu(fileName = "ImsApp", menuName = "Immersive Studios (GDK)/Assets > Configure/App (ScriptableObject)")]
    [System.Serializable]
    /**
     * Declare and define an 'Application' ScriptableObject (SO) class to live
     * for global runtime execution lifecycle, independant of Scenes. 
     *
     * - While static members & methods can exist here, in AppSO class, they
     *   have very little significance or ability to hook into the broarder
     *   Unity Object runtime behaviour ... as a basic AppSO.cs file.
     *   - Static members & methods in AppSO class can still be useful for
     *     non-unity related capabilities; like instance counting, messaging,
     *     file validation, runtime environment validation, etc (if required).
     *   - Like AppUnityIdeSO class calls ImsGDK.AppSO.Get().DoQuitGame().
     * - To get ScriptableObjects hooked into the broarder Unity Object
     *   runtime behaviour, most developers leverage embedding the SO into
     *   other classes who inherit from Unity base classes like MonoBehaviour,
     *   PlayableBehaviour and PlayableAsset. In turn, this leverages the
     *   Unity integrated behaviour for Scenes and their embedded GameObject
     *   hierarchies; either directly, dynamically or pre-fabricated.
     *
     * - But we do NOT want to be part of the GameObject lifecycle chaos.
     *
     * - [Outcome] We want the AppSO class to be decoupled from the create &
     *   destroy chaos of GameObjects in a scene.
     * - [Outcome] We want the AppSO class to instanciate, once for the life
     *   of the application/game runtime existance.
     * - [Outcome] We want the AppSO class to be exposed to the Unity IDE
     *   Inspector for anyone in the team to configure it.
     *   - [Outcome] And that configuration needs to be persistent and
     *     packaged with compiled builds for distribution.
     * - [Outcome] We want the AppSo class to exist outside of scenes and not
     *   be reliant upon scene editors having to remember to put in any kind
     *   of GameManger or AppManager GameObject into all the scenes.
     *   - [Outcome] In turn, this probably implies the need for a clean and
     *     static way for any class of any type instancied anywhere by
     *     anymeans to be able to quicly reference this global AppSO instance.
     *
     * - [Solution] A global class instance that is Unity IDE Inspector
     *   configurable and live for the runime lifetime, needs ...
     *   - A /Assets/ImsRPG25/Scripts/Configure/AppSO.cs ScriptableObject.
     *   - A /Assets/ImsRPG25/App/Configure/ImsApp.asset Asset of that SO. 
     *   - Now the ScriptableObject asset instance has access to the Unity
     *   event hooks for Awake(), OnEnable(), OnDisable() and OnDestroy() at
     *   runtime; with OnValidate() and Reset() when in the Unity IDE.
     *
     * - [ActionRequired] This ScriptableObject declaration-definition also
     *   needs to be created as Unity Asset instance, in project directories.
     *   - Exposing the AppSO class as configurable in a Unity IDE Inspector.
     *   - As a project asset, the AppSO class also gets instanciated as a
     *     Unity runtime object during gameplay; in Unity IDE Play Mode and in
     *     all compiled builds for target platforms.
     *   - Like declare as /Assets/ImsRPG25/App/Configure/ImsApp.asset in
     *     'Unity IDE' -> '/Assets/ImsRPG25/App/Configure/' directory by
     *     RCLK -> 'Create' menu
     *          -> 'Immersive Studios (GDK)' item
     *          -> 'Assets > Configure' item
     *          -> 'App (ScriptableObject)' item
     *     ... and keep the default filename suggestion of 'ImsApp'.
     *   - Now the Unity game engine will declare an 'ImsApp' named asset
     *     instanciance of the ImsGDK.AppSO class.
     *   - Now the Unity IDE Inspector can be used to configure and save the
     *     values in the /Assets/ImsRPG25/App/Configure/ImsApp.asset file.
     *     - Default values come from definition in ImsGDK.AppSO class, but
     *       overwriten with Unity IDE Inspector values as an ImsApp.asset.
     *   - The OnEnable() event hook seems to always be called when using this
     *     approach; both, in the Unity IDE Play Mode and a compiled build.
     *   - This approach does NOT require any relationship to a GameObject.
     *     - Most internet examples I could find about ScriptableObjects were
     *       all relying on a Scene GameObject instance to instanciate the
     *       ScriptableObject; but, we want class instanciation that is
     *       independent of the scene lifecycle that destroys GameObjects
     *       when switching scenes. AppSO should avoid scene chaos.
     *
     * @see   https://docs.unity3d.com/6000.2/Documentation/Manual/class-ScriptableObject.html
     * @see   https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/constructors
     * @since 0.0.1 Introduced.
     */
    public class AppSO : ScriptableObject
    {
        [Space(10)]
        [TextAreaAttribute(1,15)]
        [Tooltip("[UnityIDE] Add helpfull configuration hints here.\r\n\r\nOnly visible in Unity IDE Inspector.")]
        #pragma warning disable 0414
        [SerializeField] private string _notesForAllTeamMembers = "";

//----------------------------------------------------------------------------

        // A static reference to 'this' instances for the static Get() method.
        // For static ImsGDK.AppSO.Get()._____ easy runtime access.
        private static ImsGDK.AppSO _appSO = null;

        /**
         * A static method for all classes to use ImsGDK.AppSO.Get().______ 
         * to access public members & methods of ImsGDK.AppSO class.
         *
         * @see   https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/static-classes-and-static-class-members
         * @since 0.0.1 Introduced.
         */
        public static ImsGDK.AppSO Get()
        {
            if (null == _appSO)
            {
                _appSO = new ImsGDK.AppSO();
            }
            return _appSO;
        }

        public AppSO ()
        {
#if UNITY_EDITOR
            if (null != _appSO)
            {
                Debug.Log($"[Warning] A ImsGDK.AppSO class second instance has been created. This is not desirable. Please, only create one asset instance of this ScriptableObject in the Unity project /Asset/ directories.");
            }
#endif
            // Last ImsGDK.AppSO class instanciated, wins.
            _appSO = this;
        }

//----------------------------------------------------------------------------
// - Unity ScriptableObject event hooks.

        // public void Awake()
        // {
        //     Debug.Log($"ImsGDK.AppSO as {name}.Awake() start.");
        // }

        // public void OnEnable()
        // {
        //     Debug.Log($"ImsGDK.AppSO as {name}.OnEnable() start.");
        // }

        // public void OnDisable()
        // {
        //     Debug.Log($"ImsGDK.AppSO as {name}.OnDisable() start.");
        // }

        // public void OnDestroy()
        // {
        //     Debug.Log($"ImsGDK.AppSO as {name}.OnDestroy() start.");
        // }

        // public void OnValidate()
        // {
        //     Debug.Log($"ImsGDK.AppSO as {name}.OnValidate() start.");
        // }

        // public void Reset()
        // {
        //     Debug.Log($"ImsGDK.AppSO as {name}.Reset() start.");
        // }

//----------------------------------------------------------------------------

        /**
         * (Q)uit the application/game NOW!
         *
         * - Players return to their Operating System (OS).
         * - Developers return to the Unity IDE.
         * - Public scope enables 'UI' element -> 'OnClick()' event hook.
         *
         * @see    https://docs.unity3d.com/ScriptReference/Application.Quit.html
         * @see    https://docs.unity3d.com/6000.2/Documentation/ScriptReference/EditorApplication-isPlaying.html
         * @since  0.0.1 Introduced.
         * @return void
         */
        virtual public void DoQuitNow()
        {
            QuitToDesktopNow();
        }

        virtual protected void QuitToDesktopNow()
        {
            // Will quit the game when run as stand alone EXE.
            Application.Quit();

#if UNITY_EDITOR
            // Will quit the game when run in the Unity Editor Play Mode.
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

//----------------------------------------------------------------------------

        /**
         * imsAppScenes enum list to easily identify Unity Scenes indexes.
         *
         * - [Maintain] Ensure 'enum imsAppScenes' has all named scene indexs.
         *   - Visit 'Unity IDE' -> 'File' menu -> 'Build Profiles' item -> 'Scene List' section.
         *   - Visit enum in /Assets/{vendorProductCode}/Scripts/Configure/ImsAppSO.cs
         *
         * @see    https://docs.unity3d.com/6000.2/Documentation/ScriptReference/SceneManagement.SceneManager.LoadScene.html
         * @see    https://docs.unity3d.com/6000.2/Documentation/ScriptReference/InspectorNameAttribute.html
         * @see    https://docs.unity3d.com/6000.2/Documentation/ScriptReference/EnumButtonsAttribute.html
         * @since  0.0.1 Introduced.
         * @param  imsAppScenes inSceneindex The specific scene index number found in 'Unity IDE' -> 'File' menu -> 'Build Profiles' item -> 'Scene List' section.
         * @return void
         */
        public enum imsAppScenes : int
        {
            gameMenu = 0,    // A 'Build Profiles' -> 'Scene List' scene idex.
            gameOver = 1,    // A 'Build Profiles' -> 'Scene List' scene idex.
            level1 = 2,      // A 'Build Profiles' -> 'Scene List' scene idex.
            level2 = 3,      // A 'Build Profiles' -> 'Scene List' scene idex.
            level3 = 4       // A 'Build Profiles' -> 'Scene List' scene idex.
            // [Maintain] Add more scenes to enum using this format.
            // {sceneNameShort} = {sceneIndex},    // A 'Build Profiles' -> 'Scene List' scene idex.
        }

        /**
         * Change the gameplay experience to include a specific Unity scene.
         *
         * - Use LoadSceneMode.Single to replace the current loaded scene.
         * - Use LoadSceneMode.Additive to add new scene GameObjects as well.
         * - [Maintain] Ensure 'enum gmScenes' has all named scene indexs.
         *   - Visit 'Unity IDE' -> 'File' menu -> 'Build Profiles' item -> 'Scene List' section.
         *   - Visit enum in /Assets/{vendorProductCode}/Scripts/Configure/ImsAppSO.cs
         *
         * @see    https://docs.unity3d.com/6000.2/Documentation/ScriptReference/SceneManagement.SceneManager.LoadScene.html
         * @since  0.0.1 Introduced.
         * @param  imsAppScenes  inSceneindex The specific scene index number found in 'Unity IDE' -> 'File' menu -> 'Build Profiles' item -> 'Scene List' section.
         * @param  LoadSceneMode inMode       The scene mode when loading into a player. Like 'Single' or 'Additive'.
         * @return void
         */
        virtual public void DoLoadScene(imsAppScenes inSceneIndex, LoadSceneMode inMode = LoadSceneMode.Single)
        {
            // Debug.Log($"ImsGDK.AppSO as {name}.DoLoadScene({inSceneIndex}, {inMode}) start.");

            SceneManager.LoadScene((int) inSceneIndex, inMode);
        }

        /**
         * Change the gameplay experience to include a specific Unity scene.
         *
         * - Use LoadSceneMode.Single to replace the current loaded scene.
         * - Use LoadSceneMode.Additive to add new scene GameObjects as well.
         * - [Maintain] Ensure 'enum gmScenes' has all named scene indexs.
         *   - Visit 'Unity IDE' -> 'File' menu -> 'Build Profiles' item -> 'Scene List' section.
         *   - Visit enum in /Assets/{vendorProductCode}/Scripts/Configure/ImsAppSO.cs
         *
         * @see    https://docs.unity3d.com/6000.2/Documentation/ScriptReference/SceneManagement.SceneManager.LoadScene.html
         * @since  0.0.1 Introduced.
         * @param  string        inSceneName The specific scene path/name found in 'Unity IDE' -> 'File' menu -> 'Build Profiles' item -> 'Scene List' section.
         * @param  LoadSceneMode inMode      The scene mode when loading into a player. Like 'Single' or 'Additive'.
         * @return void
         */
        virtual public void DoLoadScene(string inSceneName, LoadSceneMode inMode = LoadSceneMode.Single)
        {
            // Debug.Log($"ImsGDK.AppSO as {name}.DoLoadScene({inSceneName}, {inMode}) start.");

            SceneManager.LoadScene(inSceneName, inMode);
        }

//----------------------------------------------------------------------------

    }
}
