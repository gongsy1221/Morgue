using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerStateEnum
{
    Idle,
    Walk,
    Run,
    Jump,
    Fall,
    Die,
    Sit
}

public class Player : Agent
{
    [Header("Setting value")]
    public float WalkSpeed = 2f;
    public float RunSpeed = 4f;
    public float SitSpeed = 2f;
    public float JumpPower = 5f;

    public float JumpMaxTime = 1;
    public float CurrentSpeed = 0;
    public bool isDead;
    public bool isMove;
    private bool isCheck = true;

    public PlayerStateMachine StateMachine { get; private set; }
    public SoundPlayer soundPlayer { get; private set; }
    [SerializeField]
    private PlayerInput _playerInput;
    private CameraControl _cameraControl;
    private PlayerUI _playerUI;
    public PlayerInput PlayerInput => _playerInput;

    protected override void Awake()
    {
        base.Awake();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StateMachine = new PlayerStateMachine();
        soundPlayer = GetComponent<SoundPlayer>();

        foreach (PlayerStateEnum stateEnum in Enum.GetValues(typeof(PlayerStateEnum)))
        {
            string typeName = stateEnum.ToString();

            try
            {
                Type t = Type.GetType($"Player{typeName}State");
                PlayerState state = Activator.CreateInstance(t, this, StateMachine, typeName) as PlayerState;

                StateMachine.AddState(stateEnum, state);
            }
            catch (Exception e)
            {
                Debug.LogError(typeName + "\n");
                Debug.LogError(e.Message);
            }
        }

        _cameraControl = GetComponentInChildren<CameraControl>();
        _playerUI = GetComponent<PlayerUI>();
    }

    protected void Start()
    {
        StateMachine.Initalize(PlayerStateEnum.Idle, this);
    }

    private void Update()
    {
        StateMachine.CurrentState.UpdateState();
        if (isDead)
        {
            StateMachine.ChangeState(PlayerStateEnum.Die);
        }
    }

    public void PlayerDie()
    {
        isDead = true;
        _cameraControl.EnableRotation(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isCheck)
        {
            if (other.CompareTag("Backward"))
            {
                if (!RoomManager.Instance.isSpecial)
                    RoomManager.Instance.roomNumber = 1;
                else
                    RoomManager.Instance.roomNumber++;

                isCheck = false;
                RoomManager.Instance.isForward = false;

                if (!RoomManager.Instance.isRoom)
                    RoomManager.Instance.PlayerCheck();
            }

            if (other.CompareTag("Forward"))
            {
                if (!RoomManager.Instance.isSpecial)
                    RoomManager.Instance.roomNumber++;
                else
                    RoomManager.Instance.roomNumber = 1;

                isCheck = false;
                RoomManager.Instance.isForward = true;

                if (!RoomManager.Instance.isRoom)
                    RoomManager.Instance.PlayerCheck();
            }

            _playerUI.UpdateRoomText();
        }

        if (other.CompareTag("Room"))
        {
            isCheck = true;
            RoomManager.Instance.CheckInRoom();
        }

        if(other.CompareTag("Last"))
        {
            //SceneManager.LoadScene("03_LastScene");
            StartCoroutine(FadeManager.Instance.FadeIn());
            SceneManager.LoadScene(0);
            Debug.Log("Game End");
        }
    }
}
