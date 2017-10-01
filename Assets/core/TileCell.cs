using UnityEngine;
using UnityEngine.UI;
using System;

public class TileCell: MonoBehaviour
{
   
   public int Number { get; set; }
   
   private Text TileText;
   
   private Image TCell;
   
   public void Awake()
   {
      TileText = GetComponentInChildren<Text>();
      TCell = GetComponentInChildren<Image>();
   }

   public void UpdateNumber(int number)
   {
      TileStyle TStyle = TileStyleHolder.Instance.TileStyles[(int)Math.Log(number, 2)];
      if(number != 1){
//      Debug.Log("Updating text to "+TStyle.Number);
        TileText.text = TStyle.Number;
      }
      Number = number;
      TileText.color = TStyle.TextColor;
      TCell.color = TStyle.TileColor;
      
      TileText.enabled = true;
      TCell.enabled = true;
   }
   
    public int getCellNumber()
    {
        return Number;
    }    
}
