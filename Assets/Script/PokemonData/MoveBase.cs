using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "CreateDataBase/Move")]
public class MoveBase : ScriptableObject
{
    [SerializeField] int moveId;
    [SerializeField] string moveName;
    [SerializeField] int maxPP;
    [SerializeField] int initialPP;
    [SerializeField] TypeEnum moveType;
    [TextArea]
    [SerializeField] string moveDesc;
    [SerializeField] int power;
    [SerializeField] int accuracy;
    [SerializeField] int priority;
    [SerializeField] MoveATKRangeEnum moveATKRangeEnum = MoveATKRangeEnum.Single;//��������
    [SerializeField] MoveContactEnum moveContactEnum = MoveContactEnum.None;//�Ӵ�����
    [SerializeField] MoveTypeEnum moveTypeEnum = MoveTypeEnum.Physical_Move;//��������
    [SerializeField] bool bIsEffectByProtect = false;//�Ƿ�����סӰ��
    [SerializeField] bool bIsEffectByMagicCoat = false;//�Ƿ���ħ������Ӱ��
    [SerializeField] bool bIsEffectBySnatch = false;//�Ƿ���Ա�����
    [SerializeField] bool bIsEffectByMirror = false;//�Ƿ�������ѧ��Ӱ��
    [SerializeField] bool bIsEffectByItem = false;//�Ƿ�������֤֮�ȵ���Ӱ��

    public int MoveId { get => moveId; }
    public string MoveName { get => moveName; }
    public int MaxPP { get => maxPP; }
    public int InitialPP { get => initialPP; }
    public TypeEnum MoveType { get => moveType; }
    public string MoveDesc { get => moveDesc; }
    public int Power { get => power; }
    public int Accuracy { get => accuracy; }
    public int Priority { get => priority; }
    public MoveATKRangeEnum MoveATKRangeEnum { get => moveATKRangeEnum; }
    public MoveContactEnum MoveContactEnum { get => moveContactEnum; }
    public MoveTypeEnum MoveTypeEnum { get => moveTypeEnum; }
    public bool BIsEffectByProtect { get => bIsEffectByProtect; }
    public bool BIsEffectByMagicCoat { get => bIsEffectByMagicCoat; }
    public bool BIsEffectBySnatch { get => bIsEffectBySnatch; }
    public bool BIsEffectByMirror { get => bIsEffectByMirror; }
    public bool BIsEffectByItem { get => bIsEffectByItem; }
}
