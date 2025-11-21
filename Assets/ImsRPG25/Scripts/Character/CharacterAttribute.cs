using UnityEngine;
using UnityEngine.UI;

namespace ImsGDK    // 'Immersive Studios' -> 'Game Development Kit (GDK)'.
{
    public class CharacterAttribute : CharacterResource
    {



        // [Do] Finish adding [Range()] & [Tooltip()] etc.
        // [Do] Add * Bonus to calculations.
        //      - [Do] Clamp 0 - 3.
        //      - [Do] if 0 make it 1.
        // [Do] Add * Difficulty to calculations.
        //      - [Do] Clamp 0 - 10.
        //      - [Do] if 0 make it 1.
        // [Do] Add [Range(Min, Max)] values as.
        //      - [Do] As private constants.
        //      - [Do] Enforce private constant min/max values in DoValidate().
        // [Do] Add Get() & Set() for public members.
        //      - [Do] Make them private, once wrapped in Get/Set.
        //      - [Do] Add DoValidate() when Set().
        // [Do] Add a read-only calculated field for editor experience only.
        //      - [Do] Add read-only 'Regen Amount Calculated' value for when '(Base + Offset) * Multiple * Resistance * Bonus * Difficulty' gets changed.
        //      - [Do] Add read-only 'Regen Seconds Calculated' value for when '(Base + Offset) * Multiple * Resistance * Bonus * Difficulty' gets changed.
        //      - [Do] Add read-only 'Regen Cooldown Calculated' value for when '(Base + Offset) * Multiple * Resistance * Bonus * Difficulty' gets changed.
        // [Do] Make private calculations truly read-only fields in the Inspector.
        //      - [Do] Will require the custom code for readonly fields in editor (Google it).



        [Header("Regen Timer")]

        [SerializeField] private bool _regenTimerActive = true;
        [SerializeField] private float _regenTimer = 0;

        [Header("Regen Amount = (Base + Offset) * Multiple * Resistance * Bonus * Difficulty")]

        [Tooltip("+/- Regeneration Amount to add when Regeneration Timer reaches Regeneration Seconds.\r\n\r\nMaximum: (99 + 99) * 10 * 10 * 3 * 10.\r\n\r\nCalculated. Do NOT Touch.")]
        [SerializeField] private float _regenAmount = 4;
        [Tooltip("+/- Regeneration Amount to add when Regeneration Timer reaches Regeneration Seconds.\r\n\r\nRange: -99f to +99f")]
        [Range(-99, 99)]
        public float regenAmountBase = 3;
        [Tooltip("+/- Regeneration Amount to add when Regeneration Timer reaches Regeneration Seconds.\r\n\r\nRange: -99f to +99f")]
        [Range(-99, 99)]
        public float regenAmountOffset = -1;
        [Tooltip("+/- Regeneration Amount to add when Regeneration Timer reaches Regeneration Seconds.\r\n\r\nRange: -10f to +10f")]
        [Range(-10, 10)]
        public float regenAmountMultiply = 2;
        [Tooltip("+/- Regeneration Amount to add when Regeneration Timer reaches Regeneration Seconds.\r\n\r\nRange: -10f to +10f")]
        [Range(-10, 10)]
        public float regenAmountResistance = 1;
        [Tooltip("+/- Regeneration Amount to add when Regeneration Timer reaches Regeneration Seconds.\r\n\r\nRange: -3f to +3f")]
        [Range(-3, 3)]
        public float regenAmountBonus = 1;
        [Tooltip("+/- Regeneration Amount to add when Regeneration Timer reaches Regeneration Seconds.\r\n\r\nRange: -10f to +10f")]
        [Range(-10, 10)]
        public float regenAmountDifficulty = 1;

        [Header("Regen Seconds = (Base + Offset) * Multiple * Resistance * Bonus * Difficulty")]

        [Tooltip("Seconds to wait until Regeneration Timer adds the Regeneration Amount.\r\n\r\nMaximum: (3600 + 3600) * 10 * 10 * 3 * 10.\r\n\r\nCalculated. Do NOT Touch.")]
        [SerializeField] private float _regenSeconds = 4;
        [Tooltip("+/- Seconds to wait until Regeneration Timer adds the Regeneration Amount.\r\n\r\nRange: -3600f to +3600f (1 hour)")]
        [Range(-3600, 3600)]
        public float regenSecondsBase = 3;
        [Tooltip("+/- Seconds to wait until Regeneration Timer adds the Regeneration Amount.\r\n\r\nRange: -3600f to +3600f (1 hour)")]
        [Range(-3600, 3600)]
        public float regenSecondsOffset = 0.2f;
        [Tooltip("+/- Seconds to wait until Regeneration Timer adds the Regeneration Amount.\r\n\r\nRange: -10f to +10f")]
        [Range(-10, 10)]
        public float regenSecondsMultiply = 1.25f;
        [Tooltip("+/- Seconds to wait until Regeneration Timer adds the Regeneration Amount.\r\n\r\nRange: -10f to +10f")]
        [Range(-10, 10)]
        public float regenSecondsResistance = 1;
        [Tooltip("+/- Seconds to wait until Regeneration Timer adds the Regeneration Amount.\r\n\r\nRange: -3f to +3f")]
        [Range(-3, 3)]
        public float regenSecondsBonus = 1;
        [Tooltip("+/- Seconds to wait until Regeneration Timer adds the Regeneration Amount.\r\n\r\nRange: -10f to +10f")]
        [Range(-10, 10)]
        public float regenSecondsDifficulty = 1;

