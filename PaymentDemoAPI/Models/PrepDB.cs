using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace PaymentDemoAPI.Models
{
    public class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SendData(serviceScope.ServiceProvider.GetService<PaymentDetailsContext>());
            }
        }

        private static void SendData(PaymentDetailsContext context)
        {
            Console.WriteLine("Applying Migration...");
            context.Database.Migrate();
            if (context.PaymentDetails.Any())
            {
                context.PaymentDetails.AddRange(new PaymentDetail()
                {
                    CardNumber="1234567891234567",
                    CardOwnerName="Ram Nath",
                    ExpirationDate="02/24",
                    SecurityCode="123"
                });
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Already have data -- not seeding");
            }
        }
    }
}
