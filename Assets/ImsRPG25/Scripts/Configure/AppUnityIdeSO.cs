/**
 * A Unity IDE utility for developer enhancements at application runtime.
 *
 * - A ScriptableObject (SO) class to live for the entire global runtime
 *   execution lifecycle, independant of the Scene GameObject chaos.
 *
 * - A helpfull configuration notes box in Unity IDE for team members.
 *   - Visit /Asstes/{vendorProductCode}/App/Configure/ImsAppUnityIde.asset
 *
 * - Adds 'EventSystem' -> 'AppUnityIdeGO_Listener' GameObject in all scenes.
 *   - Press (Q)uit to ImsGDK.AppSO.Get().DoQuitNow().
 *   - Press (0) to ImsGDK.AppSO.Get().DoLoadScene(AppSO.imsAppScenes.gameMenu).
 *   - Press (1) to ImsGDK.AppSO.Get().DoLoadScene(AppSO.imsAppScenes.level1).
 *   - Press (2) to ImsGDK.AppSO.Get().DoLoadScene(AppSO.imsAppScenes.level2).
 *   - Press (3) to ImsGDK.AppSO.Get().DoLoadScene(AppSO.imsAppScenes.level3).
 * 
 * @package   ImmersiveStudios\GameDevelopmentKit
 * @see       https://www.datamates.wtf/support/
 * @since     0.0.1 Introduced.
 * @author    John Lang at DataMates Studio <hello@datamates.wtf>
 * @license   GPLv3 or later
 * @copyright Copyright 2024-2025 DataMates Studio (https://www.datamates.wtf/).
 * This program is free software: you can redistribute it and/or modify it
 * under the terms of the GNU General Public License as published by the Free
 * Software Foundation, either version 3 of the License, or (at your option)
 * any later version. This program is distributed in the hope that it will be
 * useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details. You should have
 * received a copy of the GNU General Public License along with this program
 * in the license.txt file. If not, see <https://www.gnu.org/licenses/>.
 */

using UnityEngine;
using UnityEngine.SceneManagement;

