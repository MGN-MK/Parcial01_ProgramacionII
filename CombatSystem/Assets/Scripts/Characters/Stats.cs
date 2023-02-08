public class Stats
{
    public float hp;
    public float maxHP;

    public int lv;
    public float ap;
    public float dp;
    public float spirit;
    public float speed;

    public Stats(int _level, float _maxhealth, float _attack, float _deffense, float _spirit, float _speed)
    {
        this.lv = _level;

        this.maxHP = _maxhealth;
        this.hp = _maxhealth;

        this.ap = _attack;
        this.dp = _deffense;
        this.spirit = _spirit;
        this.speed = _speed;
    }

    public Stats Clone()
    {
        return new Stats(this.lv, this.maxHP, this.ap, this.dp, this.spirit, this.speed);
    }
}
