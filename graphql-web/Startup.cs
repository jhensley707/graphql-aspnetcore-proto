using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuditLog;
using AuditLog.MongoDB;
using AuditLog.Types;
using GraphQL;
using GraphQL.Http;
using GraphQL.Server;
using GraphQL.Server.Transports.AspNetCore;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace graphql_web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // GraphQL services
            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();

            // Add GraphQL schema objects
            services.AddSingleton<AuditLogData>();
            services.AddSingleton<AuditLogMutation>();
            services.AddSingleton<AuditLogQuery>();

            services.AddSingleton<AuditLogClient>();
            services.AddSingleton<AuditLogClientType>();
            services.AddSingleton<ClientInterface>();

            services.AddSingleton<AuditLogEntry>();
            services.AddSingleton<AuditLogEntryInputType>();
            services.AddSingleton<AuditLogEntryType>();
            services.AddSingleton<AuditLogEntryCollectionType>();
            services.AddSingleton<EntryInterface>();

            services.AddSingleton<ISchema, AuditLogSchema>();

            services.AddTransient<IDataAccess>(s => new MongoDataAccess("mongodb://localhost:27017"));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddGraphQL(_ =>
            {
                _.EnableMetrics = true;
                _.ExposeExceptions = true;
            })
            .AddUserContextBuilder(httpContext => new GraphQLUserContext { User = httpContext.User });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // add http for Schema at default url /graphql
            app.UseGraphQL<ISchema>("/graphql");

            // use graphql-playground at default url /ui/playground
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions
            {
                Path = "/ui/playground"
            });
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
