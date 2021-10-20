public abstract class EnemyState
{
    public abstract void EnterState(Enemy enemy);//enter a state, just like enter into the idle state, but on their situation it will enter into the attack state
    public abstract void onUpdate(Enemy enemy);
}
