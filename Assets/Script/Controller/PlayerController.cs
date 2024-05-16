using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    /*game�ֶ�*/
    private Vector2 m_Input;//�������
    public LayerMask BuildingLayer;//������
    public LayerMask GrassLayer;//ǳ�ݲ�
    public LayerMask DeepGrassLayer;
    private PlayerAnimator playerAnimator;//��Ҷ�������
    private bool isMoving = false;//�Ƿ�����ƶ�

    /*�������*/
    public float moveSpeed = 6;//����ƶ��ٶ�
    private Vector3 vector3 = new Vector3(0, 0, 0);//�������
    private MoveDirection playerCurDir = MoveDirection.Down;//��ҳ���
    private PlayerStateEnum playerState = PlayerStateEnum.Normal;//���״̬
    private PlayerDataStore playerInfo;//�����Ϣ��TODO������������������Ϣ��װ��playerInfo��

    /*������*/
    public MoveDirection PlayerCurDir { get => playerCurDir; set => playerCurDir = value; }
    internal PlayerStateEnum PlayerState { get => playerState; }

    private void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        playerInfo = new PlayerDataStore();
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = vector3;
        this.SetOrder();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            this.ToMove();
        }
        else
        {

        }
    }

    private void ToMove()
    {
        if (!isMoving && PlayerState == PlayerStateEnum.Normal)
        {
            m_Input.x = Input.GetAxisRaw("Horizontal");
            m_Input.y = Input.GetAxisRaw("Vertical");
            if (m_Input != Vector2.zero)
            {
                Debug.Log("ִ��ToMove" + m_Input.x + " " + m_Input.y);
                if (m_Input.x != 0)
                {
                    m_Input.y = 0;
                }
                playerAnimator.ChangeMoveX(m_Input.x);
                playerAnimator.ChangeMoveY(m_Input.y);
                var targetPos = transform.position;
                targetPos.x += m_Input.x;
                targetPos.y += m_Input.y;
                if (m_Input.Equals(Vector2.zero))
                {
                    return;
                }
                playerState = PlayerStateEnum.Busy;
                if (CheckSameDirWithNextMove(m_Input))
                {
                    isMoving = true;
                    if (CheckWalkable(targetPos) && CommonUtils.CheckIsReachable(targetPos))
                        Move(targetPos).Forget();
                    else
                    /*TODO:�����ƶ�Ҳ�ܲ�����·����*/
                    {
                        playerState = PlayerStateEnum.Normal;
                        isMoving = false;
                    }
                }
                else
                {
                    Debug.Log("change face dir");
                    ChangeFaceDir(m_Input).Forget();
                }
            }
            else
            {
                isMoving = false;
            }
        }
        playerAnimator.ChangeisMoving(isMoving);
    }

    public async UniTaskVoid ChangeFaceDir(Vector2 pos)
    {
        if (pos.x == 1)
        {
            playerCurDir = MoveDirection.Right;
            playerAnimator.SetRoleFaceDir(MoveDirection.Right);
        }
        if (pos.x == -1)
        {
            playerCurDir = MoveDirection.Left;
            playerAnimator.SetRoleFaceDir(MoveDirection.Left);
        }
        if (pos.y == -1)
        {
            playerCurDir = MoveDirection.Down;
            playerAnimator.SetRoleFaceDir(MoveDirection.Down);
        }
        if (pos.y == 1)
        {
            playerCurDir = MoveDirection.Up;
            playerAnimator.SetRoleFaceDir(MoveDirection.Up);
        }
        await UniTask.DelayFrame(50);
        isMoving = false;
        playerState = PlayerStateEnum.Normal;
    }

    public bool CheckSameDirWithNextMove(Vector2 pos)
    {
        Debug.Log("******Check Same" + pos + "******CurDir" + playerCurDir);
        if (pos.x == 1 && playerCurDir == MoveDirection.Right)
        {
            return true;
        }
        if (pos.x == -1 && playerCurDir == MoveDirection.Left)
        {
            return true;
        }
        if (pos.y == 1 && playerCurDir == MoveDirection.Up)
        {
            return true;
        }
        if (pos.y == -1 && playerCurDir == MoveDirection.Down)
        {
            return true;
        }
        return false;
    }
    async UniTaskVoid Move(Vector3 targetPos)
    {
        Debug.Log("moveTargetPos��" + targetPos);
        CommonUtils.SaveCharacterPos(playerInfo.PlayerId, targetPos);
        while ((targetPos - transform.position).sqrMagnitude > 0.00001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            await UniTask.Yield();
            await UniTask.NextFrame();
        }
        Debug.Log("Player MoveOver curPos" + transform.position + "**" + targetPos);
        transform.position = targetPos;
        CommonUtils.CorrectPosition(transform.position);
        isMoving = false;
        playerState = PlayerStateEnum.Normal;
        this.SetOrder();
        CheckForEncounters();

    }

    public bool CheckWalkable(Vector3 targetPos)
    {
        //���ﴦ�����Ǻܺã����з���Tile��collider��Ŀǰ�޷��ֶ�����collider��Χ ���ְ��TODO���Ż����/�滻collider��
        if (Physics2D.OverlapCircle(targetPos, 0.15f, BuildingLayer) != null)
        {
            return false;
        }
        return true;
    }

    public void CheckForEncounters()
    {
        Debug.Log("��ӡ������ս��");
        //���ﴦ�����Ǻܺã����з���Tile��collider��Ŀǰ�޷��ֶ�����collider��Χ ���ְ��TODO���Ż����/�滻collider��
        if (Physics2D.OverlapCircle(transform.position, 0.15f, GrassLayer) != null)
        {
            if (Random.Range(1, 200) > 30)
            {
                if (Random.Range(1, 10) > 7)
                    TurnToBatlle(BattleTypeEnum.Wild, BattleNumTypeEnum.Multiple);
                else
                    TurnToBatlle(BattleTypeEnum.Wild, BattleNumTypeEnum.Single);
            }
        }
        if (Physics2D.OverlapCircle(transform.position, 0.15f, DeepGrassLayer) != null)
        {
            if (Random.Range(1, 200) > 30)
            {
                var battleTypeRandom = Random.Range(1, 10);
                if (battleTypeRandom > 8)
                    TurnToBatlle(BattleTypeEnum.Wild, BattleNumTypeEnum.Multiple);
                else if (battleTypeRandom > 4)
                {
                    TurnToBatlle(BattleTypeEnum.Wild, BattleNumTypeEnum.Double);
                }
                else
                    TurnToBatlle(BattleTypeEnum.Wild, BattleNumTypeEnum.Single);
            }
        }
    }

    public void TurnToBatlle(BattleTypeEnum battleTypeEnum, BattleNumTypeEnum battleNumTypeEnum)
    {
        Debug.Log("Player Enter Battle. BattleType" + battleTypeEnum);
        playerState = PlayerStateEnum.Battle;
    }

    public void SetOrder()
    {
        var spriteRender = gameObject.GetComponent<SpriteRenderer>();
        spriteRender.sortingOrder = Mathf.FloorToInt(transform.position.y);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.15f);
    }

}
