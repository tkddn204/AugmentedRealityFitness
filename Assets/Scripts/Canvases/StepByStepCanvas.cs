﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using OpenCV;
using Constants;

public class StepByStepCanvas : MonoBehaviour {

    private Transform canvasListTransform;  // To grab "Canvas List" context
    Text text;

    private string currentExercise;
    private int currentStep = 1;
    private int maxStep;
    
    void Start()
    {
        canvasListTransform = GameObject.Find("/UI/Canvas List").transform;
        text = canvasListTransform.Find("StepByStepCanvas/Text")
            .GetComponent<Text>();
        currentExercise = GameObject.Find("/ImageTarget/Beta")
            .GetComponent<BetaController>().exercise;
        Init();
        maxStep = Exercise.GetStep(currentExercise);
    }
    void Init()
    {
        currentStep = 1;
        time = 0.0f;
    }

    bool isCompletedCurrentStep = false;
    float time = 0.0f;
    float endTime = 5.0f;

    void nextCurrentStep()
    {
        currentStep++;
        time = 0.0f;
    }

    void Update()
    {
        if (this.isActiveAndEnabled)
        {
            time += Time.deltaTime;
            switch (currentStep)
            {
                case 1:
                    if (time >= endTime)
                    {
                        nextCurrentStep();
                        OpenCVImage.Instance().stepOne = true;
                    }
                    break;
                case 2:
                    if (time >= endTime)
                    {
                        nextCurrentStep();
                        OpenCVImage.Instance().stepTwo = true;
                    }
                    GameObject.Find("/ImageTarget/Beta")
                        .GetComponent<BetaController>().nextStep();
                    break;
                case 3:
                    GameObject.Find("/ImageTarget/Beta")
                        .GetComponent<BetaController>().endStep();
                    GameObject.Find("/ImageTarget/Beta")
                        .GetComponent<BetaController>().stopExerciseAnimation();
                    GameObject.Find("/Managers/Canvas Manager")
                        .GetComponent<CanvasManager>().endStep();
                    break;
                default:
                    break;
            }

            text.text = (int)time + " / " + (int)endTime + "초 안에 자세를 잡아주세요!\n"
                + currentExercise + " " + currentStep + " / " + maxStep;
        }
    }
}
