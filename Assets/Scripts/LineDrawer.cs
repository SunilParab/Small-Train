using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{

    public float startx;
    public float starty;
    public float firstAngle;
    public float endAngle;
    public float endx;
    public float endy;
    public bool making;
    public List<GameObject> lineSegments;
    public GameObject segment;
    public GameObject lineHolder;
    public float segLength = 0.5f;
    bool snapped;

    int startCount;
    int endCount;

    // Lets the Line Draer know if its making a new Train Line
    private int targetLine;

    public static LineDrawer reference;

    void Awake()
    {
        reference = this;
    }

    // Update is called once per frame
    void Update()
    {

        if (making) {

            if (Input.GetMouseButtonUp(0)) {
                making = false;
                if (!snapped) { //Clear out linelist of old segments
                    for (int i = lineSegments.Count - 1; i >= 0; i--) {
                        Destroy(lineSegments[i]);
                        lineSegments.RemoveAt(i);
                    }
                } else { //Actually make line                   
                    LineMake();
                }
                return;
            }

            //Clear out linelist of old segments
            for (int i = lineSegments.Count - 1; i >= 0; i--) {
                Destroy(lineSegments[i]);
                lineSegments.RemoveAt(i);
            }

            if (!snapped) { //Find endpoitn
                Vector2 mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                endx = Mathf.Round(mousePos.x);
                endy = Mathf.Round(mousePos.y);
            }

            //Calculate angle
            float angle = Mathf.Atan2((endy-starty),(endx-startx)) * Mathf.Rad2Deg;
            if (angle < 0) {
                angle += 360;
            }

            //Calculate angle of first and second part
            if (angle >= 337.5 || angle < 22.5) {
                firstAngle = 0;

                if (angle >= 337.5) { //Double check this (ang all the cases with the = part of the greater than or equal)
                    endAngle = 315;
                } else if (angle > 0) {
                    endAngle = 45;
                } else {
                    endAngle = 0;
                }

            } else if (angle >= 22.5 && angle < 67.5) {
                firstAngle = 45;

                if (angle > 45) {
                    endAngle = 90;
                } else if (angle < 45) {
                    endAngle = 0;
                } else {
                    endAngle = 45;
                }

            } else if (angle >= 67.5 && angle < 112.5) {
                firstAngle = 90;

                if (angle > 90) {
                    endAngle = 135;
                } else if (angle < 90) {
                    endAngle = 45;
                } else {
                    endAngle = 90;
                }

            } else if (angle >= 112.5 && angle < 157.5) {
                firstAngle = 135;

                if (angle > 135) {
                    endAngle = 180;
                } else if (angle < 135) {
                    endAngle = 90;
                } else {
                    endAngle = 135;
                }

            } else if (angle >= 157.5 && angle < 202.5) {
                firstAngle = 180;

                if (angle > 180) {
                    endAngle = 225;
                } else if (angle < 180) {
                    endAngle = 135;
                } else {
                    endAngle = 180;
                }

            } else if (angle >= 202.5 && angle < 247.5) {
                firstAngle = 225;

                if (angle > 225) {
                    endAngle = 270;
                } else if (angle < 225) {
                    endAngle = 180;
                } else {
                    endAngle = 225;
                }

            } else if (angle >= 247.5 && angle < 292.5) {
                firstAngle = 270;

                if (angle > 270) {
                    endAngle = 315;
                } else if (angle < 270) {
                    endAngle = 225;
                } else {
                    endAngle = 270;
                }

            } else if (angle >= 292.5 && angle < 337.5) {
                firstAngle = 315;

                if (angle > 315) {
                    endAngle = 0;
                } else if (angle < 315) {
                    endAngle = 270;
                } else {
                    endAngle = 315;
                }

            }



            //Keep adding segments in the first direction until the angle from the newest segment to the endpoint equals the final angle
            float curx = startx;
            float cury = starty;
            float offsetx = 0;
            float offsety = 0;

            switch (firstAngle) {
                case 0:
                    offsetx = segLength / 2;
                    break;
                case 45:
                    offsetx = segLength / 2;
                    offsety = segLength / 2;
                    break;
                case 90:
                    offsety = segLength / 2;
                    break;
                case 135:
                    offsetx = -segLength / 2;
                    offsety = segLength / 2;
                    break;
                case 180:
                    offsetx = -segLength / 2;
                    break;
                case 225:
                    offsetx = -segLength / 2;
                    offsety = -segLength / 2;
                    break;
                case 270:
                    offsety = -segLength / 2;
                    break;
                case 315:
                    offsetx = segLength / 2;
                    offsety = -segLength / 2;
                    break;
            }

            bool firstHalf = true;
            int counter = 0;

            while (firstHalf && (curx != endx || cury != endy) && counter < 100) {
                var curSeg = Instantiate(segment,new Vector3(curx+offsetx,cury+offsety,0), new Quaternion());

                //Set curSeg angle

                lineSegments.Add(curSeg);

                switch (firstAngle) {
                    case 0:
                        curx += segLength;
                        break;
                    case 45:
                        curx += segLength;
                        cury += segLength;
                        break;
                    case 90:
                        cury += segLength;
                        break;
                    case 135:
                        curx -= segLength;
                        cury += segLength;
                        break;
                    case 180:
                        curx -= segLength;
                        break;
                    case 225:
                        curx -= segLength;
                        cury -= segLength;
                        break;
                    case 270:
                        cury -= segLength;
                        break;
                    case 315:
                        curx += segLength;
                        cury -= segLength;
                        break;
                }

                float secondAngle = Mathf.Atan2((endy-cury),(endx-curx)) * Mathf.Rad2Deg;
                if (secondAngle < 0) {
                    secondAngle += 360;
                }
                if (secondAngle == endAngle) {
                    firstHalf = false;
                }

                counter++;
            }

            switch (endAngle) {
                case 0:
                    offsetx = segLength / 2;
                    break;
                case 45:
                    offsetx = segLength / 2;
                    offsety = segLength / 2;
                    break;
                case 90:
                    offsety = segLength / 2;
                    break;
                case 135:
                    offsetx = -segLength / 2;
                    offsety = segLength / 2;
                    break;
                case 180:
                    offsetx = -segLength / 2;
                    break;
                case 225:
                    offsetx = -segLength / 2;
                    offsety = -segLength / 2;
                    break;
                case 270:
                    offsety = -segLength / 2;
                    break;
                case 315:
                    offsetx = segLength / 2;
                    offsety = -segLength / 2;
                    break;
            }


            counter = 0;
            while ((curx != endx || cury != endy) && counter < 100) {
                var curSeg = Instantiate(segment,new Vector3(curx+offsetx,cury+offsety,0), new Quaternion());

                //To-Do Set curSeg rotation angle here

                lineSegments.Add(curSeg);

                switch (endAngle) {
                    case 0:
                        curx += segLength;
                        break;
                    case 45:
                        curx += segLength;
                        cury += segLength;
                        break;
                    case 90:
                        cury += segLength;
                        break;
                    case 135:
                        curx -= segLength;
                        cury += segLength;
                        break;
                    case 180:
                        curx -= segLength;
                        break;
                    case 225:
                        curx -= segLength;
                        cury -= segLength;
                        break;
                    case 270:
                        cury -= segLength;
                        break;
                    case 315:
                        curx += segLength;
                        cury -= segLength;
                        break;
                }
                counter++;
            }

        }

    }

    public void Activate(int targetLine) {
        Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        startx = Mathf.Round(mousePos.x);
        starty = Mathf.Round(mousePos.y);
        making = true;

        this.targetLine = targetLine;
    }

    public void Snap(GameObject target) {
        snapped = true;
        endx = target.transform.position.x;
        endy = target.transform.position.y;
    }

    public void UnSnap() {
        snapped = false;
    }

    void LineMake() {

        //Clear out linelist of old segments
        for (int i = lineSegments.Count - 1; i >= 0; i--) {
            Destroy(lineSegments[i]);
            lineSegments.RemoveAt(i);
        }

        var holder = Instantiate(lineHolder);
        var holderInfo = holder.GetComponent<LineInfo>();
                    

        //Calculate angle
        float angle = Mathf.Atan2((endy-starty),(endx-startx)) * Mathf.Rad2Deg;
        if (angle < 0) {
            angle += 360;
        }

        //Calculate angle of first and second part
        if (angle >= 337.5 || angle < 22.5) {
            firstAngle = 0;

            if (angle >= 337.5) { //Double check this (ang all the cases with the = part of the greater than or equal)
                endAngle = 315;
            } else if (angle > 0) {
                endAngle = 45;
            } else {
                endAngle = 0;
            }

        } else if (angle >= 22.5 && angle < 67.5) {
            firstAngle = 45;

            if (angle > 45) {
                endAngle = 90;
            } else if (angle < 45) {
                endAngle = 0;
            } else {
                endAngle = 45;
            }

        } else if (angle >= 67.5 && angle < 112.5) {
            firstAngle = 90;

            if (angle > 90) {
                endAngle = 135;
            } else if (angle < 90) {
                endAngle = 45;
            } else {
                endAngle = 90;
            }

        } else if (angle >= 112.5 && angle < 157.5) {
            firstAngle = 135;

            if (angle > 135) {
                endAngle = 180;
            } else if (angle < 135) {
                endAngle = 90;
            } else {
                endAngle = 135;
            }

        } else if (angle >= 157.5 && angle < 202.5) {
            firstAngle = 180;

            if (angle > 180) {
                endAngle = 225;
            } else if (angle < 180) {
                endAngle = 135;
            } else {
                endAngle = 180;
            }

        } else if (angle >= 202.5 && angle < 247.5) {
            firstAngle = 225;

            if (angle > 225) {
                endAngle = 270;
            } else if (angle < 225) {
                endAngle = 180;
            } else {
                endAngle = 225;
            }

        } else if (angle >= 247.5 && angle < 292.5) {
            firstAngle = 270;

            if (angle > 270) {
                endAngle = 315;
            } else if (angle < 270) {
                endAngle = 225;
            } else {
                endAngle = 270;
            }

        } else if (angle >= 292.5 && angle < 337.5) {
            firstAngle = 315;

            if (angle > 315) {
                endAngle = 0;
            } else if (angle < 315) {
                endAngle = 270;
            } else {
                endAngle = 315;
            }

        }



        //Keep adding segments in the first direction until the angle from the newest segment to the endpoint equals the final angle
        float curx = startx;
        float cury = starty;
        float offsetx = 0;
        float offsety = 0;

        switch (firstAngle) {
            case 0:
                offsetx = segLength / 2;
                break;
            case 45:
                offsetx = segLength / 2;
                offsety = segLength / 2;
                break;
            case 90:
                offsety = segLength / 2;
                break;
            case 135:
                offsetx = -segLength / 2;
                offsety = segLength / 2;
                break;
            case 180:
                offsetx = -segLength / 2;
                break;
            case 225:
                offsetx = -segLength / 2;
                offsety = -segLength / 2;
                break;
            case 270:
                offsety = -segLength / 2;
                break;
            case 315:
                offsetx = segLength / 2;
                offsety = -segLength / 2;
                break;
        }

        bool firstHalf = true;
        int counter = 0;

        while (firstHalf && (curx != endx || cury != endy) && counter < 100) {
            var curSeg = Instantiate(segment,new Vector3(curx+offsetx,cury+offsety,0), new Quaternion());

            //Set curSeg angle

            lineSegments.Add(curSeg);

            switch (firstAngle) {
                case 0:
                    curx += segLength;
                    break;
                case 45:
                    curx += segLength;
                    cury += segLength;
                    break;
                case 90:
                    cury += segLength;
                    break;
                case 135:
                    curx -= segLength;
                    cury += segLength;
                    break;
                case 180:
                    curx -= segLength;
                    break;
                case 225:
                    curx -= segLength;
                    cury -= segLength;
                    break;
                case 270:
                    cury -= segLength;
                    break;
                case 315:
                    curx += segLength;
                    cury -= segLength;
                    break;
            }

            float secondAngle = Mathf.Atan2((endy-cury),(endx-curx)) * Mathf.Rad2Deg;
            if (secondAngle < 0) {
                secondAngle += 360;
            }
            if (secondAngle == endAngle) {
                firstHalf = false;
            }

            counter++;
        }

        startCount = counter;

        switch (endAngle) {
            case 0:
                offsetx = segLength / 2;
                break;
            case 45:
                offsetx = segLength / 2;
                offsety = segLength / 2;
                break;
            case 90:
                offsety = segLength / 2;
                break;
            case 135:
                offsetx = -segLength / 2;
                offsety = segLength / 2;
                break;
            case 180:
                offsetx = -segLength / 2;
                break;
            case 225:
                offsetx = -segLength / 2;
                offsety = -segLength / 2;
                break;
            case 270:
                offsety = -segLength / 2;
                break;
            case 315:
                offsetx = segLength / 2;
                offsety = -segLength / 2;
                break;
        }


        counter = 0;
        while ((curx != endx || cury != endy) && counter < 100) {
            var curSeg = Instantiate(segment,new Vector3(curx+offsetx,cury+offsety,0), new Quaternion());

            //To-Do Set curSeg rotation angle here

            lineSegments.Add(curSeg);

            switch (endAngle) {
                case 0:
                    curx += segLength;
                    break;
                case 45:
                    curx += segLength;
                    cury += segLength;
                    break;
                case 90:
                    cury += segLength;
                    break;
                case 135:
                    curx -= segLength;
                    cury += segLength;
                    break;
                case 180:
                    curx -= segLength;
                    break;
                case 225:
                    curx -= segLength;
                    cury -= segLength;
                    break;
                case 270:
                    cury -= segLength;
                    break;
                case 315:
                    curx += segLength;
                    cury -= segLength;
                    break;
            }
            counter++;
        }

        endCount = counter;

        //Set the variables for the lineHolder

        holderInfo.firstAngle = firstAngle;
        holderInfo.endAngle = endAngle;
        holderInfo.startCount = startCount;
        holderInfo.endCount = endCount;
        for (int i = lineSegments.Count - 1; i >= 0; i--) {
            lineSegments[i].transform.SetParent(holder.transform);
            lineSegments.RemoveAt(i);
        }

        TrainList.reference.addSegment(holderInfo,targetLine);

    }

}
