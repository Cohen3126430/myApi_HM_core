using Microsoft.AspNetCore.Mvc;
using myApi.models;
using myApi.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Text.Json;
//using Microsoft.AspNetCore.Hosting;
// using Microsoft.Extensions.Hosting;

namespace myApi.Services;

public class GiftServiceJson:IGiftService
{
    List<Gift> Gifts {get;}
    private static string fileName = "gift.json";
    private string filePath;

    public GiftServiceJson(IHostEnvironment env)
    {
       filePath = Path.Combine(env.ContentRootPath, "Data" ,fileName);

        using (var jsonFile = File.OpenText(filePath))
            {
                Gifts = JsonSerializer.Deserialize<List<Gift>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
    }

    public void saveToFile()
    {
        File.WriteAllText(filePath, JsonSerializer.Serialize(Gifts));
    }
    public List<Gift> Get()
    {
        return Gifts;
    }
    public Gift Get(int id) => Gifts.FirstOrDefault(g => g.Id == id);

    public int Insert(Gift newGift)
    {
        if (newGift == null 
            || string.IsNullOrWhiteSpace(newGift.Name) 
            || newGift.Price <= 0)
            {
                return -1;
            }
        var maxId = Gifts.Max(g => g.Id) + 1;
        newGift.Id = maxId;
        Gifts.Add(newGift);
        saveToFile();
        return maxId;
    }
    public bool Update(int id, Gift newGift)
    {
        if(newGift == null 
            || string.IsNullOrWhiteSpace(newGift.Name) 
            || newGift.Price <= 0 ||newGift.Id!=id){
             return false;
        }
        var gift= Gifts.FirstOrDefault(g => g.Id == id);
        gift.Id=newGift.Id;
        gift.Name=newGift.Name;
        gift.Price=newGift.Price;
        gift.Summary=newGift.Summary;
        saveToFile();
        return true;
    }
     public bool Delete(int id){
        var gift= Gifts.FirstOrDefault(g => g.Id == id);
        if(gift==null)
            return false;
        var index=Gifts.IndexOf(gift);
        Gifts.RemoveAt(index);
        saveToFile();
        return true;
     }

}

public static class GiftUtilities
{
    public static void AddGiftJson(this IServiceCollection services){
        services.AddSingleton<IGiftService,GiftServiceJson>();
    }
}
