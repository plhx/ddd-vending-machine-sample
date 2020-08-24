using System;
using Microsoft.Extensions.DependencyInjection;
using VendingMachineSample.Core.Application.UseCases;
using VendingMachineSample.Core.Domain.Models;
using VendingMachineSample.Core.Domain.Repositories;
using VendingMachineSample.Core.Presentation.Controller;
using VendingMachineSample.Core.Presentation.View;
using VendingMachineSample.Infra.Repositories;


namespace VendingMachineSample.Core {
    class Program {
        static void Main(string[] args) {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<I支払機<硬貨>, 硬貨支払機>();
            serviceCollection.AddSingleton<I商品在庫Repository, Memory商品在庫Repository>();
            serviceCollection.AddTransient<I自動販売機<硬貨>, 硬貨自動販売機>();
            serviceCollection.AddTransient<I自動販売機ユーザUseCase, 硬貨自動販売機ユーザUseCase>();
            serviceCollection.AddTransient<I自動販売機Controller, 自動販売機ConsoleController>();
            serviceCollection.AddTransient<I自動販売機Presentation, 自動販売機ConsolePresentation>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            I商品在庫Repository stockRepository = serviceProvider.GetService<I商品在庫Repository>();
            I自動販売機Controller controller = serviceProvider.GetService<I自動販売機Controller>();
            I自動販売機Presentation presentation = serviceProvider.GetService<I自動販売機Presentation>();

            stockRepository.Register商品(new 販売コード("a"), new 販売商品(new 商品名("水"), new 通貨(100)));
            stockRepository.Register商品(new 販売コード("b"), new 販売商品(new 商品名("コーラ"), new 通貨(120)));
            stockRepository.Register商品(new 販売コード("c"), new 販売商品(new 商品名("オレンジジュース"), new 通貨(130)));
            foreach(var code in stockRepository.販売コード一覧())
                stockRepository.Add数量(code, 5);

            while(true) {
                var input = controller.Input();
                var command = controller.Parse(input);
                var result = controller.Execute(command);
                presentation.Print(result);
            }
        }
    }
}
