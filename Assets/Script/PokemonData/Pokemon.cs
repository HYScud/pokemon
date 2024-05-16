using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{
    private PokemonBase pkBase { get; }

    //������Ϣ
    private int level;
    private long Id;//ʵ�ʵ�id��>ÿ�������ζ���ͬ
    private long ownerId = 0;
    private int catchArea = 0;
    private CatchTypeEnum catchTypeEnum = CatchTypeEnum.None;
    private string nickName = "";
    private NatrueTypeEnum natrueTypeEnum { get; set; }
    //Ŭ��ֵ
    [SerializeField] int BasePoints_HP = 0;
    [SerializeField] int BasePoints_ATk = 0;
    [SerializeField] int BasePoints_SPATk = 0;
    [SerializeField] int BasePoints_Def = 0;
    [SerializeField] int BasePoints_SPDef = 0;
    [SerializeField] int BasePoints_Speed = 0;

    //���ܶ�-�����ȵ�
    [SerializeField] int BasePoints_Friendship = 0;
    [SerializeField] int BasePoints_Beauty = 0;
    [SerializeField] int BasePoints_Cute = 0;
    [SerializeField] int BasePoints_Smart = 0;
    [SerializeField] int BasePoints_Cool = 0;
    [SerializeField] int BasePoints_Tough = 0;

    //��������-����ֵ
    [SerializeField] int IV_Hp = 0;
    [SerializeField] int IV_Attack = 0;
    [SerializeField] int IV_Defence = 0;
    [SerializeField] int IV_SpecialATK = 0;
    [SerializeField] int IV_SpecialDef = 0;
    [SerializeField] int IV_Speed = 0;

    //ս������
    public int CurHp { get; set; }

    public int MaxHp { get { if (pkBase.PokemonId == 292) { return 1; } else return Mathf.FloorToInt(((pkBase.Hp * 2 + IV_Hp + BasePoints_HP / 4) * level) / 100f) + 10 + level; } }
    public int Attack { get { return Mathf.FloorToInt((((pkBase.Attack * 2 + IV_Attack + BasePoints_ATk / 4) * level) / 100f + 5 + level) * PokemonTable.GetNatureEffect(2, natrueTypeEnum)); } }
    public int Defense { get { return Mathf.FloorToInt((((pkBase.Defence * 2 + IV_Defence + BasePoints_SPATk / 4) * level) / 100f + 5 + level) * PokemonTable.GetNatureEffect(2, natrueTypeEnum)); } }
    public int SpecailAttack { get { return Mathf.FloorToInt((((pkBase.SpecialATK * 2 + IV_SpecialATK + BasePoints_Def / 4) * level) / 100f + 5 + level) * PokemonTable.GetNatureEffect(2, natrueTypeEnum)); } }
    public int SpecialDefense { get { return Mathf.FloorToInt((((pkBase.SpecialDef * 2 + IV_SpecialDef + BasePoints_SPDef / 4) * level) / 100f + 5 + level) * PokemonTable.GetNatureEffect(2, natrueTypeEnum)); } }
    public int Speed { get { return Mathf.FloorToInt((((pkBase.Speed * 2 + IV_Speed + BasePoints_Speed / 4) * level) / 100f + 5 + level) * PokemonTable.GetNatureEffect(2, natrueTypeEnum)); } }

    //����
    private List<Move> moveCache { get; } = new List<Move>();//���������п�ѧ���ܣ������Ӵ����ܳ�
    private List<Move> moveForgetedCache { get; set; } = new List<Move>();//���������п�ѧ���ܣ������Ӵ����ܳ�
    private List<Move> curMoveCache { get; set; } = new List<Move>();
    private int maxMoveSize = 4;
    public List<Move> GetCurMove()
    {
        if (curMoveCache == null || curMoveCache.Capacity == 0)
        {
            this.InitCurMoveCache();
        }
        else
        {
            Debug.Log("�Ѵ��ڼ��ܳ�");
        }
        return curMoveCache;
    }
    //����Ұ��pokemon�ļ�����
    private void InitCurMoveCache()
    {
        if (pkBase != null && pkBase.AllLearnableMoves != null)
        {
            foreach (var learnableMove in pkBase.AllLearnableMoves)
            {
                var moveBase = learnableMove.LearnableMoveBase;
                if (moveBase == null)
                {
                    Debug.LogError("moveBase is null pkbase.id:" + pkBase.PokemonId);
                    continue;
                }
                Move move = new Move(moveBase, moveBase.InitialPP);
                if (learnableMove.MoveLearnedTypeEnum == MoveLearnedTypeEnum.None)
                {
                    WildPokemonAddMove(move);
                }
                else if (learnableMove.MoveLearnedTypeEnum == MoveLearnedTypeEnum.Level)
                {
                    if (level >= learnableMove.Level)
                    {
                        WildPokemonAddMove(move);
                    }
                }
            }
        }
    }

    public void WildPokemonAddMove(Move move)
    {
        if (move == null)
        {
            Debug.LogError("WildPokemonAddMove move is null");
            return;
        }
        if (curMoveCache == null)
        {
            curMoveCache = new List<Move>
            {
                move
            };
            return;
        }
        if (curMoveCache.Capacity >= maxMoveSize)
        {
            ForgetMove(curMoveCache[0]);
            LearnedMove(move);
        }
        else
        {
            curMoveCache.Add(move);
        }
    }

    public void LearnedMove(Move move)
    {
        if (move != null && moveCache.Contains(move))
        {
            if (curMoveCache == null)
                curMoveCache = new List<Move>();
            curMoveCache.Add(move);
        }
        else
            Debug.Log("ѧϰ����ʧ��");
    }

    public void ForgetMove(Move move)
    {
        if (move != null && curMoveCache.Contains(move))
        {
            curMoveCache.Remove(move);
            if (moveForgetedCache.Contains(move) == false)
            {
                Debug.Log("�����Pokemon����moveForgetedCache���ܳ�");
                moveForgetedCache.Add(move);
            }
        }
        else
            Debug.Log("��������ʧ��");
    }
    /*��ʼ����Ϣ��һЩû���ڹ��캯������ģ�*/
    public void Init()
    {
        if (pkBase != null && pkBase.ShinyType != ShinyTypeEnum.None)
        {
            pkBase.FrontSprite = Resources.Load("PokemonSprite/Front_Shiny/" + pkBase.PokemonId + "_0", typeof(Sprite)) as Sprite;
            pkBase.BackSprite = Resources.Load("PokemonSprite/Back_Shiny/" + pkBase.PokemonId + "_0", typeof(Sprite)) as Sprite; ;
        }
    }
    /*�������*/
    public void RandomIV()
    {
        IV_Hp = Random.Range(1, 31);
        IV_Attack = Random.Range(1, 31);
        IV_Defence = Random.Range(1, 31);
        IV_SpecialATK = Random.Range(1, 31);
        IV_SpecialDef = Random.Range(1, 31);
        IV_Speed = Random.Range(1, 31);
    }
    //ȫ�ι��캯��
    public Pokemon(PokemonBase pkBase, int level, long id, long ownerId, int catchArea, CatchTypeEnum catchTypeEnum, string nickName, int basePoints_HP, int basePoints_ATk, int basePoints_SPATk, int basePoints_Def, int basePoints_SPDef, int basePoints_Speed, int basePoints_Friendship, int basePoints_Beauty, int basePoints_Cute, int basePoints_Smart, int basePoints_Cool, int basePoints_Tough, int iV_Hp, int iV_Attack, int iV_Defence, int iV_SpecialATK, int iV_SpecialDef, int iV_Speed, int curHp, List<Move> moveCache, List<Move> moveForgetedCache, List<Move> curMoveCache, int maxMoveSize, NatrueTypeEnum natrueTypeEnum)
    {
        this.pkBase = pkBase ?? throw new System.ArgumentNullException(nameof(pkBase));
        this.level = level;
        Id = id;
        this.ownerId = ownerId;
        this.catchArea = catchArea;
        this.catchTypeEnum = catchTypeEnum;
        this.nickName = nickName ?? throw new System.ArgumentNullException(nameof(nickName));
        BasePoints_HP = basePoints_HP;
        BasePoints_ATk = basePoints_ATk;
        BasePoints_SPATk = basePoints_SPATk;
        BasePoints_Def = basePoints_Def;
        BasePoints_SPDef = basePoints_SPDef;
        BasePoints_Speed = basePoints_Speed;
        BasePoints_Friendship = basePoints_Friendship;
        BasePoints_Beauty = basePoints_Beauty;
        BasePoints_Cute = basePoints_Cute;
        BasePoints_Smart = basePoints_Smart;
        BasePoints_Cool = basePoints_Cool;
        BasePoints_Tough = basePoints_Tough;
        IV_Hp = iV_Hp;
        IV_Attack = iV_Attack;
        IV_Defence = iV_Defence;
        IV_SpecialATK = iV_SpecialATK;
        IV_SpecialDef = iV_SpecialDef;
        IV_Speed = iV_Speed;
        CurHp = curHp;
        this.moveCache = moveCache ?? throw new System.ArgumentNullException(nameof(moveCache));
        this.moveForgetedCache = moveForgetedCache ?? throw new System.ArgumentNullException(nameof(moveForgetedCache));
        this.curMoveCache = curMoveCache ?? throw new System.ArgumentNullException(nameof(curMoveCache));
        this.maxMoveSize = maxMoveSize;
        this.natrueTypeEnum = natrueTypeEnum;
    }

    //ʡ�Լ��ܳ�
    public Pokemon(PokemonBase pkBase, int level, long id, long ownerId, int catchArea, CatchTypeEnum catchTypeEnum, string nickName, int basePoints_HP, int basePoints_ATk, int basePoints_SPATk, int basePoints_Def, int basePoints_SPDef, int basePoints_Speed, int basePoints_Friendship, int basePoints_Beauty, int basePoints_Cute, int basePoints_Smart, int basePoints_Cool, int basePoints_Tough, int iV_Hp, int iV_Attack, int iV_Defence, int iV_SpecialATK, int iV_SpecialDef, int iV_Speed, int curHp, NatrueTypeEnum natrueTypeEnum)
    {
        this.pkBase = pkBase ?? throw new System.ArgumentNullException(nameof(pkBase));
        this.level = level;
        Id = id;
        this.ownerId = ownerId;
        this.catchArea = catchArea;
        this.catchTypeEnum = catchTypeEnum;
        this.nickName = nickName ?? throw new System.ArgumentNullException(nameof(nickName));
        BasePoints_HP = basePoints_HP;
        BasePoints_ATk = basePoints_ATk;
        BasePoints_SPATk = basePoints_SPATk;
        BasePoints_Def = basePoints_Def;
        BasePoints_SPDef = basePoints_SPDef;
        BasePoints_Speed = basePoints_Speed;
        BasePoints_Friendship = basePoints_Friendship;
        BasePoints_Beauty = basePoints_Beauty;
        BasePoints_Cute = basePoints_Cute;
        BasePoints_Smart = basePoints_Smart;
        BasePoints_Cool = basePoints_Cool;
        BasePoints_Tough = basePoints_Tough;
        IV_Hp = iV_Hp;
        IV_Attack = iV_Attack;
        IV_Defence = iV_Defence;
        IV_SpecialATK = iV_SpecialATK;
        IV_SpecialDef = iV_SpecialDef;
        IV_Speed = iV_Speed;
        CurHp = curHp;
        this.natrueTypeEnum = natrueTypeEnum;
    }

    //����Ұ���Ĺ��캯�����������ܳأ�
    public Pokemon(PokemonBase pkBase, int level, long id, long ownerId, int curHp, NatrueTypeEnum natrueTypeEnum)
    {
        this.pkBase = pkBase ?? throw new System.ArgumentNullException(nameof(pkBase));
        this.level = level;
        Id = id;
        this.ownerId = ownerId;
        CurHp = curHp;
        this.natrueTypeEnum = natrueTypeEnum;
    }
}
