using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {
    [SerializeField] private int bowlingSpeed = 100;
    [SerializeField] private GameObject ballObject;
    [SerializeField] private BowlButtonBehaviour bowlButton;
    [SerializeField] private MarkerBehaviour marker;
    private Vector3 ballStartPoint;
    private Camera _camera;
    private Rigidbody _rigitBody;
    private static float OFF_SPIN = -1.5f;
    private static float LEG_SPIN = 1.5f;
    private static float STRAIGHT = 0f;
    
    
    
    public void Awake() {
        ballStartPoint = this.transform.position;
        _camera = Camera.main;
        _rigitBody = ballObject.GetComponent<Rigidbody>();
        _rigitBody.useGravity = false;
    }
    public void Start() {
        bowlButton.OnBowlButtonClicked += ThrowBall;
    }

    private void ThrowBall(object sender, BowlButtonBehaviour.OnBowlButtonClickedArgs e) {
        
        Debug.Log("speed"+bowlingSpeed);

        float speedMultiplier = 0.15f;
        if(bowlingSpeed<100){
            bowlingSpeed = 100;
        }

        float speed = (float)bowlingSpeed*speedMultiplier;
        Vector3 distance = marker.transform.position - transform.position;

        float gSquared = Physics.gravity.sqrMagnitude;
        float b = speed * speed + Vector3.Dot(distance, Physics.gravity);    
        float discriminant = b * b - gSquared * distance.sqrMagnitude;

        float discRoot = Mathf.Sqrt(discriminant);
        float bowlTime = Mathf.Sqrt((b - discRoot) * 1f / gSquared);
        // float bowlTime = Mathf.Sqrt(Mathf.Sqrt(distance.sqrMagnitude * 2f/gSquared));
        // float bowlTime = Mathf.Sqrt((b + discRoot) * 2f / gSquared);


        Vector3 velocity = distance / bowlTime - Physics.gravity * bowlTime / 2f;

        _rigitBody.AddForce(velocity, ForceMode.VelocityChange);        
        _rigitBody.useGravity = true;
    }

    void OnCollisionEnter(Collision collision) {

        int randomIndex = Random.Range(1, 4);
        float spinDir = STRAIGHT;
        if(randomIndex == 2) spinDir = LEG_SPIN;
        if(randomIndex == 3) spinDir = OFF_SPIN;

        _rigitBody.AddForce(new Vector3(spinDir, 0, 0), ForceMode.Impulse);
    }

    public void SetBowlingSpeed(string speed) {
        if(int.Parse(speed) > 100) {
            bowlingSpeed = int.Parse(speed);
            
        }
    }
}
