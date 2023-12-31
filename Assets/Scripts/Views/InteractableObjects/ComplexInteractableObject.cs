﻿using System;
using System.Collections.Generic;
using Abilities;
using Interfaces;
using UnityEngine;

namespace Views
{
    public class ComplexInteractableObject : InteractableObject, IOwner
    {
        [SerializeField] protected AbilityDescription[] Abilities;
        
        private List<IDisposable> _disposable = new();

        public Transform Transform => transform;

        public override void Interact(IOwner owner = null, ITarget target = null)
        {
            if (owner == null)
            {
                owner = this;
            }
            
            foreach (var a in Abilities)
            {
                a.GetAbility.Execute(owner, target);
            }
        }

        private void Awake()
        {
            foreach (var a in Abilities)
            {
                var ability = a.GetAbility;
                if (ability is IInitable initable)
                {
                    initable.Init();
                }

                if (ability is IDisposable disposable)
                {
                    _disposable.Add(disposable);
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var d in _disposable)
            {
                d?.Dispose();
            }
        }
    }
}