using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    
    
    private Vector3 temp;
    private TransformHolder _transformHolder;
    
    Ray ray;
    RaycastHit hit;
    
    private bool swipe = false;
    public static bool onProgress=false;
    private bool swipeProgress=false;
    private Vector3 rotationVector;
    private bool locBool=true;
    private GameObject hitObject;
    private BoxCollider _boxCollider;
    private int childCount;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _transformHolder = GetComponent<TransformHolder>();
    }
    
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            if (hit.collider.name == this.name)
            {
                if (onProgress==false)
                {
                    swipe = true;
                }
            }
            else
            {
                if (!onProgress)
                {
                    swipe = false;
                }
            }
        }
        if (swipe)
        {
            Swipe();
        }

        if (swipeProgress)
        {
            move();
            if (locBool)
            {
                StartCoroutine(Wait(0.4f));
            }
        }
    }
    
   
    public void Swipe()
    {
         if(Input.GetMouseButtonDown(0))
         {
             onProgress = true;
             
            firstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
         }
         if(Input.GetMouseButtonUp(0))
         {
            secondPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
           
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
               
            currentSwipe.Normalize();
     
           
            if(currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                
                if (checkForward() !=Vector3.zero)
                { 
                    temp = new Vector3(checkForward().x,checkForward().y+(0.30f)+transform.childCount*0.30f + hitObject.transform.childCount*0.30f,checkForward().z);
                    rotationVector = new Vector3(-180,0,0);
                    swipeProgress = true;
                }
            }
         
            if(currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                if (checkBack() !=Vector3.zero)
                {
                    temp = new Vector3(checkBack().x,checkBack().y+(0.30f +transform.childCount*0.30f + hitObject.transform.childCount*0.30f),checkBack().z);
                    rotationVector = new Vector3(180,0,0);
                    swipeProgress = true;
                }
            }
          
            if(currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                
                if (checkLeft()!=Vector3.zero)
                {
                    temp = new Vector3(checkLeft().x,checkLeft().y+(0.30f +transform.childCount*0.30f + hitObject.transform.childCount*0.30f),checkLeft().z);
                    rotationVector = new Vector3(0,0,-180);
                    swipeProgress = true;
                }
            }
           
            if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                if (checkRight() !=Vector3.zero)
                {
                    temp = new Vector3(checkRight().x,checkRight().y+(0.30f +transform.childCount*0.30f + hitObject.transform.childCount*0.30f),checkRight().z);
                    rotationVector = new Vector3(0,0,180);
                    swipeProgress = true;
                }
            }

            swipe = false;
            onProgress = false;
         }
    }




    public Vector3 checkLeft()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 2))
        {
         
          //this.transform.parent = hit.collider.transform;
          
          hitObject = hit.collider.gameObject;
          
            return hit.collider.transform.position;
        }
        else
        {
            return Vector3.zero;
        }
    }
    
    public Vector3 checkRight()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 2))
        {
           
           //this.transform.parent = hit.collider.transform;
           hitObject = hit.collider.gameObject;
           return hit.collider.transform.position;
        }
        else
        {
            return Vector3.zero;
        }
    }
    
    public Vector3 checkForward()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2))
        {
            
            //this.transform.parent = hit.collider.transform;
           
            hitObject = hit.collider.gameObject;
            return hit.collider.transform.position;
        }
        else
        {
            return Vector3.zero;
        }
    }
    
    public Vector3 checkBack()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 2))
        {
           
           //this.transform.parent = hit.collider.transform;
           
           
           hitObject = hit.collider.gameObject;
            return hit.collider.transform.position;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public void move()
    {
        transform.position = Vector3.MoveTowards(transform.position, temp, 8f*Time.deltaTime);

       
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(rotationVector), 500 * Time.deltaTime);
        
        
    }
    
    IEnumerator Wait(float waitTime)
    {
        locBool = false;
        yield return new WaitForSeconds(waitTime);
        //Debug.Log("süre geçti");
        swipeProgress = false;
        _boxCollider.enabled = false;
        this.transform.parent = hitObject.transform;
        
        int count = transform.childCount;
        for(int i = 0; i < count; i++)
        {
            transform.GetChild(0).parent=transform.parent;
            
        }
        locBool = true;
        UpdateLocation();
    }

    public void UpdateLocation()
    {
       //Debug.Log("listeye eklenecek transformlar");
        _transformHolder.AddLocation(transform.position);
    }
    
    
    
}
