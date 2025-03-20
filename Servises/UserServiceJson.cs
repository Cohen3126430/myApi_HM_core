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

public class UserServiceJson:IUserService
{
    List<User> Users {get;}
    private static string fileName = "user.json";
    private string filePath;

    public UserServiceJson(IHostEnvironment env)
    {
       filePath = Path.Combine(env.ContentRootPath, "Data" ,fileName);
        System.Console.WriteLine(filePath);
        using (var jsonFile = File.OpenText(filePath))
            {
                Users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        System.Console.WriteLine(Users.ToString());
    }

    public void saveToFile()
    {
        File.WriteAllText(filePath, JsonSerializer.Serialize(Users));
    }
    public List<User> Get()
    {
        System.Console.WriteLine("in get of UserServiceJson/////////");
        return Users;
    }
    public User Get(int id) => Users.FirstOrDefault(u => u.Id == id);

    public int Insert(User newUser)
    {
        if (newUser == null 
            || string.IsNullOrWhiteSpace(newUser.Name) 
            || string.IsNullOrWhiteSpace(newUser.Password)
            || newUser.PermissionLevel >2 || newUser.PermissionLevel <=0)
            {
        Console.WriteLine(Users);
                return -1;
            }
        var maxId = Users.Max(u => u.Id) + 1;
        newUser.Id = maxId;
        Console.WriteLine(Users);
        Users.Add(newUser);
        Console.WriteLine(newUser);
        saveToFile();
        return maxId;
    }
    public bool Update(int id, User newUser)
    {
         if (newUser == null 
            || string.IsNullOrWhiteSpace(newUser.Name) 
            || string.IsNullOrWhiteSpace(newUser.Password)
            || newUser.PermissionLevel >2 || newUser.PermissionLevel <=0
            || newUser.Id!=id){
             return false;
        }
        var user= Users.FirstOrDefault(u => u.Id == id);
        user.Name=newUser.Name;
        user.Password=newUser.Password;
        user.PermissionLevel=newUser.PermissionLevel;
        saveToFile();
        return true;
    }
     public bool Delete(int id){
        var user= Users.FirstOrDefault(u => u.Id == id);
        if(user==null)
            return false;
        var index=Users.IndexOf(user);
        Users.RemoveAt(index);
        saveToFile();
        return true;
     }
}

public static class UserUtilities
{
    public static void AddUserJson(this IServiceCollection services){
        services.AddSingleton<IUserService,UserServiceJson>();
    }
}
