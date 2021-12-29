using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public  List<Transform> Checkpoints;
     Transform start , end ;
     int from, to , current;
    public  MapManager manager;
    public  GameObject tracker  ;
      Transform PointA ,PointB ;
     float speedfactor;
     float time = 0;
    
    public ButtonFunctionWrapper wrap;
     float animationTime = 0;
    bool buttonpressed = false;
    bool endreached = false;
    bool roadReached = false;
    bool firstiteration = false;
    public enum direction
    {
        Clockwise,
        CounterClockwise,
    };
    public direction direc = direction.Clockwise;
    private void Awake()
    {
        PointA = tracker.transform;
            PointB = tracker.transform;
        start = tracker.transform;
        if (PrevCurrScene.curr == 0)
        {
            from = 2;
        }
        else if(PrevCurrScene.curr == 1)
        {
            from = 1;
        }
        else if (PrevCurrScene.curr == 2)
        {
            from = 3;
        }
        else if (PrevCurrScene.curr == 3)
        {
            from = 5;
        }
        else if (PrevCurrScene.curr == 4)
        {
            from = 6;
        }
        else if (PrevCurrScene.curr == 5)
        {
            from = 7;
        }

    }
    public void Animate()
    {
        
        end = wrap.end;
        
        to = wrap.to;
        if (((from == 1 || from == 2) && to == 7) || ((to == 1 || to == 2) && from == 7))
        {
            direc = direction.CounterClockwise;
        }
        if(wrap.mode== ButtonFunctionWrapper.modeOfTransportation.Cab)
        {
            speedfactor = 1;
        }
        else
        {
            speedfactor = 0.5f;
        }
        PointA = start;
        PointB = Checkpoints[from]; 
        current = from;
        buttonpressed = true;
    }
    public void LerpPointToPoint(Transform pointa, Transform pointb)
    {

        PointA = pointa.transform;
        PointB = pointb.transform;
        time = 0;
        
    }
    int mod(int x, int m)
    {
        return (x % m + m) % m;
    }

    private void Update()
    {
        tracker.transform.position = Vector3.Lerp(PointA.position, PointB.position, time);
        time += speedfactor * Time.deltaTime;
        animationTime += Time.deltaTime;
        print(current);
        print(roadReached);
        print(endreached);
        if (buttonpressed)
        {
            if (direc == direction.Clockwise)
            {
               
                    if (tracker.transform.position == PointB.position  )
                    {
                    if (!endreached)
                    {
                        if (roadReached)
                        {
                            if (!firstiteration)
                            {
                                current = mod((current + 1) , (Checkpoints.Count));
                            }
                            else
                            {
                                firstiteration = false;
                            }
                            
                            if (current != to)
                            {
                                LerpPointToPoint(Checkpoints[current], Checkpoints[mod((current + 1), (Checkpoints.Count))]);
                            }
                            else
                            {
                                LerpPointToPoint(Checkpoints[to], end);
                                endreached = true;
                            }
                        }
                        else
                        {
                            if (current != to)
                            {
                                LerpPointToPoint(Checkpoints[current], Checkpoints[mod((current + 1), (Checkpoints.Count))]);
                            }
                            else
                            {
                                LerpPointToPoint(Checkpoints[to], end);
                                endreached = true;
                            }
                            roadReached = true;
                        }
                            
                        
                        

                    }
                    else
                    {
                        if(tracker.transform.position == end.position)
                        {
                            manager.mapscenechange(wrap, animationTime);
                        }
                        
                    }
                        
                    }
                }
            else
            {
                if (tracker.transform.position == PointB.position)
                {
                    if (!endreached)
                    {
                        if (roadReached)
                        {
                            if (!firstiteration)
                            {

                                current = mod((current - 1),(Checkpoints.Count));
                            }
                            else
                            {
                                firstiteration = false;
                            }

                            if (current != to)
                            {
                                LerpPointToPoint(Checkpoints[current], Checkpoints[mod((current - 1), (Checkpoints.Count))]);
                            }
                            else
                            {
                                LerpPointToPoint(Checkpoints[to], end);
                                endreached = true;
                            }
                        }
                        else
                        {
                            if (current != to)
                            {
                                LerpPointToPoint(Checkpoints[current], Checkpoints[mod((current - 1), (Checkpoints.Count))]);
                            }
                            else
                            {
                                LerpPointToPoint(Checkpoints[to], end);
                                endreached = true;
                            }
                            roadReached = true;
                        }




                    }
                    else
                    {
                        if (tracker.transform.position == end.position)
                        {
                            manager.mapscenechange(wrap, animationTime);
                        }

                    }

                }

            }
            }
            }
           
        }
    


