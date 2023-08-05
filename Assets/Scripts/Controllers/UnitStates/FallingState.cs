﻿using Interfaces;

namespace Controllers.UnitStates
{
    public class FallingState : UnitStateBase
    {
        private float _timeOut = 1f;
        private float _currentTimeOut;
        
        public FallingState(IUnitContext unit) : base(unit)
        {
        }

        public override void HandleState(UnitStateBase newState)
        {
            switch (newState)
            {
                case DeadState deadState:
                    Unit.SetState(Unit.DeadState);
                    break;
                case StandUpState standUpState:
                    Unit.SetState(Unit.StandUpState);
                    break;
            }
        }

        public override void StartState()
        {
            Unit.View.SetRigidbodiesKinematic(false);
            Unit.View.SetAnimatorEnabled(false);
            Unit.Model.SetIsInteractable(false);
        }

        public override void UpdateLocal(float deltaTime)
        {
            if (_currentTimeOut< 0)
            {
                _currentTimeOut = _timeOut;
                CheckForLanding();
            }
            else
            {
                _currentTimeOut -= deltaTime;
            }
        }

        private void CheckForLanding()
        {
            
        }

        public override void EndState()
        {
        }
    }
}