# AspNetCore GraphQL Prototype

Implements the GraphQL for .NET module by Joe McBride with the GraphQL.Server.Ui.Playground interface by Pekka Heikura.

Run the solution. Web service is available on port https://localhost:5001/. The GraphQL UI is available at https://localhost:5001/ui/playground

Requires a local MongoDB backend running on http://localhost:27017 for complete functionality.

## Issues

While the service runs and the UI playground provides query execution, the UI schema explorer does not appear to work. There are no reported error messages to help debug the issue.