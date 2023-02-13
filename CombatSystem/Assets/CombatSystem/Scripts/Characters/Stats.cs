public class Stats
{
    public float hp;
    public float maxHP;

    public int lv;
    public float ap;
    public float dp;
    public float speed;

    public Stats(int _level, float _maxhealth, float _attack, float _deffense, float _speed)
    {
        lv = _level;
        maxHP = _maxhealth;
        hp = _maxhealth;
        ap = _attack;
        dp = _deffense;
        speed = _speed;
    }

    public Stats Clone()
    {
        return new Stats(this.lv, this.maxHP, this.ap, this.dp, this.speed);
    }
}
