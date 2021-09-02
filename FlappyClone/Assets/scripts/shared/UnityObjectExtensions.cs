﻿using UnityEngine;
using UnityEngine.Assertions;

namespace FlappyClone
{
    public static class UnityObjectExtensions
    {
        public static void AssertAssociation(this Object component, Object member, string memberName) =>
            Assert.IsNotNull(member, $"{component.name} must have an associated {memberName}.");
    }
}