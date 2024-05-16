/******************���ö��**********************/
enum PlayerActionStateEnum
{
    Normal,
    Run,
    Bicycle,
    WaterWalk,
}

enum PlayerStateEnum
{
    Normal,
    Busy,
    Battle,
    Animing,
    Dialog,
}
/******************���ö��End**********************/

/******************ս��ö��**********************/
public enum BattleTypeEnum
{
    Wild,
    PVP,
    PVE,
    Boss,
    Plot,
    Champion,
}
public enum BattleNumTypeEnum
{
    Single,
    Double,
    Three,
    Multiple,
}
/******************ս��ö��End**********************/

/******************������ö��**********************/
public enum PokemonType
{
    Normal,
    Alpha,
}
public enum TypeEnum
{
    None,//������
    Normal,//һ��
    Fighting,//��ϵ
    Fly,//����ϵ
    Poison,//��ϵ
    Ground,//��ϵ
    Rock,//��ʯϵ
    Bug,//��ϵ
    Ghost,//����ϵ
    Steel,//��ϵ
    Fire,//��ϵ
    Water,//ˮϵ
    Grass,//��ϵ
    Electric,//��ϵ
    Psychic,//������
    Ice,//��ϵ
    Dragon,//��ϵ
    Dark,//��ϵ
    Fairy,//����ϵ
}

public enum NatrueTypeEnum
{
    Hardy,//�ڷ�
    Lonely,//�¼�į
    Adamant,//��ִ
    Naughty,//��Ƥ
    Brave,//�¸�
    Bold,//��
    Docile,//̹��
    Impish,//����
    Lax,//����
    Relaxed,//����
    Modest,//����
    Mild,//������
    Bashful,//����
    Rash,//��
    Quiet,//�侲
    Calm,//�º�
    Gentle,//��˳
    Careful,//����
    Quirky,//����
    Sassy,//�Դ�
    Timid,//��С
    Hasty,//����
    Jolly,//ˬ��
    Naive,//����
    Serious,//����

}
public enum ShinyTypeEnum
{
    None,
    Square,
    Star,
    Special
}

public enum CatchTypeEnum
{
    None,
    Wild,
    Egg,
    Gift
}
/******************������ö��End**********************/

/******************����ö��**********************/
public enum MoveTypeEnum
{
    Special_Move,
    Physical_Move,
    Status_Move
}
public enum MoveATKRangeEnum
{
    None,
    Single,
    Multiple,
    All_Enemy,
    All
}

public enum MoveContactEnum
{
    None,
    Direct_Attack,
    Special_Attack,
}
public enum MoveLearnedTypeEnum
{
    None,
    Level,
    Hidden_Machine,
    Technical_Machine,
    Teach,
    Inherit,
}
/******************����ö��End**********************/