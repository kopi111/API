using Infrastructure.serviceInjection;



namespace zDataApi.serviceInjection
{

    public static class ProgramExtensions
    {
        public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
        {
            // Add services to the container.
       
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddInfrastructureServices();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name"));
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            return builder; // Ensure you return the builder at the end.
        }
    }
}