        [Header("Regen Cooldown = (Base + Offset) * Multiple * Resistance * Bonus * Difficulty")]

        [Tooltip("Do Delay X Seconds before Regeneration Timer can start again.")]
        [SerializeField] private bool _regenCooldownActive = false;
        [Tooltip("Repeats for Delay X Seconds before Regeneration Timer can start again.\r\n\r\nRange: 0 to +99")]
        [Range(0, 99)]
        public int regenCooldownRepeat = 0;
        [Tooltip("Showing when a -ve Cooldown is blocking the Regeneration Timer from starting again.\r\n\r\nCalculated. Do NOT Touch.")]
        [SerializeField] private bool _regenCooldownDoing = false;
        [Tooltip("Delay X Seconds before Regeneration Timer can start again.\r\n\r\nMaximum: (3600 + 3600) * 10 * 10 * 3 * 10.\r\n\r\nCalculated. Do NOT Touch.")]
        [SerializeField] private float _regenCooldownSeconds = -3;
        [Tooltip("Delay X Seconds before Regeneration Timer can start again.\r\n\r\nRange: -3600f to +3600f (1 hour)")]
        [Range(-3600, 3600)]
        public float regenCooldownBase = -3;
        [Tooltip("Delay X Seconds before Regeneration Timer can start again.\r\n\r\nRange: -3600f to +3600f (1 hour)")]
        [Range(-3600, 3600)]
        public float regenCooldownOffset = 0;
        [Tooltip("Delay X Seconds before Regeneration Timer can start again.\r\n\r\nRange: -10f to +10f")]
        [Range(-10, 10)]
        public float regenCooldownMultiply = 1;
        [Tooltip("Delay X Seconds before Regeneration Timer can start again.\r\n\r\nRange: -10f to +10f")]
        [Range(-10, 10)]
        public float regenCooldownResistance = 1;
        [Tooltip("Delay X Seconds before Regeneration Timer can start again.\r\n\r\nRange: -3f to +3f")]
        [Range(-3, 3)]
        public float regenCooldownBonus = 1;
        [Tooltip("Delay X Seconds before Regeneration Timer can start again.\r\n\r\nRange: -10f to +10f")]
        [Range(-10, 10)]
        public float regenCooldownDifficulty = 1;

        private UnityEngine.UI.Image _canvasPlayHUDCharacterCooldownBar;

        public bool RegenTimerActive { get => _regenTimerActive; set => _regenTimerActive = value; }
        public bool RegenCooldownActive { get => _regenCooldownActive; set => _regenCooldownActive = value; }

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

                    // Apply regenCooldown if active and repeats are outstanding.
                    if (_regenCooldownActive
                     && 0 < regenCooldownRepeat
                    )
                    {
                        --regenCooldownRepeat;
                        _regenTimer += _regenCooldownSeconds;

                        // regenSeconds must be >= 1 second. To play nice with regenerateTimer.
                        float safetySeconds = _regenSeconds - 1;
                        _regenTimer = (_regenTimer >= safetySeconds) ? safetySeconds : _regenTimer;
                    }

                    // Increase amount by regenAmount.
                    amount += _regenAmount;

                    DoValidate();
                    // doShowNow = true;
                }

