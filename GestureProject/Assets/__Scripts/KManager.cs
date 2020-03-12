using System;
using System.Collections.Generic;
using System.IO;
using Windows.Kinect;
using Microsoft.Kinect.VisualGestureBuilder;
using UnityEngine;
using System.Collections;

public class KManager : MonoBehaviour 
{
    VisualGestureBuilderDatabase _dbGestures;
    Windows.Kinect.KinectSensor _kinect;
    VisualGestureBuilderFrameSource _gestureFrameSource;
    Windows.Kinect.BodyFrameSource _bodyFrameSource;
    VisualGestureBuilderFrameReader _gestureFrameReader;
    Windows.Kinect.BodyFrameReader _bodyFrameReader;
    Gesture _jump; // наш жест
    Gesture _armRaise;
    Gesture _moveLeft;
    Gesture _moveRight;
    bool isJumping = false;
    Windows.Kinect.Body[] _bodies; // все пользователи, найденные Kinect'ом
    Windows.Kinect.Body _currentBody = null; //Текущий пользователь, жесты которого мы отслеживаем
    private string _getsureBasePath; //Путь до нашей обученной модели
    bool gestureDetected = false;
    public delegate void SimpleEvent();
    public static event SimpleEvent OnSwipeUpDown;
    public GameObject startgame;

    private bool gamestarted = false;

    void Start () 
    {
        
        InitKinect();
    }


    void InitKinect()
    {
        _getsureBasePath = Path.Combine(Application.streamingAssetsPath, "GestureDB/JumpDB.gbd");
        _dbGestures = VisualGestureBuilderDatabase.Create(_getsureBasePath);
        _bodies = new Windows.Kinect.Body[6];
        _kinect = Windows.Kinect.KinectSensor.GetDefault();
        _kinect.Open();
        _gestureFrameSource = VisualGestureBuilderFrameSource.Create(_kinect, 0);

        foreach (Gesture gest in _dbGestures.AvailableGestures)
        {
            Debug.Log(gest.Name);
            _gestureFrameSource.AddGesture(gest);
            if (gest.Name == "Jump")
            {
                _jump = gest;
                Debug.Log("Added:" + gest.Name);
            }else if(gest.Name == "Lean_Left"){
                _moveLeft = gest;
                Debug.Log("Added:" + gest.Name);
            }else if(gest.Name == "Lean_Right"){
                _moveRight = gest;
                Debug.Log("Added:" + gest.Name);
            }
        }
        _bodyFrameSource = _kinect.BodyFrameSource;
        _bodyFrameReader = _bodyFrameSource.OpenReader();
        _bodyFrameReader.FrameArrived += _bodyFrameReader_FrameArrived;

        _gestureFrameReader = _gestureFrameSource.OpenReader();
        _gestureFrameReader.IsPaused = true;
        _gestureFrameReader.FrameArrived += _gestureFrameReader_FrameArrived;
    }

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
                //Debug.Log("_currentBody is not null");
                _gestureFrameSource.TrackingId = _currentBody.TrackingId;
                _gestureFrameReader.IsPaused = false;
            }
            else 
            {
                //Debug.Log("_currentBody is null");
                _gestureFrameSource.TrackingId = 0;
                _gestureFrameReader.IsPaused = true;
            }
            
        }
    }

    void _gestureFrameReader_FrameArrived(object sender, VisualGestureBuilderFrameArrivedEventArgs args)
    {

        if (_gestureFrameSource.IsTrackingIdValid && StartGame.ControlsOn)
        {
            //Debug.Log("Tracking id is valid, value = " + _gestureFrameSource.TrackingId);
            using (var frame = args.FrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    //using (var results = frame.DiscreteGestureResults)
                    var results = frame.DiscreteGestureResults;
                    if (results != null && results.Count > 0)
                    {
                        DiscreteGestureResult jumpResult;
                        DiscreteGestureResult moveLeftResult;
                        DiscreteGestureResult moveRightResult;
                        results.TryGetValue(_jump, out jumpResult);
                        results.TryGetValue(_moveLeft, out moveLeftResult);
                        results.TryGetValue(_moveRight, out moveRightResult);
                        //Debug.Log("Result not null, conf = " + jumpResult.Confidence);

                        if (jumpResult.FirstFrameDetected && gestureDetected == false)
                        {
                            gestureDetected = true;
                            Debug.Log("Jump Gesture");
                            if(startgame.GetComponent<StartGame>().IsGrounded()){
                                startgame.GetComponent<StartGame>().PlayerJump();
                            }
                            isJumping = true;
                            
                        }
                        //else if(armRaiseResult.Confidence > 0.1 && gestureDetected == false){
                        //     gestureDetected = true;
                        //     Debug.Log("Arm raised");
                        //     if(gamestarted == false){
                        //         startgame.GetComponent<StartGame>().OnMouseDown();
                        //         gamestarted = true;
                        //     }
                        // }
                        else if(moveLeftResult.FirstFrameDetected && gestureDetected == false){
                            gestureDetected = true;
                            Debug.Log("Left Roll");
                            startgame.GetComponent<StartGame>().MoveLeft();
                        }else if(moveRightResult.FirstFrameDetected && gestureDetected == false){
                            gestureDetected = true;
                            startgame.GetComponent<StartGame>().MoveRight();
                        }
                        else
                        {
                            //Debug.Log("False");

                            gestureDetected = false;
                        }
                    }
                }
            }
        }
    }



    void Update () 
    {
	
    }
}

