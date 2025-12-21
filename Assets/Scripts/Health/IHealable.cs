public interface IHealable
{
    float Health { get; }
    float TakeHeal(float healAmount);
}
