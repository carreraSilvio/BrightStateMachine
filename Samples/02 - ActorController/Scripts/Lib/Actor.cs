﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace BrightLib.StateMachine.Samples
{
    public class Actor : MonoBehaviour
    {
        private Dictionary<Type, ActorModule> _modules;

        private void Awake()
        {
            _modules = new Dictionary<Type, ActorModule>();

            var rawMods = GetComponentsInChildren<ActorModule>(true);
            foreach (var rawMod in rawMods)
            {
                _modules.Add(rawMod.GetType(), rawMod);
            }
        }

        public T FetchModule<T>() where T : ActorModule => (T)_modules[typeof(T)];
    }
}