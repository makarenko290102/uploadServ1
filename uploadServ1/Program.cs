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
        await response.WriteAsync("���������");
    }
    else
    {
        await response.SendFileAsync("html/index.html");
    }
});

app.Run();


/*
 *1.���������� ����������� +
 *2.���������� ����������� � �������� ������ � ����� ������ ������+
 *3.������������� ���������+
 *4. ������ �������+
 *5. ������ ������ ������� �������� ��������� ����������� ������+
 *� ������� �������� Request.Form.Files, ������� ������������ ��� 
 *IFormFileCooection+
 *6. ����� ���������� ������� ��� ����������� ������, ���� ����� ����� ���, �� ������� � +
 *7. ����� ���������� ��������� ������, ��������� ���� � ����� ����������� ������.  
 *8. ��� ����������� ������ � ������ �������, ��������� ����� FileStream, � ������� ������������ ���� � ������� ������ CopyToAsync+
 *���� ������ ���� �� ������� ������ ��� �� ������������ ��� post �� ���������� index.html +
 * 
 */
