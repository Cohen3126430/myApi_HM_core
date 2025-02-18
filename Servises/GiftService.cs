using Microsoft.AspNetCore.Mvc;

namespace myApi.Services;
using myApi.models;

[ApiController]
[Route("[controller]")]
public static class GiftService
{
    private static List<Gift> list;

    static GiftService()
    {
        list = new List<Gift>
        {
            new Gift{Id=1,Name="Jewelry",Price=500,Summary="Jewelry set"},
            new Gift{Id=2,Name="ornaments",Price=400,Summary="decoration shelf"}
        };
    }

    public static List<Gift> Get()
    {
        return list;
    }
    public static Gift Get(int id)
    {
        return list.FirstOrDefault(g => g.Id == id);
    }
    public static int Insert(Gift newGift)
    {
        if (newGift == null 
            || string.IsNullOrWhiteSpace(newGift.Name) 
            || newGift.Price <= 0)
            {
                return -1;
            }
        var maxId = list.Max(g => g.Id) + 1;
        newGift.Id = maxId;
        list.Add(newGift);
        return maxId;
    }
    public static bool Update(int id, Gift newGift)
    {
        if(newGift == null 
            || string.IsNullOrWhiteSpace(newGift.Name) 
            || newGift.Price <= 0 ||newGift.Id!=id)
            {
                return false;
            }
        var gift= list.FirstOrDefault(g => g.Id == id);
        gift.Id=newGift.Id;
        gift.Name=newGift.Name;
        gift.Price=newGift.Price;
        gift.Summary=newGift.Summary;
        return true;
    }
     public static bool Delete(int id){
        var gift= list.FirstOrDefault(g => g.Id == id);
        if(gift==null)
            return false;
        var index=list.IndexOf(gift);
        list.RemoveAt(index);
        return true;
     }

}
