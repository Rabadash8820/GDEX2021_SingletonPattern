using UnityEngine;
using UnityEngine.Assertions;

namespace FlappyClone.Shared
{
    public static class UnityObjectExtensions
    {
        public static void AssertAssociation(this Object component, object member, string memberName) =>
            Assert.IsNotNull(member, $"{component.name} must have an associated {memberName}.");
    }
}