namespace ImsGDK    // 'Immersive Studios' -> 'Game Development Kit (GDK)'.
{
    [CreateAssetMenu(fileName = "ImsAppUnityIde", menuName = "Immersive Studios (GDK)/Assets > Configure/App Unity IDE (ScriptableObject)")]
    [System.Serializable]
    /**
     * Declare and define an 'Application' ScriptableObject (SO) class to live
     * for global runtime execution lifecycle, independant of Scenes. 
     *
     * - While static members & methods can exist here, in AppUnityIdeSO
     *   class, they have very little significance or ability to hook into the
     *   broarder Unity Object runtime behaviour ... as a basic
     *   AppUnityIdeSO.cs file.
     *   - Static members & methods in AppUnityIdeSO class can still be useful
     *     for non-unity related capabilities; like instance counting,
     *     messaging file validation, runtime environment validation, etc
     *     (if required).
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
     * - [Outcome] We want the AppUnityIdeSO class to be decoupled from the
     *   create & destroy chaos of GameObjects in a scene.
     * - [Outcome] We want the AppUnityIdeSO class to instanciate, once for
     *   the life of the application/game runtime existance.
     * - [Outcome] We want the AppUnityIdeSO class to be exposed to the Unity
     *   IDE Inspector for anyone in the team to configure it.
     *   - [Outcome] And that configuration needs to be persistent and
     *     packaged with compiled builds for distribution.
     * - [Outcome] We want the AppUnityIdeSo class to exist outside of scenes
     *   and not be reliant upon scene editors having to remember to put in
     *   any kind of GameManger or AppManager GameObject into all the scenes.
     *   - [Outcome] In turn, this probably implies the need for a clean and
     *     static way for any class of any type instancied anywhere by
     *     anymeans to be able to quicly reference this global AppUnityIdeSO
     *     instance.
     *
     * - [Solution] A global class instance that is Unity IDE Inspector
     *   configurable and live for the runime lifetime, needs ...
     *   - A /Assets/ImsRPG25/Scripts/Configure/AppUnityIdeSO.cs ScriptableObject.
     *   - A /Assets/ImsRPG25/App/Configure/ImsAppUnityIde.asset Asset of that SO.
     *   - Now the ScriptableObject asset instance has access to the Unity
     *   event hooks for Awake(), OnEnable(), OnDisable() and OnDestroy() at
     *   runtime; with OnValidate() and Reset() when in the Unity IDE.
     *
     * - [ActionRequired] This ScriptableObject declaration-definition also
     *   needs to be created as Unity Asset instance, in project directories.
     *   - Exposing the AppSO class as configurable in a Unity IDE Inspector.
     *   - As a project asset, the AppUnityIdeSO class also gets instanciated
     *     as a Unity runtime object during gameplay; in Unity IDE Play Mode
     *     and in all compiled builds for target platforms.
     *   - Like declare as /Assets/ImsRPG25/App/Configure/ImsAppUnityIde.asset
     *     in 'Unity IDE' -> '/Assets/ImsRPG25/App/Configure/' directory by
     *     RCLK -> 'Create' menu
     *          -> 'Immersive Studios (GDK)' item
     *          -> 'Assets > Configure' item
     *          -> 'App Unity IDE (ScriptableObject)' item
     *     ... and keep the default filename suggestion of 'ImsAppUnityIde'.
     *   - Now the Unity game engine will declare an 'ImsAppUnityIde' named
     *     asset instanciance of the ImsGDK.AppUnityIdeSO class.
     *   - Now the Unity IDE Inspector can be used to configure and save the
     *     values in the /Assets/ImsRPG25/App/Configure/ImsAppUnityIde.asset
     *     file.
     *     - Default values come from definition in ImsGDK.AppUnityIdeSO
     *       class, but overwriten with Unity IDE Inspector values as an
     *       ImsAppUnityIde.asset.
     *   - The OnEnable() event hook seems to always be called when using this
     *     approach; both, in the Unity IDE Play Mode and a compiled build.
     *   - This approach does NOT require any relationship to a GameObject.
     *     - Most internet examples I could find about ScriptableObjects were
     *       all relying on a Scene GameObject instance to instanciate the
     *       ScriptableObject; but, we want class instanciation that is
     *       independent of the scene lifecycle that destroys GameObjects
     *       when switching scenes. AppUnityIdeSO should avoid scene chaos.
     *
     * @see   https://docs.unity3d.com/6000.2/Documentation/Manual/class-ScriptableObject.html
     * @see   https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/constructors
     * @since 0.0.1 Introduced.
     */
    public class AppUnityIdeSO : ScriptableObject
    {
        [Space(10)]
        [TextAreaAttribute(1,15)]
        [Tooltip("[UnityIDE] Add helpfull configuration hints here.\r\n\r\nOnly visible in Unity IDE Inspector.")]
        #pragma warning disable 0414
        [SerializeField] private string _notesForAllTeamMembers = "1. This AppUnityIdeSO ScriptableObject class will only action the below 'Input KeyCodes' within the Unity IDE; not available in compiled builds.";

//----------------------------------------------------------------------------

        // A static reference to 'this' instance for the static Get() method.
        // For static ImsGDK.AppUnityIdeSO.Get()._____ easy runtime access.
        private static ImsGDK.AppUnityIdeSO _appUnityIdeSO = null;

        /**
         * A static method for all classes to use ImsGDK.AppSO.Get().______ 
         * to access public members & methods of ImsGDK.AppUnityIdeSO class.
         *
         * @see   https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/static-classes-and-static-class-members
         * @since 0.0.1 Introduced.
         */
        public static ImsGDK.AppUnityIdeSO Get()
        {
            if (null == _appUnityIdeSO)
            {
                _appUnityIdeSO = new ImsGDK.AppUnityIdeSO();
            }
            return _appUnityIdeSO;
        }

        public AppUnityIdeSO ()
        {
#if UNITY_EDITOR
            if (null != _appUnityIdeSO)
            {
                Debug.Log($"[Warning] A ImsGDK.AppUnityIdeSO class second instance has been created. This is not desirable. Please, only create one asset instance of this ScriptableObject in the Unity project /Asset/ directories.");
            }
#endif
            // Last ImsGDK.AppUnityIdeSO class instanciated, wins.
            _appUnityIdeSO = this;
        }

//----------------------------------------------------------------------------
// - Unity ScriptableObject event hooks.

        // public void Awake()
        // {
        //     Debug.Log($"ImsGDK.AppUnityIdeSO as {name}.Awake() start.");
        // }

