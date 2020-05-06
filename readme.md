# Getting started

1. Start the server: `dotnet -p src/server`
2. Test that it's running: hit the endpoint `http://locahost:8085/api/init` - it should return `Value : 42`
3. Start the client: `npx webpack-dev-server`
4. Test that it's running: hit the endpoint `http://localhost:8080` - it should server the SAFE starter application.

# What's different from the standard SAFE template?
* Slimmed down webpack (with limited HMR support).
* Slimmed down application with no optional data on the model to simplify things.
* Example of "raw" CSS and styling.
* Tidied up formatting of Fable React usages.