using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Jauge : MonoBehaviour
{
    public Transform                mask = null;
    public float                    emptyX = 0;
    public float                    fullX = 0;
    public float                    initialTotal = 1;
    public float                    initialJauge = 1;
    public float                    speed = 1;
    public TMPro.TextMeshProUGUI    text = null;
    public Color                    basic = Color.white;
    public Color                    enough = Color.black;
    public float                    treshold = 0;

    private float           range = 0;
    private float           total = 0;
    private float           portion = 0;
    private Vector3         maskTarget = Vector3.zero;
    private SpriteRenderer  color = null;

    void Start()
    {
        this.range = this.fullX - this.emptyX;
        this.color = this.GetComponent<SpriteRenderer>();
        this.Total = initialTotal;
    }

    void Update()
    {
        float   diff = this.maskTarget.x - this.mask.localPosition.x;

        if (diff != 0)
        {
            this.mask.Translate(Mathf.Sign(diff) * this.speed * Time.deltaTime, 0, 0);
            if (Mathf.Abs(this.maskTarget.x - this.mask.localPosition.x) < 0.01)
                this.mask.localPosition = this.maskTarget;
            if ((this.mask.localPosition.x - this.emptyX) / this.range >= this.treshold / this.total)
                this.color.color = this.enough;
            else
                this.color.color = this.basic;
        }
    }

    public float Portion
    {
        get
        {
            return (this.portion);
        }
        set
        {
            this.portion = Math.Clamp(value, 0, this.total);
            this.text.text = Mathf.Round(this.portion).ToString() + " / " + this.total.ToString();
            this.maskTarget.x = this.emptyX + this.range * this.portion / this.total;
        }
    }

    public float Total
    {
        get
        {
            return (this.total);
        }
        set
        {
            this.total = value;
            this.text.text = Mathf.Round(this.portion).ToString() + " / " + this.total.ToString();
            this.Portion = this.initialJauge;
            this.mask.localPosition = new Vector3(
                this.maskTarget.x,
                this.mask.localPosition.y,
                this.mask.localPosition.z
            );
            this.maskTarget = this.mask.localPosition;
            if ((this.mask.localPosition.x - this.emptyX) / this.range >= this.treshold / this.total)
                this.color.color = this.enough;
            else
                this.color.color = this.basic;
        }
    }
}
