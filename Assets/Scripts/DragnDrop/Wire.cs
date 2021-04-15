using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    
    Canvas canvas;
    //public bool isDrawing = false;
    RectTransform rectTransform;
    Vector3 mousePos;
    float height;
    float distance;
    GameObject simArea;
    public Transform[] childs;
    public bool isDrawing=false;
    bool mouseSwitch = true;
    

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        simArea = GameObject.FindGameObjectWithTag("SimArea");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDrawing)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            distance = Mathf.Pow(Mathf.Pow(mousePos.x-transform.position.x,2)+
                Mathf.Pow(mousePos.y - transform.position.y, 2),0.5f)/canvas.transform.localScale.x;
            childs[2].transform.localPosition = new Vector3(0,-distance,childs[2].transform.localPosition.z);
            height = Vector2.Distance(gameObject.transform.position,childs[2].transform.position)
                /canvas.transform.localScale.x;
            rectTransform.sizeDelta = new Vector2(rectTransform.rect.width, height);
            childs[1].transform.localPosition = new Vector3(0, -height / 2, 
                childs[1].transform.localPosition.z);

            Vector2 angleVector = new Vector2(mousePos.x - transform.position.x,
                mousePos.y - transform.position.y);
            
            if (angleVector.x < 0)
            {
                float angle = Mathf.Atan(-angleVector.y / angleVector.x) * (180 / Mathf.PI) + 90;
                transform.eulerAngles = new Vector3(0, 0, -angle);
                childs[2].transform.eulerAngles= childs[2].transform.eulerAngles*0.5f;
            }
            else
            {
                float angle = Mathf.Atan(angleVector.y / angleVector.x) * (180 / Mathf.PI) + 90;
                transform.eulerAngles = new Vector3(0, 0, angle);
                childs[2].transform.eulerAngles= childs[2].transform.eulerAngles*0.5f;

            }

            if (Input.GetMouseButton(0) && mouseSwitch)
            {
                mouseSwitch = false;
                childs[2].GetComponent<WireNode>().colliderCheck = true;
            }
            else
            {
                mouseSwitch = true;
            }

            if (childs[2].GetComponent<WireNode>().colliderCheck)
            {
                childs[2].GetComponent<CircleCollider2D>().radius += 1000 * Time.deltaTime;
            }
        }

    }

    public void Draw()
    {
        if (!isDrawing)
        {
            isDrawing = true;
            childs = GetComponentsInChildren<Transform>();
            rectTransform = childs[1].GetComponent<RectTransform>();


        }
    }

    


}