        public void OnEnable()
        {
            // Debug.Log($"ImsGDK.AppUnityIdeSO as {name}.OnEnable() start.");

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void OnDisable()
        {
            // Debug.Log($"ImsGDK.AppUnityIdeSO as {name}.OnDisable() start.");
            
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        // public void OnDestroy()
        // {
        //     Debug.Log($"ImsGDK.AppUnityIdeSO as {name}.OnDestroy() start.");
        // }

        // public void OnValidate()
        // {
        //     Debug.Log($"ImsGDK.AppUnityIdeSO as {name}.OnValidate() start.");
        // }

        // public void Reset()
        // {
        //     Debug.Log($"ImsGDK.AppUnityIdeSO as {name}.Reset() start.");
        // }

//----------------------------------------------------------------------------

#if UNITY_EDITOR
        [Header("UnityIDEPlay Input KeyCodes")]

        [Space(10)]
        [Tooltip("[UnityIDEPlay] (Q)uit game now.\r\n\r\nOnly works in Unity IDE Game Play mode.")]
        [SerializeField] private KeyCode _gameQuitKeycode = KeyCode.Q;

        [Space(10)]
        [Tooltip("[UnityIDEPlay] Load Game Menu now.\r\n\r\nOnly works in Unity IDE Game Play mode.")]
        [SerializeField] private KeyCode _loadGameMenuKeyCode = KeyCode.Alpha0;
        [Tooltip("[UnityIDEPlay] Load Level 1 scene now.\r\n\r\nOnly works in Unity IDE Game Play mode.")]
        [SerializeField] private KeyCode _loadLevel1KeyCode = KeyCode.Alpha1;
        [Tooltip("[UnityIDEPlay] Load Level 2 scene now.\r\n\r\nOnly works in Unity IDE Game Play mode.")]
        [SerializeField] private KeyCode _loadLevel2KeyCode = KeyCode.Alpha2;
        [Tooltip("[UnityIDEPlay] Load Level 3 scene now.\r\n\r\nOnly works in Unity IDE Game Play mode.")]
        [SerializeField] private KeyCode _loadLevel3KeyCode = KeyCode.Alpha3;
#endif

        /**
         * A scene has finished loading, now we can do scene post-processing.
         *
         * - See ImsGDK.AppUnityIdeSO.OnEnable() and ImsGDK.AppUnityIdeSO.OnDisable()
         *
         * @see    https://docs.unity3d.com/6000.2/Documentation/ScriptReference/SceneManagement.SceneManager-sceneLoaded.html
         * @since  0.0.1 Introduced.
         * @param  Scene         inScene The runtime data structure for a scene.
         * @param  LoadSceneMode inMode  The scene mode when loading into a player. Like 'Single' or 'Additive'.
         * @return void
         */
        public void OnSceneLoaded(Scene inScene, LoadSceneMode inMode)
        {
            // Debug.Log($"ImsGDK.AppUnityIdeSO as {name}.OnSceneLoaded({inScene.name}, {inMode}) start.");

            // Add ImsGDK.AppUnityIdeGO GameObject to Scene for EventSystem listening.
            AddToScene_AppUnityIdeGO();
        }

        /**
         * A scene has almost finished loading, now we can do scene post-processing.
         *
         * - Uses ImsGDK.AppUnityIdeGO GameObject and adds it as a child of EventSystem.
         *
         * @see    https://docs.unity3d.com/6000.2/Documentation/ScriptReference/SceneManagement.SceneManager-sceneLoaded.html
         * @see    https://docs.unity3d.com/ScriptReference/GameObject.html
         * @see    https://docs.unity3d.com/ScriptReference/GameObject-scene.html
         * @see    https://docs.unity3d.com/ScriptReference/GameObject.SetActive.html
         * @see    https://docs.unity3d.com/ScriptReference/GameObject-activeInHierarchy.html
         * @since  0.0.1 Introduced.
         * @return void
         */
        public void AddToScene_AppUnityIdeGO()
        {
            // Debug.Log($"ImsGDK.AppUnityIdeSO as {name}.AddToScene_AppUnityIdeGO() start.");

            // Guard for only one AppUnityIdeGO GameObject in the scene, ever.
            AppUnityIdeGO appUnityIdeGO = FindAnyObjectByType<AppUnityIdeGO>();
            if (null != appUnityIdeGO)
            {
                return;
            }

            // Guard for scene EventSystem GameObject must exist.
            UnityEngine.EventSystems.EventSystem eventSystemGO = FindAnyObjectByType<UnityEngine.EventSystems.EventSystem>();
            if (null == eventSystemGO)
            {
                // [Future] Consider adding <UnityEngine.EventSystems.EventSystem> to current scene?
                //          With a go.AddComponent<UnityEngine.EventSystems.EventSystem>();
                return;
            }

            // Yes, add an ImsGDK.AppUnityIdeGO GameObject to the scene.
            GameObject go = new GameObject("AppUnityIdeGO_Listener");
            go.AddComponent<AppUnityIdeGO>();
            go.transform.SetParent(eventSystemGO.transform, false);

            // Debug.Log($"ImsGDK.AppUnityIdeSO as {name}.AddToScene_AppUnityIdeGO() finish.");
        }

//----------------------------------------------------------------------------

        /**
         * Simulate Scene EventSystem hook into ImsGDK.AppUnityIdeSo class,
         * despite this being a global runtime ScriptableObject instance that
         * is decoupled from the Scene GameObject chaos.
         *
         * - Uses OnSceneLoaded() to dynamically add ImsGDK.AppUnityIdeGO
         *   GameObject instance when a new scene is loaded.
         *   - The dynamically added ImsGDK.AppUnityIdeGO GameObject instance
         *     is then a Scene -> EventSystem hook relayer to into
         *     ImsGDK.AppUnityIdeSO.DoUpdate() ... yeah, this method.
         *
         * @see    https://docs.unity3d.com/6000.2/Documentation/Manual/class-ScriptableObject.html
         * @see    https://docs.unity3d.com/6000.2/Documentation/Manual/working-with-gameobjects.html
         * @since  0.0.1 Introduced.
         * @return void
         */
        public void DoUpdate()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(_gameQuitKeycode))
            {
                ImsGDK.AppSO.Get().DoQuitNow();
                return;
            }

            if (Input.GetKeyDown(_loadGameMenuKeyCode))
            {
                ImsGDK.AppSO.Get().DoLoadScene(AppSO.imsAppScenes.gameMenu);
                return;
            }

            if (Input.GetKeyDown(_loadLevel1KeyCode))
            {
                ImsGDK.AppSO.Get().DoLoadScene(AppSO.imsAppScenes.level1);
                return;
            }

            if (Input.GetKeyDown(_loadLevel2KeyCode))
            {
                ImsGDK.AppSO.Get().DoLoadScene(AppSO.imsAppScenes.level2);
                return;
            }

            if (Input.GetKeyDown(_loadLevel3KeyCode))
            {
                ImsGDK.AppSO.Get().DoLoadScene(AppSO.imsAppScenes.level3);
                return;
            }
#endif
        }

//----------------------------------------------------------------------------

    }










//----------------------------------------------------------------------------

