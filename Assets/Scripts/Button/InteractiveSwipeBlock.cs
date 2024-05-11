// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Oculus.Interaction;
// using Oculus.Interaction.Surfaces;

// public class InteractiveSwipeBlock : MonoBehaviour
// {
//     [SerializeField]
//     private float moveDistanceLimit = 1.0f; // Limit of how far the object can move along the x-axis.

//     private Vector3 initialPosition;

//     protected override void Start()
//     {
//         base.Start();
//         initialPosition = transform.position; // Store the initial position of the object.
//     }

//     public override void ProcessInteractor(IPokeInteractor interactor)
//     {
//         base.ProcessInteractor(interactor);
//         if (interactor.State == InteractorState.Select)
//         {
//             MoveObjectX(interactor);
//         }
//     }

//     private void MoveObjectX(IPokeInteractor interactor)
//     {
//         Vector3 interactorPosition = interactor.Transform.position;
//         float moveX = Mathf.Clamp(interactorPosition.x - initialPosition.x, -moveDistanceLimit, moveDistanceLimit);
//         transform.position = new Vector3(initialPosition.x + moveX, transform.position.y, transform.position.z);
//     }
// }