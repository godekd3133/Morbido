using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceScroll : MonoBehaviour
{
    [SerializeField]
    RectTransform ScrollObject01;

    [SerializeField]
    RectTransform ScrollObject02;

    [SerializeField]
    RectTransform Bottom;

    [SerializeField]
    RectTransform Top;

    [SerializeField]
    CenterLine CenterLine;

    Vector3 OldPos = Vector3.zero;
    Vector3 Pos = Vector3.zero;


   public  float Power;
    bool Scroll = false;
    float fTime = 0f;
    float ScrollTime = 0f;
    bool Bounce = false;
    float Dis;

    // Start is called before the first frame update
    void Start()
    {
        Power = -5;
    }

    // Update is called once per frame
    void Update()
    {
        //  Power *= 0.99f;
        OldPos = Pos;
        Pos = Input.mousePosition;
        if (Input.GetMouseButton(0))
        {

            float delta =  OldPos.y - Pos.y;
            Scroll_By(delta);
            Scroll = true;
        }
        Power *= 0.99f;

        if (Scroll == true)
        {
            ScrollTime += Time.deltaTime;

            if(ScrollTime > 0.5f && Mathf.Abs(Power) < 0.4f)
            {
                if (Bounce == false)
                {
                    Bounce = true;
                    Dis = CenterLine.transform.position.y - CenterLine.CollisionObject.position.y;
                }
              
            }

            if(Bounce == false)
            Scroll_Move(Power * Time.deltaTime);
            else
            {
                //n의 길이를 s초동안 이동
                float temp = Dis;
                fTime += Time.deltaTime;
                Dis = Mathf.SmoothStep(temp, 0, fTime/ 2f);

                Scroll_Move(Dis - temp);

                if (Mathf.Abs(Dis) < 0.1f)
                {
                    Bounce = false;
                    Scroll = false;
                    Power = 0f;
                    fTime = 0f;
                    ScrollTime = 0f;
                }
            }
            
        }

    }
    public void Scroll_Move(float _Distance)
    {
        ScrollObject01.transform.position += Vector3.down * _Distance;
        ScrollObject02.transform.position += Vector3.down * _Distance;

        if (ScrollObject01.anchoredPosition.y < Bottom.anchoredPosition.y)
        {
            ScrollObject01.anchoredPosition += new Vector2(0, 2000);
        }
        if (ScrollObject02.anchoredPosition.y < Bottom.anchoredPosition.y)
        {
            ScrollObject02.anchoredPosition += new Vector2(0, 2000);
        }

        if (ScrollObject01.anchoredPosition.y > Top.anchoredPosition.y)
        {
            ScrollObject01.anchoredPosition -= new Vector2(0, 2000);
        }
        if (ScrollObject02.anchoredPosition.y > Top.anchoredPosition.y)
        {
            ScrollObject02.anchoredPosition -= new Vector2(0, 2000);
        }
    }

    public void Scroll_By(float _Power)
    {
        Power = _Power;
    }

    public void Scroll_To(int _Num)
    {

    }
}
