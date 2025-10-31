
using LMTestFileParser.Application.Interface;
using LMTestFileParser.Application.Services;
using Microsoft.Extensions.DependencyInjection;
try
{
    var services = new ServiceCollection();

    // Register services
    services.AddScoped<IFileParserService, CSVFileParserService>();
    services.AddScoped(provider =>
    {
        var csvFileParserService = provider.GetRequiredService<IFileParserService>();
        return new App(csvFileParserService, args);
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