using UnityEngine;
using UnityEngine.UI;

namespace ImsGDK    // 'Immersive Studios' -> 'Game Development Kit (GDK)'.
{
    public class CharacterTimer : CharacterResource
    {
        [Header("Regen Timer")]

        [SerializeField] private bool _regenTimerActive = true;
        [SerializeField] private float _regenTimer = 0;

        [Header("Regen Amount")]

        [Tooltip("+/- Regeneration Amount to add when Regeneration Timer reaches Regeneration Seconds.")]
        [SerializeField] private float _regenAmount = 1;

        [Header("Regen Seconds = 1 to 3600")]

        [Tooltip("Seconds to wait until Regeneration Timer adds the Regeneration Amount.\r\n\r\nMaximum: 3600 seconds (1 hour)")]
        [Range(1, 3600)]
        [SerializeField] private float _regenSeconds = 1;

        public bool regenTimerActive { get => _regenTimerActive; set => _regenTimerActive = value; }
        public float regenTimer  { get => _regenTimer; set => _regenTimer = value; }
        public float regenAmount  { get => _regenAmount; set => _regenAmount = value; }
        public float regenSeconds  { get => _regenSeconds; set => _regenSeconds = value; }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        override public void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        override public void Update()
        {
            // Defer DoShowNow() while still doing parent Update() first. OOP ancestor aware.
            bool wasShowNow = __doShowNow;
            __doShowNow = false;
            base.Update();
            __doShowNow = wasShowNow;

            if (_regenTimerActive)
            {
                // Increase regenTimer by deltaTimeElapsed since last frame loop.
                _regenTimer += Time.deltaTime;

                // Remember regenNow if regenTimer exceeds regenSeconds.
                if (_regenTimer >= _regenSeconds)
                {
                    // Force regenTimer close'ish to 0 and allow for minor deltaTimeElapsed variances.
                    _regenTimer -= _regenSeconds;

                    amount += _regenAmount;

                    DoValidate();
                }
            }

            // Show changes in gameplay experience. OOP descendant aware.
            if (__doShowNow)
            {
                DoShow();
            }
        }

        override public void DoValidate()
        {
            base.DoValidate();

            // regenSeconds must be >= 1 second. To play nice with regenerateTimer.
            _regenSeconds *= (0 > _regenSeconds) ? -1 : 1;
            _regenSeconds = (1 > _regenSeconds) ? 1 : _regenSeconds;

            // regenTimer is what it is. Do not touch.
        }
    }
}
