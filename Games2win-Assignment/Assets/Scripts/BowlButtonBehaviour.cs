using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BowlButtonBehaviour : MonoBehaviour {
    
    private const int OFF_SPIN = 0;
    private const int STRAIGHT = 1;
    private const int LEG_SPIN = 2;

    public event EventHandler<OnBowlButtonClickedArgs> OnBowlButtonClicked;

    public class OnBowlButtonClickedArgs : EventArgs {
        public int BallSpinSelection;
    }

    public void BowlButtonClick() {
        Debug.Log("on button click");
        OnBowlButtonClicked?.Invoke(this, new OnBowlButtonClickedArgs {
            BallSpinSelection = STRAIGHT
        });
    }
}
