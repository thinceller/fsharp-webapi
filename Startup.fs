namespace FsharpWebapi
open Microsoft.AspNetCore.Cors.Infrastructure

module ConfigurationCors =
   let ConfigureCors(corsBuilder: CorsPolicyBuilder): unit =
         corsBuilder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials() |> ignore

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.HttpsPolicy;
open Microsoft.AspNetCore.Mvc
open Microsoft.EntityFrameworkCore
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection

open FsharpWebapi.Models
open ConfigurationCors

type Startup private () =
    new (configuration: IConfiguration) as this =
        Startup() then
        this.Configuration <- configuration

    // This method gets called by the runtime. Use this method to add services to the container.
    member this.ConfigureServices(services: IServiceCollection) =
        // Add framework services.
        services.AddCors() |> ignore
        services.AddDbContext<TodoContext>(fun options -> options.UseInMemoryDatabase("Todos") |> ignore) |> ignore
        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2) |> ignore

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IHostingEnvironment) =
        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore
        else
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts() |> ignore

        app.UseCors(Action<CorsPolicyBuilder> ConfigureCors) |> ignore
        app.UseHttpsRedirection() |> ignore
        app.UseMvc() |> ignore

    member val Configuration : IConfiguration = null with get, set
