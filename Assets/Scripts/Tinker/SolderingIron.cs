using System.Collections.Generic;
using UnityEngine;

public class SolderingIron : MonoBehaviour
{
    bool isSoldering = false;
    Vector2 targetPosition;
    Vector2 initialPosition;
    [SerializeField] int movingSpeed=10;
    [SerializeField] float solderingTime=3;
    //int mode = 1; //is coming or going
    float currentWait = 0;
    bool isWaiting=false;
    [SerializeField] GameObject smoke;
    GameObject newSmoke;
    Vector2 finalPosition;

    private void Start()
    {

        initialPosition = transform.position;
        //targetPosition = new Vector2(((float)Screen.width / Screen.height) * Camera.main.GetComponent<Camera>().orthographicSize, initialPosition.y);
        StaticData.isSoldering = true;
        finalPosition = new Vector2(((float)Screen.width / Screen.height) * Camera.main.GetComponent<Camera>().orthographicSize, initialPosition.y);
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;


    }

    private void Update()
    {
        if (isSoldering)
        {
            //float x = transform.position.x;
            //float y = transform.position.y;
            Vector2 pastPosition = new Vector2(transform.position.x, transform.position.y);
            /*if (mode == 1)
            {
                x += movingSpeed * Time.deltaTime * (targetPosition.x - x);
                y += movingSpeed * Time.deltaTime * (targetPosition.y - y);
            }
            if (mode == 2)*/
            //{
                transform.position=Vector2.Lerp(pastPosition, targetPosition, Time.deltaTime*movingSpeed);
                //pastPosition.x += movingSpeed * Time.deltaTime * (targetPosition.x - pastPosition.x);
                //pastPosition.y += movingSpeed * Time.deltaTime * (targetPosition.y- pastPosition.y);
                //print(targetPosition);
                
                if (Vector2.Distance(finalPosition, transform.position) <= 0.1f )
                {
                    DestroySolder();
                }
            //}
            //transform.position = pastPosition;

            if (Vector2.Distance(targetPosition, transform.position) <= 0.05f && !isWaiting && targetPosition != finalPosition) 
            {
                isWaiting = true;
                isSoldering = false;
                newSmoke = Instantiate<GameObject>(smoke);
                newSmoke.transform.position = transform.position;

            }
        }

        if (isWaiting)
        {
            currentWait += Time.deltaTime;
            if (currentWait >= solderingTime)
            {
                if (SolderingIronIcon.noOfSolders.Count == 0)
                {
                    targetPosition = finalPosition;
                    //mode = 2;
                    
                }
                else
                {
                    targetPosition = SolderingIronIcon.noOfSolders.Dequeue();
                    print("target" + targetPosition);
                }
                isSoldering = true;
                Destroy(newSmoke);
                isWaiting = false;
                currentWait = 0;
            }
        }
    }


    public void Solder(Vector2 position)
    {
        targetPosition = position;
        isSoldering = true;
        //print(SolderingIronIcon.noOfSolders.Peek());
    }

    public void DestroySolder()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        StaticData.isSoldering = false;
        Destroy(gameObject);
        if (newSmoke)
        {
            Destroy(newSmoke);
        }
    }

    

    







}
