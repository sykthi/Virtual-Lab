using System;
using UnityEngine;

namespace Oculus.Interaction
{
    /// <summary>
    /// A Transformer that moves the target left and right along a specified axis.
    /// </summary>
    public class ProbeMovement : MonoBehaviour, ITransformer
    {
        public enum MoveAxis
        {
            Right = 0,
            Up = 1,
            Forward = 2
        }

        [SerializeField, Optional]
        private Transform _pivotTransform = null;

        public Transform Pivot => _pivotTransform != null ? _pivotTransform : transform;

        [SerializeField]
        private MoveAxis _moveAxis = MoveAxis.Right;

        public MoveAxis MovementAxis => _moveAxis;

        [Serializable]
        public class OneGrabMoveConstraints
        {
            public FloatConstraint MinPosition;
            public FloatConstraint MaxPosition;
        }

        [SerializeField]
        private OneGrabMoveConstraints _constraints =
            new OneGrabMoveConstraints()
            {
                MinPosition = new FloatConstraint(),
                MaxPosition = new FloatConstraint()
            };

        public OneGrabMoveConstraints Constraints
        {
            get => _constraints;
            set => _constraints = value;
        }

        private IGrabbable _grabbable;
        private Vector3 _initialGrabPosition;
        private float _startPosition;
        private Vector3 _worldMoveAxis;

        public void Initialize(IGrabbable grabbable)
        {
            _grabbable = grabbable;
        }

        public void BeginTransform()
        {
            var grabPoint = _grabbable.GrabPoints[0];
            var targetTransform = _grabbable.Transform;

            Vector3 localAxis = Vector3.zero;
            localAxis[(int)_moveAxis] = 1f;
            _worldMoveAxis = Pivot.rotation * localAxis;

            _initialGrabPosition = grabPoint.position;
            _startPosition = Vector3.Dot(targetTransform.position - Pivot.position, _worldMoveAxis);
        }

        public void UpdateTransform()
        {
            var grabPoint = _grabbable.GrabPoints[0];
            var targetTransform = _grabbable.Transform;

            float moveDelta = Vector3.Dot(grabPoint.position - _initialGrabPosition, _worldMoveAxis);
            float newPosition = _startPosition + moveDelta;

            if (Constraints.MinPosition.Constrain)
            {
                newPosition = Mathf.Max(newPosition, Constraints.MinPosition.Value);
            }
            if (Constraints.MaxPosition.Constrain)
            {
                newPosition = Mathf.Min(newPosition, Constraints.MaxPosition.Value);
            }

            targetTransform.position = Pivot.position + _worldMoveAxis * newPosition;
        }

        public void EndTransform() { }

        #region Inject

        public void InjectOptionalPivotTransform(Transform pivotTransform)
        {
            _pivotTransform = pivotTransform;
        }

        public void InjectOptionalMovementAxis(MoveAxis moveAxis)
        {
            _moveAxis = moveAxis;
        }

        public void InjectOptionalConstraints(OneGrabMoveConstraints constraints)
        {
            _constraints = constraints;
        }

        #endregion
    }
}