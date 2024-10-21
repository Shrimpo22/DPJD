using UnityEngine;

[System.Serializable]

public abstract class Item 
{
   public abstract string GiveName();
     public abstract string getNameWithSpaces();
   public virtual int MaxStacks()
   {
        return 10;
   }

   public virtual Sprite GiveItemImage()
   {
        return Resources.Load<Sprite>("UI/Item Images/No Item Image Icon");
   }


   public abstract string GiveDiscresp();
    


}
