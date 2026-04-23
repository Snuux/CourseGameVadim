using System;

namespace _Project.Develop.Runtime.Utilities.Conditions
{
    public interface ICompositeCondition : ICondition
    {
        ICompositeCondition Add(ICondition condition, int order = 0, Func<bool, bool, bool> standardLogicOperation = null);

        ICompositeCondition Remove(ICondition condition);
    }
}
