# UrlShortener
## Azure Function App

This is a URL shortening Azure Function app that demonstrates how to create an HTTP-triggered function using C#. The function receives queries and returns a short URL for the original, or if you pass the short URL, it returns the original.

The idea behind this app is to allow users to create a short URL for a longer one, making it easier to share and remember. When the user passes the short URL, the app will redirect them to the original URL.

Frontend Application Repository: [Here](https://github.com/theodosiskats/UrlShortener-FrontEnd)

Live Demo: [Here](https://tkurl.herokuapp.com/)

## Prerequisites

Before deploying this app, you'll need the following:

- An Azure subscription
- Visual Studio (if you're deploying from your local machine)
- The Azure Functions extension for Visual Studio (if you're deploying from your local machine)
- The Azure CLI (if you're deploying from the Azure portal)

## Setting up MongoDB Atlas

To set up a MongoDB Atlas cluster, follow these steps:

1. Sign up for a free MongoDB Atlas account at https://www.mongodb.com/cloud/atlas/register.
2. Create a new project and cluster.
3. In the cluster settings, add a new IP address to the whitelist to allow access from your Azure Function app.
4. Create a new database and collection for storing the short URL mappings.
5. In the `local.settings.json` file (or in the Azure Portal application settings), set the `MongoDBAtlasConnectionString` value to your MongoDB Atlas connection string. The connection string should have the following format:

```
mongodb+srv://<username>:<password>@<clustername>.mongodb.net/<database>?retryWrites=true&w=majority
```

Replace `<username>`, `<password>`, `<clustername>`, and `<database>` with your MongoDB Atlas credentials and cluster information.

**Note:** MongoDB Atlas provides a free tier with limited storage and features, which should be sufficient for this app.

## Deploying from Visual Studio

To deploy the app from Visual Studio, follow these steps:

1. Open the solution file (`.sln`) in Visual Studio.
2. Right-click on the project in the Solution Explorer and select "Publish".
3. In the "Publish" window, select "Azure" as the target.
4. Select "Azure Functions" as the type of target.
5. Choose or create a new Azure Function App to deploy the function to.
6. Follow the prompts to complete the deployment.
7. Go to "Deploying from the Azure portal" step 6, to setup MongoDB Connection String.

## Deploying from the Azure portal

To deploy the app from the Azure portal, follow these steps:

1. Navigate to the Azure portal and sign in to your account.
2. Create a new Azure Function App or select an existing one.
3. Select "Functions" from the left-hand menu.
4. Click "New Function" and choose "HTTP trigger" as the function type.
5. Follow the prompts to create the function.
6. Click the "Configuration" tab.
7. Under "Application settings", click the "New application setting" button.
8. Enter `MongoDBAtlasConnectionString` for the name and your MongoDB Atlas connection string for the value.
9. Click "OK" to save the setting.
10. Once the function is created, you can test it from within the portal or via a web browser.

## Conclusion

That's it! You now have a functioning URL shortener Azure Function app with MongoDB Atlas as your backend database. If you have any questions or issues with the app, please refer to the official Azure Functions and MongoDB Atlas documentation or reach out to the Azure support team.