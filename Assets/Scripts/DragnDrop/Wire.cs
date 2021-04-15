using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    [SerializeField] GameObject wire;
    Canvas canvas;
    //public bool isDrawing = false;
    GameObject newWire;
    RectTransform rectTransform;
    Vector3 mousePos;
    float height;
    SimArea simArea;
   
    public bool isDrawing=false;


    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        simArea = GetComponentInParent<SimArea>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDrawing)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            height = Vector2.Distance(mousePos, transform.position)/canvas.transform.localScale.x;
            rectTransform.sizeDelta = new Vector2(rectTransform.rect.width, height);
            newWire.transform.localPosition = new Vector3(0, -height / 2, newWire.transform.localPosition.z);
            Vector2 angleVector = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
            if (angleVector.x < 0)
            {
                float angle = Mathf.Atan(-angleVector.y / angleVector.x) * (180 / Mathf.PI) + 90;
                transform.eulerAngles = new Vector3(0, 0, -angle);
            }
            else
            {
                float angle = Mathf.Atan(angleVector.y / angleVector.x) * (180 / Mathf.PI) + 90;
                transform.eulerAngles = new Vector3(0, 0, angle);
            }
            
        }
        
    }

    public void Draw()
    {
        if (!isDrawing && simArea.selectedNode==null)
        {
            simArea.selectedNode = gameObject;
            isDrawing = true;
            newWire = Instantiate(wire, gameObject.transform.position, Quaternion.identity);
            newWire.transform.SetParent(gameObject.transform);
            newWire.transform.localScale = gameObject.transform.localScale;
            rectTransform = newWire.GetComponent<RectTransform>();
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            height = Vector2.Distance(mousePos, transform.position)/canvas.transform.localScale.x;
            
            rectTransform.sizeDelta = new Vector2(rectTransform.rect.width, height);
        }
        else
        {
            simArea.selectedNode.GetComponent<Wire>().isDrawing = false;
            simArea.selectedNode = null;
            
        }
    }
}
