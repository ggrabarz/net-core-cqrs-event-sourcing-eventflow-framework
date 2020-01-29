using EventFlow.Specifications;
using System.Collections.Generic;

namespace NetCoreEventFlow.Domain.Inventory.Specifications
{
    public class CheckInNumberSpecification : Specification<int>
    {
        protected override IEnumerable<string> IsNotSatisfiedBecause(int count)
        {
            if (count <= 0) yield return "must have a count greater than 0 to add to inventory";
        }
    }
}
