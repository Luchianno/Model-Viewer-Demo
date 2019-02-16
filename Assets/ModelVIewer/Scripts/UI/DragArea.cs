using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Image))]
public class DragArea : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float Pinch { get; protected set; }
    public Vector2 Swipe { get; protected set; }

    public PinchEvent OnPinchChanged;
    public SwipeEvent OnSwipeChanged;
    public UnityEvent OnPinchEnded;
    public UnityEvent OnSwipeEnded;

    // public float MinPinch = 0.1f;
    // public float MaxPinch = 2f;

    public List<TouchInfo> pressStart = new List<TouchInfo>();
    public LinkedList<TouchInfo> currentPos = new LinkedList<TouchInfo>();

    public float startDistance;
    public float angle;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (pressStart.Count == 2)
            return;

        Debug.Log($"OnBeginDrag called. touchID: {eventData.pointerId}");

        pressStart.Add(new TouchInfo(eventData.pointerId, eventData.position));
        currentPos.AddLast(new TouchInfo(eventData.pointerId, eventData.position));

        if (pressStart.Count > 1)
        {
            startDistance = (pressStart[pressStart.Count - 1].Pos - pressStart[pressStart.Count - 2].Pos).magnitude;
            // startAngle = Vector2.SignedAngle()
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!pressStart.Exists(x => x.Id == eventData.pointerId))
            return;

        //swipe
        if (pressStart.Count == 1)
        {
            Swipe = eventData.position - pressStart[0].Pos;
            OnSwipeChanged.Invoke(new SwipeInfo() { Amount = Swipe, Delta = eventData.delta });
        }
        // pinch
        else if (pressStart.Count > 1)
        {
            var node = currentPos.Nodes().FirstOrDefault(x => x.Value.Id == eventData.pointerId); // find touch info
            if (node == null)
                return;
            currentPos.Remove(node);
            currentPos.AddLast(node); // increase it's priority 

            // save previous values
            var oldPos = node.Value.Pos;
            var oldPinch = Pinch;

            // update touch info
            node.Value = new TouchInfo(eventData.pointerId, eventData.position);

            var currentDistance = (currentPos.Last.Value.Pos - currentPos.Last.Previous.Value.Pos).magnitude;
            Pinch = currentDistance / startDistance;

            //black magic
            var angleDelta = Vector2.SignedAngle(
                                    oldPos - currentPos.Last.Previous.Value.Pos,
                                    currentPos.Last.Value.Pos - currentPos.Last.Previous.Value.Pos);



            OnPinchChanged.Invoke(new PinchInfo()
            {
                Amount = Pinch,
                Delta = Pinch - oldPinch,
                Angle = 0f,
                AngleDelta = angleDelta, // Angdelta Merkel https://a.wattpad.com/cover/167060355-288-k824453.jpg
                CenterDelta = eventData.delta / 2
            });
        }

        // swipe
        // pinch
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        pressStart.RemoveAll(x => x.Id == eventData.pointerId);
        currentPos.Remove(currentPos.Nodes().First(x => x.Value.Id == eventData.pointerId));

        if (pressStart.Count < 2)
            OnPinchEnded.Invoke();
        if (pressStart.Count == 0)
            OnSwipeEnded.Invoke();
    }

    public struct TouchInfo
    {
        public int Id;
        public Vector2 Pos;

        public TouchInfo(int pointerId, Vector2 position)
        {
            this.Id = pointerId;
            this.Pos = position;
        }
    }

    public struct PinchInfo
    {
        public float Amount;
        public float Delta;
        public float Angle;
        public float AngleDelta;
        public Vector2 CenterDelta;
    }

    public struct SwipeInfo
    {
        public Vector2 Amount;
        public Vector2 Delta;
    }

    [Serializable]
    public class PinchEvent : UnityEvent<PinchInfo> { }

    [Serializable]
    public class SwipeEvent : UnityEvent<SwipeInfo> { }


}