    public class AppUnityIdeGO : MonoBehaviour
    {
        private ImsGDK.AppUnityIdeSO _appUnityIdeSO = null;

        // Instance constructor for ImsGDK.AppUnityIdeSO ScriptableObject.
        public AppUnityIdeGO (ref ImsGDK.AppUnityIdeSO inAppUnityIdeSO)
        {
            _appUnityIdeSO = inAppUnityIdeSO;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created.
        public void Start()
        {
            // Debug.Log($"ImsGDK.AppUnityIdeGO as {name}.Start() start.");

            DoValidate();
        }

        // Update is called once per frame.
        public void Update()
        {
            // Debug.Log($"ImsGDK.AppUnityIdeGO as {name}.Update() start.");

            // Guard for ImsGDK.AppUnityIdeSO reference integrity.
            if (null == _appUnityIdeSO)
            {
                DoValidate();
                if (null == _appUnityIdeSO)
                {
                    return;
                }
            }

            // Simulate Scene EventSystem hook in ImsGDK.AppUnityIdeSo class.
            _appUnityIdeSO.DoUpdate();
        }

        public void DoValidate()
        {
            // Ensure ImsGDK.AppUnityIdeSO reference integrity.
            if (null == _appUnityIdeSO)
            {
                _appUnityIdeSO = ImsGDK.AppUnityIdeSO.Get();
            }
        }
    }
}
