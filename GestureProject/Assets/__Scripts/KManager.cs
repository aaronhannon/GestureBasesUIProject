using System;
using System.Collections.Generic;
using System.IO;
using Windows.Kinect;
using Microsoft.Kinect.VisualGestureBuilder;
using UnityEngine;
using System.Collections;

// Class which handles all Kinect Input - Gesture detecton
public class KManager : MonoBehaviour 
{
    VisualGestureBuilderDatabase _dbGestures;
    public Windows.Kinect.KinectSensor _kinect;
    VisualGestureBuilderFrameSource _gestureFrameSource;
    Windows.Kinect.BodyFrameSource _bodyFrameSource;
    VisualGestureBuilderFrameReader _gestureFrameReader;
    Windows.Kinect.BodyFrameReader _bodyFrameReader;
    Gesture _jump;
    Gesture _armRaise;
    Gesture _moveLeft;
    Gesture _moveRight;
    Gesture _swing;
    //Gesture _crouch;

    bool isJumping = false;
    Windows.Kinect.Body[] _bodies; 
    Windows.Kinect.Body _currentBody = null; 
    private string _getsureBasePath; 
    bool gestureDetected = false;
    public delegate void SimpleEvent();
    public static event SimpleEvent OnSwipeUpDown;
    public GameObject startgame;

    private bool gamestarted = false;

    void Start () 
    {
        //initialise kinect
        InitKinect();
    }

    //Methof to initialise your kinect
    void InitKinect()
    {
        _getsureBasePath = Path.Combine(Application.streamingAssetsPath, "GestureDB/JumpDB.gbd");
        _dbGestures = VisualGestureBuilderDatabase.Create(_getsureBasePath);
        _bodies = new Windows.Kinect.Body[6];
        _kinect = Windows.Kinect.KinectSensor.GetDefault();
        _kinect.Open();
        _gestureFrameSource = VisualGestureBuilderFrameSource.Create(_kinect, 0);

        //for each gesture in trained database of custom gestures - add them to kinect frame source
        foreach (Gesture gest in _dbGestures.AvailableGestures)
        {
            _gestureFrameSource.AddGesture(gest);
            if (gest.Name == "Jump")
            {
                _jump = gest;
            }else if(gest.Name == "Lean_Left"){
                _moveLeft = gest;
            }else if(gest.Name == "Lean_Right"){
                _moveRight = gest;
            }
            else if (gest.Name == "Swing")
            {
                _swing = gest;
            }
        }
        _bodyFrameSource = _kinect.BodyFrameSource;
        _bodyFrameReader = _bodyFrameSource.OpenReader();
        _bodyFrameReader.FrameArrived += _bodyFrameReader_FrameArrived;

        _gestureFrameReader = _gestureFrameSource.OpenReader();
        _gestureFrameReader.IsPaused = true;
        _gestureFrameReader.FrameArrived += _gestureFrameReader_FrameArrived;
    }

    //method for detecting a body in kinect frame
    void _bodyFrameReader_FrameArrived(object sender, Windows.Kinect.BodyFrameArrivedEventArgs args)
    {
        var frame = args.FrameReference;
        using (var multiSourceFrame = frame.AcquireFrame())
        {
            multiSourceFrame.GetAndRefreshBodyData(_bodies); //обновляем данные о найденных людях
            _currentBody = null;
            foreach (var body in _bodies)
            {
                if (body != null && body.IsTracked)
                {
                    _currentBody = body; // для простоты берем первого найденного человека
		            break;
                }
            }
            if (_currentBody != null)
            {
                _gestureFrameSource.TrackingId = _currentBody.TrackingId;
                _gestureFrameReader.IsPaused = false;
            }
            else 
            {
                _gestureFrameSource.TrackingId = 0;
                _gestureFrameReader.IsPaused = true;
            }
        }
    }

    //method for detecting a gesture in kinect frame
    void _gestureFrameReader_FrameArrived(object sender, VisualGestureBuilderFrameArrivedEventArgs args)
    {

        if (_gestureFrameSource.IsTrackingIdValid && StartGame.ControlsOn)
        {
            using (var frame = args.FrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    var results = frame.DiscreteGestureResults;
                    if (results != null && results.Count > 0)
                    {
                        DiscreteGestureResult jumpResult;
                        DiscreteGestureResult moveLeftResult;
                        DiscreteGestureResult moveRightResult;
                        DiscreteGestureResult crouchResult;
                        DiscreteGestureResult swingResult;
                        results.TryGetValue(_jump, out jumpResult);
                        results.TryGetValue(_moveLeft, out moveLeftResult);
                        results.TryGetValue(_moveRight, out moveRightResult);
                        results.TryGetValue(_swing, out swingResult);

                        //if jump gesture detected
                        if (jumpResult.FirstFrameDetected && gestureDetected == false)
                        {
                            gestureDetected = true;
                            if(startgame.GetComponent<StartGame>().IsGrounded()){
                                startgame.GetComponent<StartGame>().PlayerJump();
                            }
                            isJumping = true;
                        }
                        //if move left gesture detected
                        else if (moveLeftResult.FirstFrameDetected && gestureDetected == false){
                            gestureDetected = true;
                            startgame.GetComponent<StartGame>().MoveLeft();
                        }
                        //if move right gesture detected
                        else if (moveRightResult.FirstFrameDetected && gestureDetected == false){
                            gestureDetected = true;
                            startgame.GetComponent<StartGame>().MoveRight();
                        }
                        //if swing gesture detected
                        else if (swingResult.FirstFrameDetected && gestureDetected == false)
                        {
                            gestureDetected = true;
                            startgame.GetComponent<StartGame>().PlayerAttack();
                        }
                        else
                        {
                            //no gesture detected in frame
                            gestureDetected = false;
                        }
                    }
                }
            }
        }
    }

    void OnApplicationQuit()
    {
        _kinect.Close();
        Debug.Log("Application ending after " + Time.time + " seconds");
    }
}

