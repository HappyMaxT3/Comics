namespace Main
{
    //интерфейс для взаимодействиями с объектами, которым можно нанести урон
    public interface IDamageable
    {
        void TakeDamage(int amount);
    }
}