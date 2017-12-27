# XamChatCosmosOnly
Xamarin Cross Platform Realtime Chat Using Azure Cosmos DB Change Feed

* No WebSockets
* No SignalR

## Benefit
Creating real time chat application without servers, you still need servers to manager your users and CosmosDB collections. But this way make it easy for you to start. Even SignalR, Notification Hubs, or Azure Relay services are ready for Xamarin, you still need to manage a backend database. Using CosmosDB Changes Feed is an easy possible alternative.

## How does it work
CosmosDB Changes Feed is a feature in Azure Cosmos DB, it allows applications to be notified when documents change. The change CosmosDB publishes is the change made by Update and Insert operations. You can chose to write observers against PartionKey Rang allowing your changes to be published to multiple consumers, in this example no partitioning is used. For complete explanation: [Working with the change feed](https://docs.microsoft.com/en-us/azure/cosmos-db/change-feed)

## This example is simplified
The code in this example is simplified, It creates the Collection and the Lease Collection once the app starts, you should change that so a server creates these collection for each room (or chat session), Your server should also create Access Tokens for the users. Also the IoC in this example is simple created by me you can opt for more serious one.

## Where To Start
* For easy code reading, start from DocumentChangeObserverFactory
* Then review DocumentObserverChatClient

## Run the app
* You need CosmosDB account or you install the emulator, the example uses the emulator
* Run the app, then send a message. A Mocked replay will be returned to the UI, this Mock inserts reply in the database, so when it appears in the UI it is coming from the change feed the same way the DocumentObserverChatClient should work.



