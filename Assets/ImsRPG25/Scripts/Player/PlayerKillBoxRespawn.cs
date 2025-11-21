using UnityEngine;
using System.Collections.Generic;

namespace ImsGDK    // 'Immersive Studios' -> 'Game Development Kit (GDK)'.
{
    [RequireComponent(typeof(CharacterController))]
    [AddComponentMenu("Immersive Studios (GDK)/Component > Player/World Kill Box to Force Respawn")]
    public class PlayerKillBoxRespawn : MonoBehaviour
    {
        private bool _ok = true;
        protected Transform _playerTerrainSpawnTransform;
        protected Transform _playerTransform;
        protected CharacterController _playerCharacterController;

        [Tooltip("Offset for World Kill Box (WKB) limits.")]
        [Range(0, 99)]
        [SerializeField] private float _worldKillBoxOffset = 9;
        [Space(10)]
        [Tooltip("World boundary limit to auto-kill characters.\r\n\r\n[ReadOnly] A runtime calculated field.")]
        [SerializeField] private float _wkbXLeft;
        [Tooltip("World boundary limit to auto-kill characters.\r\n\r\n[ReadOnly] A runtime calculated field.")]
        [SerializeField] private float _wkbXRight;
        [Tooltip("World boundary limit to auto-kill characters.\r\n\r\n[ReadOnly] A runtime calculated field.")]
        [SerializeField] private float _wkbYTop;
        [Tooltip("World boundary limit to auto-kill characters.\r\n\r\n[ReadOnly] A runtime calculated field.")]
        [SerializeField] private float _wkbYBottom;
        [Tooltip("World boundary limit to auto-kill characters.\r\n\r\n[ReadOnly] A runtime calculated field.")]
        [SerializeField] private float _wkbZFront;
        [Tooltip("World boundary limit to auto-kill characters.\r\n\r\n[ReadOnly] A runtime calculated field.")]
        [SerializeField] private float _wkbZBack;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            // Debug.Log($"ImsGDK.PlayerKillBoxRespawn as {name}.Start() start.");

            DoValidate();
        }

        // Update is called once per frame
        void Update()
        {
            if (!_ok) return;

            if (IsOutsideWorldKillBox(ref _playerTransform))
            {
                DoPlayerRespawn();
            }

        }

        public void DoValidate()
        {
            // Get Player gameobject for respawn.
            GameObject playerGO = GameObject.Find("Player");
            if (null == playerGO)
            {
                Debug.LogError($"ImsGDK.PlayerKillBoxRespawn as {name}.DoValidate() says Scene missing Player prefab.");
                _ok = false;
                ImsGDK.AppSO.Get().DoQuitNow();
                return;
            }
            _playerTransform = playerGO.transform;

            // Get Player character controller for respawn.
            _playerCharacterController = this.GetComponent<CharacterController>();
            if (null == _playerCharacterController)
            {
                Debug.LogError($"ImsGDK.PlayerKillBoxRespawn as {name}.DoValidate() says Player missing CharacterController component.");
                _ok = false;
                ImsGDK.AppSO.Get().DoQuitNow();
                return;
            }

            // Move player to starting spawn position.
            GameObject playerSpawnGO = GameObject.Find("PlayerSpawn");
            if (null == playerSpawnGO)
            {
                Debug.LogError($"ImsGDK.PlayerKillBoxRespawn as {name}.DoValidate() says Scene missing PlayerSpawn gameobject.");
                _ok = false;
                ImsGDK.AppSO.Get().DoQuitNow();
                return;
            }
            _playerTerrainSpawnTransform = playerSpawnGO.transform;
            this.transform.position = _playerTerrainSpawnTransform.position;

            //
            // [Future] This DoValidate() needs a dynamic trigger whenever a
            //          new Terrain gets added to any loaded scene; to force
            //          a world limit recalculation. But, that is a problem
            //          for more complicated games that do additive scenes for
            //          an endless world that does distant terrain management.
            //

            // Get a list of all active Terrains in the scene.
            List<Terrain> allTerrains = new List<Terrain>();
            Terrain.GetActiveTerrains(allTerrains);
            if (allTerrains.Count <= 0)
            {
                Debug.LogError($"ImsGDK.PlayerKillBoxRespawn as {name}.DoValidate() says Scene missing Terrain gameobject.");
                _ok = false;
                ImsGDK.AppSO.Get().DoQuitNow();
                return;
            }

            // Find world limits from all terains.
            for (int i = allTerrains.Count - 1; i >= 0; i--)
            {
                // Debug.Log($"ImsGDK.PlayerKillBoxRespawn as {name}.DoValidate() says '{allTerrains[i].name}' terrain is active.");

                Vector3 worldAxisMin = allTerrains[i].terrainData.bounds.min;
                Vector3 worldAxisMax = allTerrains[i].terrainData.bounds.max;

                if (0 == i) {
                    // Set World Kill Box (WKB) axis limits to be a terrain.
                    _wkbXLeft = worldAxisMax.x;
                    _wkbXRight = worldAxisMin.x;
                    _wkbYTop = worldAxisMax.y;
                    _wkbYBottom = worldAxisMin.y;
                    _wkbZFront = worldAxisMax.z;
                    _wkbZBack = worldAxisMin.z;
                }
                else
                {
                    // Expand World Kill Box (WKB) to include all terrains.
                    _wkbXLeft = (worldAxisMax.x > _wkbXLeft) ? worldAxisMax.x : _wkbXLeft;
                    _wkbXRight = (worldAxisMin.x < _wkbXRight) ? worldAxisMin.x : _wkbXRight;

                    _wkbYTop = (worldAxisMax.y > _wkbYTop) ? worldAxisMax.y : _wkbYTop;
                    _wkbYBottom = (worldAxisMin.y < _wkbYBottom) ? worldAxisMin.y : _wkbYBottom;

                    _wkbZFront = (worldAxisMax.z > _wkbZFront) ? worldAxisMax.z : _wkbZFront;
                    _wkbZBack = (worldAxisMin.z < _wkbZBack) ? worldAxisMin.z : _wkbZBack;
                }
            }

            // Ensure World Kill Box Offset is a positive number.
            _worldKillBoxOffset *= (0 > _worldKillBoxOffset) ? -1 : 1;

            // Adjust world limits by KillBoxOffset.
            _wkbXLeft += _worldKillBoxOffset;
            _wkbXRight -= _worldKillBoxOffset;
            _wkbYTop += _worldKillBoxOffset;
            _wkbYBottom -= _worldKillBoxOffset;
            _wkbZFront += _worldKillBoxOffset;
            _wkbZBack -= _worldKillBoxOffset;

            _ok = true;
        }

        protected bool IsOutsideWorldKillBox(ref Transform inTransform)
        {
            // Debug.Log($"ImsGDK.PlayerKillBoxRespawn as {name}.IsOutsideWorldKillBox() character at X:{inTransform.position.x} Y:{inTransform.position.y} Z:{inTransform.position.z}");

            // Is Character outside World Kill Box (WKB) for all terrains?
            if (_wkbXLeft < inTransform.position.x
             || _wkbXRight > inTransform.position.x
             || _wkbYTop < inTransform.position.y
             || _wkbYBottom > inTransform.position.y
             || _wkbZFront < inTransform.position.z
             || _wkbZBack > inTransform.position.z
            )
            {
                return true;
            }
            return false;
        }

        protected void DoPlayerRespawn()
        {
            // Move player to terrain player spawn position.
            _playerCharacterController.enabled = false;
            _playerTransform.position = _playerTerrainSpawnTransform.position;
            _playerCharacterController.enabled = true;
        }
    }
}