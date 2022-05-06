var serviceProvider = new ServiceProvider();
serviceProvider.AddTransient<AppSettings>();
serviceProvider.AddTransient<IMyCustomService, MyCustomService>();
var serviceInstance = serviceProvider.GetService<IMyCustomService>();
serviceInstance.Run();