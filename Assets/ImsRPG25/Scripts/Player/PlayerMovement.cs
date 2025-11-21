namespace ImsGDK    // 'Immersive Studios' -> 'Game Development Kit (GDK)'.
{
    using UnityEngine;

    [RequireComponent(typeof(CharacterController))]
    [AddComponentMenu("Immersive Studios (GDK)/Component > Player/Movement")]
    public class PlayerMovement : MonoBehaviour
    {
        public enum Kind : int
        {
            rest,
            prone,
            crouch,
            walk,
            jog,
            sprint,
            hunted
        }

        // Use for _moveSpeed = _speed[_moveKind].
        [SerializeField] private Kind _moveKind = Kind.walk;
        private Kind _moveKindLastFrame = Kind.walk;
        [SerializeField] private float[] _speed = {0, 1, 2.5f, 5, 7.5f, 10, 15};

        public KeyCode crouchKeyCode = KeyCode.LeftControl;
        public KeyCode sprintKeyCode = KeyCode.LeftShift;

        Vector3 _movementDirection = Vector3.zero;
        CharacterController _characterController;

        [Header("Speed That The Player Moves")]
        [SerializeField] float _moveSpeed;
        [SerializeField] float _jumpSpeed = 10;
        [SerializeField] float _gravity = 20;

        private CharacterAttribute _playerStaminaCharacterAttribute;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            // Dynamically get reference to GameObject -> Character Controller component.
            _characterController = this.GetComponent<CharacterController>();


            // Find matching character attribute image bar in HUD ... only once.
            if (null == _playerStaminaCharacterAttribute)
            {
                GameObject go = GameObject.Find("/Player/Attribute/Stamina");
                if (null != go)
                {
                    _playerStaminaCharacterAttribute = go.GetComponent<ImsGDK.CharacterAttribute>() as ImsGDK.CharacterAttribute;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (_characterController.isGrounded)
            {
                // [Optional] Dynamic InputManager ... if (Input.GetKey("Sprint")) {}
                // - Did you configure "Sprint" in Project Settings -> Input Manager?
                // Hardcoded ... if (Input.GetKey(KeyCode.LeftShift)) {}

                // Allow latest key press to become the active movement kind.
                if (Input.GetKeyDown(sprintKeyCode))
                {
                    _moveKind = Kind.sprint;
                }
                else if (Input.GetKeyDown(crouchKeyCode))
                {
                    _moveKind = Kind.crouch;
                }

                // Are we still holding the respective movement kind key down?
                if (Kind.sprint == _moveKind
                 && !Input.GetKey(sprintKeyCode)
                ) {
                    _moveKind = Kind.walk;
                    // Should we toggle back to the other movement kind (sprint vs crouch)?
                    if (Input.GetKey(crouchKeyCode))
                    {
                        _moveKind = Kind.crouch;
                    }
                }
                else if (Kind.crouch == _moveKind
                      && !Input.GetKey(crouchKeyCode)
                ) {
                    _moveKind = Kind.walk;
                    // Should we toggle back to the other movement kind (sprint vs crouch)?
                    if (Input.GetKey(sprintKeyCode))
                    {
                        _moveKind = Kind.sprint;
                    }
                }

                // Only Sprint when we have Stamina.
                if (null != _playerStaminaCharacterAttribute)
                {
                    // Is there any Stamina to Sprint?
                    // Is this the LAST frame doing Sprinting?
                    if ((Kind.sprint == _moveKind
                      && 0 >= _playerStaminaCharacterAttribute.amount)
                     || (Kind.sprint != _moveKind
                      && Kind.sprint == _moveKindLastFrame)
                    )
                    {
                        _playerStaminaCharacterAttribute.regenAmountResistance = 0;
                        _playerStaminaCharacterAttribute.regenSecondsResistance = 0;
                        _playerStaminaCharacterAttribute.DoValidate();
                        if (Kind.sprint == _moveKind)
                        {
                            _moveKind = Kind.walk;
                        }
                    }
                    // Is this the FIRST frame doing Sprinting?
                    else if (Kind.sprint == _moveKind
                            && Kind.sprint != _moveKindLastFrame
                    )
                    {
                        _playerStaminaCharacterAttribute.regenAmountResistance = -0.1f;
                        _playerStaminaCharacterAttribute.regenSecondsResistance = 0.1f;
                        _playerStaminaCharacterAttribute.DoValidate();
                    }
                }

                _moveSpeed = _speed[(int)_moveKind];
                _moveKindLastFrame = _moveKind;

                // SET movementDirection To Both Horizontal Vertical INPUT
                _movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                _movementDirection *= _moveSpeed;
                _movementDirection = transform.TransformDirection(_movementDirection);
                if (Input.GetButton("Jump"))
                {
                    _movementDirection.y = _jumpSpeed;
                }
            }
            _movementDirection.y -= _gravity * Time.deltaTime;
            _characterController.Move(_movementDirection * Time.deltaTime);
        }
    }
}
