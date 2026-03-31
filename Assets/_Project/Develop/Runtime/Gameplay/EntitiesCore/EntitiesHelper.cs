using _Project.Develop.Runtime.Gameplay.Features.TeamsFeature;
using _Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay.EntitiesCore
{
    public static class EntitiesHelper
    {
        public static bool TryTakeDamageFrom(Entity source, Entity damageable, float damage)
        {
            if (damageable.TryGetTakeDamageRequest(out ReactiveEvent<float> takeDamageRequest) == false)
                return false;

            if (IsSameTeam(source, damageable))
                return false;

            takeDamageRequest.Invoke(damage);

            Debug.Log($"Урон:  {damage} От: {source} К: {damageable}");

            return true;
        }

        public static bool IsSameTeam(Entity firstEntity, Entity secondEntity)
        {
            if (firstEntity.TryGetTeam(out ReactiveVariable<Teams> sourceTeam)
                && secondEntity.TryGetTeam(out ReactiveVariable<Teams> targetTeam))
            {
                return sourceTeam.Value == targetTeam.Value;
            }

            return false;
        }
    }
}