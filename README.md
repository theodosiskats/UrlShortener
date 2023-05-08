# UrlShortener
## Azure Function App

This is a URL shortening Azure Function app that demonstrates how to create an HTTP-triggered function using C#. The function receives queries and returns a short URL for the original, or if you pass the short URL, it returns the original.

The idea behind this app is to allow users to create a short URL for a longer one, making it easier to share and remember. When the user passes the short URL, the app will redirect them to the original URL.

Live Demo: http.

## Prerequisites

Before deploying this app, you'll need the following:

- An Azure subscription
- Visual Studio (if you're deploying from your local machine)
- The Azure Functions extension for Visual Studio (if you're deploying from your local machine)
- The Azure CLI (if you're deploying from the Azure portal)

## Deploying from Visual Studio

To deploy the app from Visual Studio, follow these steps:

1. Open the solution file (`.sln`) in Visual Studio.
2. Right-click on the project in the Solution Explorer and select "Publish".
3. In the "Publish" window, select "Azure" as the target.
4. Select "Azure Functions" as the type of target.
5. Choose or create a new Azure Function App to deploy the function to.
6. Follow the prompts to complete the deployment.

## Deploying from the Azure portal

To deploy the app from the Azure portal, follow these steps:

1. Navigate to the Azure portal and sign in to your account.
2. Create a new Azure Function App or select an existing one.
3. Select "Functions" from the left-hand menu.
4. Click "New Function" and choose "HTTP trigger" as the function type.
5. Follow the prompts to create the function.
6. Once the function is created, you can test it from within the portal or via a web browser.

## Conclusion

That's it! You now have a functioning Azure Function app that can be triggered via HTTP requests. If you have any questions or issues with the deployment process, please refer to the official Azure Functions documentation or reach out to the Azure support team.
