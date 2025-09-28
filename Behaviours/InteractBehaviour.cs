using System;
using System.Collections.Generic;
using UnityEngine;
using ProjectDew.FSM;

namespace ProjectDew.CharacterController
{
    public class InteractBehaviour : StateBehaviour, IDisposable
	{
		private readonly List<InteractableObject> interactableObjects;

		private readonly Controller controller;

		private readonly int interactableLayer;

		private bool ThereAreInteractableObjects => interactableObjects.Count > 0;

		public InteractBehaviour(Controller controller, IInteractData interactData)
		{
			this.controller = controller;

			interactableLayer = interactData.InteractableLayer.ToInt();

			interactableObjects = new();

			controller.TriggerEnter += AddInteractableObject;
			controller.TriggerExit += RemoveInteractableObject;
		}

		public void Dispose()
		{
			controller.TriggerEnter -= AddInteractableObject;
			controller.TriggerExit -= RemoveInteractableObject;
		}

		public override void Execute() { }

		public bool Interact() => ThereAreInteractableObjects && Interact(interactableObjects[^1]);

		public bool Interact(InteractableObject interactableObject)
		{
			if (ThereAreInteractableObjects && interactableObjects.Contains(interactableObject))
			{
				return interactableObject.Interact();
			}

			return false;
		}

		private void AddInteractableObject(Collider collider)
		{
			GameObject obj = collider.gameObject;

			if (obj.layer == interactableLayer)
			{
				if (obj.TryGetComponent(out InteractableObject interactable))
				{
					interactableObjects.Add(interactable);
				}
			}
		}

		private void RemoveInteractableObject(Collider collider)
		{
			GameObject obj = collider.gameObject;

			if (obj.layer == interactableLayer)
			{
				if (obj.TryGetComponent(out InteractableObject interactable))
				{
					interactableObjects.Remove(interactable);
				}
			}
		}
    }
}
