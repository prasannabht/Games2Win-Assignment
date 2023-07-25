using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MarkerBehaviour : MonoBehaviour {

    [SerializeField] private GameObject minMarkerPoint;
    [SerializeField] private GameObject maxMarkerPoint;

    private Vector3 mOffset;
    private float mZCoord;

    public Vector3 GetMarkerPosition() {
        return transform.position;
    }
    
    public void SetMarkerPosition(Vector3 markerPosition) {
        transform.position = markerPosition;
    }

    void OnMouseDown() {
        
        mZCoord = Camera.main.WorldToScreenPoint(transform.position).z;

        mOffset = transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos() {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag() {
        
        Vector3 newPos = GetMouseWorldPos() + mOffset;
        Vector3 newPosXZ = new Vector3(newPos.x, transform.position.y, newPos.z);

        if(newPosXZ.x < minMarkerPoint.transform.position.x + 0.5f ||
            newPosXZ.x > maxMarkerPoint.transform.position.x - 0.5f ||
            newPosXZ.z > minMarkerPoint.transform.position.z - 0.5f ||
            newPosXZ.z < maxMarkerPoint.transform.position.z + 0.5f) {
                //do nothing
        } else {
            transform.position = newPosXZ;
        }
        
        
    }

}
