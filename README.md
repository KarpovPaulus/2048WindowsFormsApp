# 🎮 2048WindowsFormsApp
## 🔢 A copy of the popular game "2048", written in the process of learning Windows Forms technology, with results stored in JSON format.


![Изображение](https://github.com/vq11/2048WindowsFormsApp/blob/master/2024-04-26_20-03-21.png)

## 🔧 Technical part

* The project is implemented on the Windows Forms platform.

* Made in compliance with the principles of OOP.

* The JSON format is used to save the results.


## 💾 Serialization
Using JSON serialization and deserialization, you can save and load game results.
~~~ csharp
namespace _2048WindowsFormsApp
{
    public class UserManager
    {
        public static string path = "result.json";
        public static List<User> GetAll()
        {
            if(FileProvider.Exists(path))
            {
                var jsonData = FileProvider.GetValue(path);
                return JsonConvert.DeserializeObject<List<User>>(jsonData);
            }
            return new List<User>();
        }

        public static void Add(User newUser)
        {
            var users = GetAll();
            users.Add(newUser);

            var jsonData = JsonConvert.SerializeObject(users);
            FileProvider.Replace(path, jsonData);
        }
    }
}
~~~~
