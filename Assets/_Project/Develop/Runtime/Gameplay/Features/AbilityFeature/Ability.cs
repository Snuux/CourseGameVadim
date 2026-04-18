namespace _Project.Develop.Runtime.Gameplay.Features.AbilityFeature
{
    public abstract class Ability
    {
        protected Ability(string id)
        {
            ID = id;
        }

        public string ID { get; }

        public abstract void Activate();
    }
}