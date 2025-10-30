
using LMTestFileParser.Application.Interface;
using LMTestFileParser.Application.Services;
using Microsoft.Extensions.DependencyInjection;

try
{
    var services = new ServiceCollection();

    // Register services
    services.AddScoped<ICSVFileParserService, CSVFileParserService>();

    services.AddScoped<App>(provider =>
    {
        var messageService = provider.GetRequiredService<ICSVFileParserService>();
        return new App(messageService, args);
    });

    var serviceProvider = services.BuildServiceProvider();

    var app = serviceProvider.GetRequiredService<App>();
    app.Run();

}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
finally
{

}