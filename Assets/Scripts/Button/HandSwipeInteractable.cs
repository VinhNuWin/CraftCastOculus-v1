// using UnityEngine;
// using Oculus.SampleFramework;

// public class HandSwipeInteractable : MonoBehaviour
// {
//     public Collider interactionSurface;
//     private OVRHand hand;
//     private OVRSkeleton skeleton;
//     private Vector3 initialFingerPosition;
//     private bool isTrackingSwipe = false;

//     void Start()
//     {
//         hand = GetComponent<OVRHand>();
//         skeleton = GetComponent<OVRSkeleton>();
//         if (hand == null || skeleton == null)
//         {
//             Debug.LogError("Required components are missing");
//         }
//     }

//     void Update()
//     {
//         if (hand == null || skeleton == null) return;

//         if (OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, OVRInput.Controller.Hands))
//         {
//             OVRBone indexTip = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip];
//             Vector3 closestPoint = interactionSurface.ClosestPoint(indexTip.Transform.position);
//             float proximityThreshold = 0.05f; // Adjust based on sensitivity needed

//             if ((closestPoint - indexTip.Transform.position).sqrMagnitude < proximityThreshold * proximityThreshold)
//             {
//                 if (!isTrackingSwipe)
//                 {
//                     initialFingerPosition = indexTip.Transform.position;
//                     isTrackingSwipe = true;
//                 }
//             }
//             else if (isTrackingSwipe)
//             {
//                 // Swipe ends when finger moves away from proximity threshold
//                 TrackSwipe(initialFingerPosition, indexTip.Transform.position);
//                 isTrackingSwipe = false;
//             }
//         }
//     }

//     private void TrackSwipe(Vector3 start, Vector3 end)
//     {
//         Vector3 direction = end - start;
//         string swipeDirection;

//         if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
//         {
//             swipeDirection = direction.x > 0 ? "Right" : "Left";
//         }
//         else
//         {
//             swipeDirection = direction.y > 0 ? "Up" : "Down";
//         }

//         Debug.Log($"Swipe detected: {swipeDirection}");
//         // Additional logic based on swipe direction
//     }
// }


// using UnityEngine;
// using UnityEngine.Events;

// public class HandSwipeInteractable : MonoBehaviour
// {
//     public Collider interactionSurface;
//     private bool isPoked = false;
//     private bool isSwiping = false;
//     private Vector3 initialTouchPosition;
//     private float touchStartTime;

//     public UnityEvent onPoke;   // Event triggered on poke
//     public UnityEvent onSwipe;  // Event triggered on swipe

//     void Update()
//     {
//         // Detect touch input
//         if (OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, OVRInput.Controller.Hands))
//         {
//             CheckPokeAndSwipe();
//         }
//         else
//         {
//             // Reset interaction when the touch ends
//             if (isSwiping || isPoked)
//             {
//                 ResetInteraction();
//             }
//         }
//     }

//     void CheckPokeAndSwipe()
//     {
//         OVRHand hand = GetComponent<OVRHand>();
//         OVRSkeleton skeleton = GetComponent<OVRSkeleton>();
//         OVRBone indexTip = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip];
//         float distanceToSurface = Vector3.Distance(interactionSurface.ClosestPoint(indexTip.Transform.position), indexTip.Transform.position);

//         // Start or continue a poke
//         if (!isPoked && distanceToSurface < 0.05f) // Threshold for considering a touch a poke
//         {
//             if (!isSwiping)
//             {
//                 isPoked = true;
//                 onPoke.Invoke(); // Trigger event or custom logic for poke
//             }
//         }

//         // Check if swipe should be initiated
//         if (isPoked && !isSwiping)
//         {
//             initialTouchPosition = indexTip.Transform.position;
//             isSwiping = true; // Start tracking swipe
//         }

//         // Process swipe movement
//         if (isSwiping)
//         {
//             Vector3 currentFingerPosition = indexTip.Transform.position;
//             if (Vector3.Distance(currentFingerPosition, initialTouchPosition) > 0.1f) // Swipe distance threshold
//             {
//                 onSwipe.Invoke(); // Trigger event or custom logic for swipe
//                 ResetInteraction(); // Reset after swipe
//             }
//         }
//     }

//     void ResetInteraction()
//     {
//         isPoked = false;
//         isSwiping = false;
//     }
// }
