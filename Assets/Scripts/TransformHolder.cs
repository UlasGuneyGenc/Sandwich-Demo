using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TransformHolder : MonoBehaviour
{
    private List<Vector3> locations = new List<Vector3>();
    // Start is called before the first frame update
    Vector3 startPosition;
    private BoxCollider _boxCollider;
    public bool temp=false;
    Quaternion  startRotation;
    
    void Start()
    {
        locations.Add(transform.position);
        startPosition = transform.position;
        _boxCollider = GetComponent<BoxCollider>();
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (temp)
        {
            transform.parent = null;
            resetPos();
            if (transform.position==locations[0])
            {
                temp = false;
                _boxCollider.enabled = true;
            }
        }
      //Debug.Log(locations[locations.Count-1]);
      
     
    }
    
    public void AddLocation(Vector3 transform)
    {
            locations.Add(transform);
            //Debug.Log("Yeni yer listeye eklendi");
    }


    public void resetPos()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPosition, 7f*Time.deltaTime);
        transform.rotation = startRotation;

    }

    public void enableMyBool()
    {
        temp = true;
    }
       
}
    

