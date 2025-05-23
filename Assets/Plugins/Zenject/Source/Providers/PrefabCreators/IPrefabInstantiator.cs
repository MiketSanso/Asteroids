#if !NOT_UNITY3D

using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Zenject
{
    public interface IPrefabInstantiator
    {
        Type ArgumentTarget { get; }

        List<TypeValuePair> ExtraArguments { get; }

        GameObjectCreationParameters GameObjectCreationParameters { get; }

        GameObject Instantiate(InjectContext context, List<TypeValuePair> args, out Action injectAction);

        Object GetPrefab(InjectContext context);
    }
}

#endif