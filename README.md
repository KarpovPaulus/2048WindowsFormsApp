# 🎮 2048WindowsFormsApp
## 🔢 Копия популярной игры «2048», написанная в процессе изучения технологии Windows Forms, с хранением результатов в формате JSON.

![Изображение](https://upload.wikimedia.org/wikipedia/commons/thumb/4/48/Markdown-mark.svg/1920px-Markdown-mark.svg.png "Логотип Markdown")

## 🔧 Техническая часть
* Проект реализован на платформе Windows Forms.
* Выполнен с соблюдением принципов ООП.
* Для сохранения результатов используется формат JSON.


## 💾 Сериализация
С помощью сериализации и десериализации JSON реализовано сохранение и загрузка результатов игры.
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