                if (_regenCooldownActive
                 && amount < amountMaximum
                )
                {
                    // Only show -ve cooldown while it is happening.
                    // if (_regenCooldownDoing)
                    // {
                    //     doShowNow = (null != _canvasPlayHUDCharacterCooldownBar) ? true : doShowNow;
                    // }
                    // else
                    if (!_regenCooldownDoing)
                    {
                        if (0 > _regenCooldownSeconds
                         && 0 > _regenTimer
                        )
                        {
                            _regenCooldownDoing = true;
                            // doShowNow = (null != _canvasPlayHUDCharacterCooldownBar) ? true : doShowNow;
                        }
                    }
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

            // regenAmountBase is allowed to be +ve or -ve.
            // regenAmountOffset is allowed to be +ve or -ve.
            // regenAmountMultiply is allowed to be +ve or -ve.
            regenAmountMultiply = (0 == regenAmountMultiply) ? 1 : regenAmountMultiply;
            // regenAmountResistance is allowed to be +ve or -ve.
            regenAmountResistance = (0 == regenAmountResistance) ? 1 : regenAmountResistance;
            // regenAmountBonus is allowed to be +ve or -ve.
            regenAmountBonus = (0 == regenAmountBonus) ? 1 : regenAmountBonus;
            // regenAmountDifficulty is allowed to be +ve or -ve.
            regenAmountDifficulty = (0 == regenAmountDifficulty) ? 1 : regenAmountDifficulty;

            // regenAmount is allowed to be +ve or -ve.
            _regenAmount = regenAmountBase + regenAmountOffset;
            _regenAmount *= regenAmountMultiply;
            _regenAmount *= regenAmountResistance;
            _regenAmount *= regenAmountBonus;
            _regenAmount *= regenAmountDifficulty;

            // regenSecondsBase is allowed to be +ve or -ve.
            // regenSecondsOffset is allowed to be +ve or -ve.
            // regenSecondsMultiply is allowed to be +ve or -ve.
            regenSecondsMultiply = (0 == regenSecondsMultiply) ? 1 : regenSecondsMultiply;
            // regenSecondsResistance is allowed to be +ve or -ve.
            regenSecondsResistance = (0 == regenSecondsResistance) ? 1 : regenSecondsResistance;
            // regenSecondsBonus is allowed to be +ve or -ve.
            regenSecondsBonus = (0 == regenSecondsBonus) ? 1 : regenSecondsBonus;
            // regenSecondsDifficulty is allowed to be +ve or -ve.
            regenSecondsDifficulty = (0 == regenSecondsDifficulty) ? 1 : regenSecondsDifficulty;

            // regenSeconds must be >= 1 second. To play nice with regenerateTimer.
            _regenSeconds = regenSecondsBase + regenSecondsOffset;
            _regenSeconds *= regenSecondsMultiply;
            _regenSeconds *= regenSecondsResistance;
            _regenSeconds *= regenSecondsBonus;
            _regenSeconds *= regenSecondsDifficulty;
            _regenSeconds *= (0 > _regenSeconds) ? -1 : 1;
            _regenSeconds = (1 > _regenSeconds) ? 1 : _regenSeconds;

            regenCooldownRepeat = (0 > regenCooldownRepeat) ? 0 : regenCooldownRepeat;
            regenCooldownRepeat = (99 < regenCooldownRepeat) ? 99 : regenCooldownRepeat;
            // regenCooldownBase is allowed to be +ve or -ve.
            // regenCooldownOffset is allowed to be +ve or -ve.
            // regenCooldownMultiply is allowed to be +ve or -ve.
            regenCooldownMultiply = (0 == regenCooldownMultiply) ? 1 : regenCooldownMultiply;
            // regenCooldownResistance is allowed to be +ve or -ve.
            regenCooldownResistance = (0 == regenCooldownResistance) ? 1 : regenCooldownResistance;
            // regenCooldownBonus is allowed to be +ve or -ve.
            regenCooldownBonus = (0 == regenCooldownBonus) ? 1 : regenCooldownBonus;
            // regenCooldownDifficulty is allowed to be +ve or -ve.
            regenCooldownDifficulty = (0 == regenCooldownDifficulty) ? 1 : regenCooldownDifficulty;

            // regenCooldownSeconds is allowed to be +ve or -ve.
            _regenCooldownSeconds = regenCooldownBase + regenCooldownOffset;
            _regenCooldownSeconds *= regenCooldownMultiply;
            _regenCooldownSeconds *= regenCooldownResistance;
            _regenCooldownSeconds *= regenCooldownBonus;
            _regenCooldownSeconds *= regenCooldownDifficulty;

            // regenTimer is what it is. Do not touch.
        }

        override public void DoOnce()
        {
            base.DoOnce();

            // Find Character cooldown image bar in HUD ... only once.
            if (null == _canvasPlayHUDCharacterCooldownBar)
            {
                GameObject cooldownGO = GameObject.Find($"{uiHudGO_Prefix}{name}CooldownBar");
                if (null != cooldownGO)
                {
                    _canvasPlayHUDCharacterCooldownBar = cooldownGO.GetComponent<UnityEngine.UI.Image>() as UnityEngine.UI.Image;

                    // Force 1st show because of conditional optimisation in DoShow().
                    DoShowCooldownBar(0);
                }
            }
        }

        override public void DoShow()
        {
            base.DoShow();

            // Only show -ve cooldown while it is happening.
            if (_regenCooldownDoing
             && 0 > _regenCooldownSeconds
             && null != _canvasPlayHUDCharacterCooldownBar
            )
            {
                float cooldownFill = _regenTimer;
                if (0 > _regenTimer)
                {
                    cooldownFill /= _regenCooldownSeconds;
                }
                else
                {
                    cooldownFill = 0;
                    _regenCooldownDoing = false;
                }
                DoShowCooldownBar(cooldownFill);
            }
        }

        virtual public void DoShowCooldownBar(float inValue01 = 0)
        {
            if (null != _canvasPlayHUDCharacterCooldownBar)
            {
                _canvasPlayHUDCharacterCooldownBar.fillAmount = Mathf.Clamp01(inValue01);
            }
        }
    }
}
