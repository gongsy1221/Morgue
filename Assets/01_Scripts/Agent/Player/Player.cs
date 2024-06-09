using System;
using UnityEngine;

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
    private bool isCheck = true;

    public PlayerStateMachine StateMachine { get; private set; }
    [SerializeField]
    private PlayerInput _playerInput;
    public PlayerInput PlayerInput => _playerInput;

    protected override void Awake()
    {
        base.Awake();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StateMachine = new PlayerStateMachine();

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isCheck)
        {
            if (other.CompareTag("Backward"))
            {
                if (!RoomManager.Instance.isSpecial)
                {
                    RoomManager.Instance.roomNumber = 1;
                    Debug.Log("B X");
                }
                else
                {
                    RoomManager.Instance.roomNumber++;
                    Debug.Log("B O");
                }

                isCheck = false;
                RoomManager.Instance.isForward = false;

                if (!RoomManager.Instance.isRoom)
                    RoomManager.Instance.PlayerCheck();
            }

            if (other.CompareTag("Forward"))
            {
                if (!RoomManager.Instance.isSpecial)
                {
                    RoomManager.Instance.roomNumber++;
                    Debug.Log("F O");
                }
                else
                {
                    RoomManager.Instance.roomNumber = 1;
                    Debug.Log("F X");
                }

                isCheck = false;
                RoomManager.Instance.isForward = true;

                if (!RoomManager.Instance.isRoom)
                    RoomManager.Instance.PlayerCheck();
            }
        }

        if (other.CompareTag("Room"))
        {
            isCheck = true;
            RoomManager.Instance.CheckInRoom();
        }
    }
}
