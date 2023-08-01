using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCanChargeScript : MonoBehaviour
{
    public bool isWallIN;
    public bool CanCharge;
    public LeftCanChargeScript left;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isWallIN == false && left.isWallIN == false)
        {
            CanCharge = true;
        }
        else
        {
            if(isWallIN==true&& left.isWallIN == false)
            {
                CanCharge = false;
            }
            if (isWallIN == false && left.isWallIN == false)
            {
                CanCharge = false;
            }
            if (isWallIN == false && left.isWallIN == true)
            {
                CanCharge = false;
            }
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tile"))
        {
            isWallIN = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Tile"))
        {
            isWallIN = false;
        }
    }
    //public void CanCharge_() {
    //    if (isWallIN==false && left.isWallIN==false)
    //    {
    //        CanCharge =true;
    //    }
    //    else
    //    {
    //        CanCharge = false; 

    //    }
    //}
}
