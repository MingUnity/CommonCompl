﻿using UnityEngine;

namespace MingUnity.InputModule
{
    public class MDoubleFingerGesture
    {

        public MInput.GestureType currentGesture = MInput.GestureType.None;
        public MInput.GestureType oldGesture = MInput.GestureType.None;
        public int finger0;
        public int finger1;

        public float startTimeAction;
        public float timeSinceStartAction;
        public Vector2 startPosition;
        public Vector2 position;
        public Vector2 deltaPosition;
        public Vector2 oldStartPosition;
        public float startDistance;

        public float fingerDistance;
        public float oldFingerDistance;

        public bool lockPinch = false;
        public bool lockTwist = true;
        public float lastPinch = 0;
        public float lastTwistAngle = 0;

        // Game Object
        public GameObject pickedObject;
        public GameObject oldPickedObject;
        public Camera pickedCamera;
        public bool isGuiCamera;

        // UI
        public bool isOverGui;
        public GameObject pickedUIElement;

        public bool dragStart = false;
        public bool swipeStart = false;

        public bool inSingleDoubleTaps = false;
        public float tapCurentTime = 0;

        public void ClearPickedObjectData()
        {
            pickedObject = null;
            oldPickedObject = null;
            pickedCamera = null;
            isGuiCamera = false;
        }

        public void ClearPickedUIData()
        {
            isOverGui = false;
            pickedUIElement = null;
        }
    }
}
