using UnityEngine;
using ProjectDew.FSM;

namespace ProjectDew.CharacterController
{
    [CreateAssetMenu(fileName = "SimpleControllerData", menuName = "Scriptable Objects/Simple Controller Data")]
    public class SimpleControllerData : ScriptableObject
    {
		[SerializeField]
		private SimpleInputData inputData;

		[SerializeField]
		private ControllerData controllerData;

		[SerializeField]
		private StateMachineData stateMachineData;

		[SerializeField]
		private MoveData groundedData;

		[SerializeField]
		private MoveData runData;

		[SerializeField]
		private MoveData crouchData;

		[SerializeField]
		private MoveData slideData;

		[SerializeField]
		private AnimateData animateData;

		[SerializeField]
		private float rotationSpeed;

		public SimpleInputData InputData => inputData;

		public ControllerData ControllerData => controllerData;

		public StateMachineData StateMachineData => stateMachineData;

		public MoveData GroundedData => groundedData;

		public MoveData RunData => runData;

		public MoveData CrouchData => crouchData;

		public MoveData SlideData => slideData;

		public AnimateData AnimateData => animateData;

		public float RotationSpeed => rotationSpeed;
    }
}
