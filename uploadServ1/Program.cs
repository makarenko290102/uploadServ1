var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.Run(async (context) =>
{
    var response = context.Response; 
    var request = context.Request;

    response.ContentType = "text/html; charset=utf-8";


    if(request.Path=="/upload" && request.Method == "POST")
    {
        IFormFileCollection formFiles = request.Form.Files;

        var uploadPath = $"{Directory.GetCurrentDirectory()}/upload";
        Directory.CreateDirectory(uploadPath);


        foreach(var file in formFiles)
        {
            string fullPath = $"{uploadPath}/{file.FileName}";

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

        }
        await response.WriteAsync("Загружены");
    }
    else
    {
        await response.SendFileAsync("html/index.html");
    }
});

app.Run();


/*
 *1.Определяем конструктор +
 *2.Определяем регистрацию и отправку ответа в самом начале метода+
 *3.устанавливаем кодировку+
 *4. вводим условие+
 *5. вводим объект который получает коллекцию загруженных файлов+
 *с помощью свойства Request.Form.Files, которое представляет тип 
 *IFormFileCooection+
 *6. Далее определяем каталог для загружаемых файлов, если такой папки нет, то создаем её +
 *7. Затем перебираем коллекцию файлов, установив путь к папке загруженных файлов.  
 *8. Для копирования файлов в нужный каталог, создается поток FileStream, в который записывается файл с помощью метода CopyToAsync+
 *Если запрос идет по другому адресу или не представляет тип post то отправляем index.html +
 * 
 */
