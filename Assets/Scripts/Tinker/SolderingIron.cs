using System.Collections.Generic;
using UnityEngine;

public class SolderingIron : MonoBehaviour
{
    bool isSoldering = false;
    Vector2 targetPosition;
    Vector2 initialPosition;
    [SerializeField] int movingSpeed=10;
    [SerializeField] float solderingTime=3;
    int mode = 1; //is coming or going
    float currentWait = 0;
    bool isWaiting=false;
    [SerializeField] GameObject smoke;
    GameObject newSmoke;


    private void Start()
    {

        initialPosition = transform.position;
        //targetPosition = new Vector2(((float)Screen.width / Screen.height) * Camera.main.GetComponent<Camera>().orthographicSize, initialPosition.y);
        StaticData.isSoldering = true;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        
        
    }

    private void Update()
    {
        if (isSoldering)
        {
            float x = transform.position.x;
            float y = transform.position.y;
            if (mode == 1)
            {
                x += movingSpeed * Time.deltaTime * (targetPosition.x - x);
                y += movingSpeed * Time.deltaTime * (targetPosition.y - y);
            }
            if (mode == 2)
            {
                targetPosition= new Vector2(((float)Screen.width / Screen.height) * Camera.main.GetComponent<Camera>().orthographicSize, initialPosition.y);
                x += movingSpeed * Time.deltaTime * (targetPosition.x - x);
                y += movingSpeed * Time.deltaTime * (targetPosition.y-y);
                
                if (Vector2.Distance(targetPosition, transform.position) <= 0.1f)
                {
                    DestroySolder();
                }
            }
            transform.position = new Vector2(x, y);

            if (Vector2.Distance(targetPosition, transform.position) <= 0.1f && !isWaiting)
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
                mode = 2;
                isSoldering = true;
                Destroy(newSmoke);
                //isWaiting = false;
                //currentWait = 0;
            }
        }
    }


    public void Solder(Vector2 position/*,Vector2 initialPosition,Vector3 finalPosition*/)
    {
        targetPosition = position;
        isSoldering = true;
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
