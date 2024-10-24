using UnityEngine;
using System;

[System.Serializable]

public abstract class Item 
{
   public abstract string GiveName();
   public abstract string getNameWithSpaces();
   public abstract int Stats();
   public virtual int MaxStacks()
   {
        return 10;
   }

   public virtual Sprite GiveItemImage()
   {
        return Resources.Load<Sprite>("UI/Item Images/No Item Image Icon");
   }

   public abstract string GiveDiscresp();

   public abstract Action UseFunc();


}
