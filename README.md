
# Spotify Dashboard 🎧

This application use the Spotify-Api to retreive data and manipulate them to get only the necessary datas to make the widgets work.


## Tech Stack

**Client:** Angular, Typescript

**Server:** C# minimal Api, MongoDb

____
The components that this dashboard contains are the following:

- card (contains):
    - User favourite artist
    - Favourite artist best track
- list (contains):
    - User top tracks
    - Recommended tracks
    - User playlists
- header (contains):
    - Data about the current user
- multi-list (contains):
    - New Releases
    - Artist Albums

### This components are made to be re-usable and resizable editing only some data on the mongodb 

#### Authentication Method

On the [Spotify-Api Authorization Documentation](https://developer.spotify.com/documentation/web-api/concepts/authorization) you will find four different types of OAuth flow to follow. Choose wisely wich authorization flow to use because they all have some differences between one another. This application uses the Implicit Grant Authorization flow, it may not be the best but it works for the calls i need.


### Disclaimer
#### Without a proper mongo database with relative collection the dashboard would not render any component.


## API Reference
The apis call's start from the services on the front-end project and reach the backend dedicated endpoints.

In this version of the projects all the data are retrieved with a single api call to the DashboardEndpoint.

#### Get widgets usefull data

```http
  GET /ServerApi/dashboard/data
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `access_token` | `string` | **Required**. Your access_token |

#### Return an object with all the needed properties of the widgets.

Without the access_token parameter you wont be able to make any api call to the api, so make sure the provided token is in the right format and it's not expired. Here is the link to the [Implicit Grant Flow](https://developer.spotify.com/documentation/web-api/tutorials/implicit-flow) i used that explains to you the workflow and how to use his response.

___

#### Get configuration data
Also, to get the widget configuration u need to make a call to a mongo client.
```http
  GET /ServerApi/dashboard/config
```
The configuration data are used to check if the widget have the correct key-pair parameters in the configuration collection on the db, if the key-pair doesn't match the component will not be shown on the dashboard.

In my case, the key pair will be widgetName - type.

The widgetLabel will be used as a title for the widget.

#### Returns a list of WidgetComponents 
The mongo Output look somethig like this:

`````
{
  "_id": {
    "$oid": "66850572b8956085e90cb0e3"
  },
  "widgetName": "top-ten-songs",
  "widgetProperty": "topTenSongs",
  "widgetLabel": "Favourite Tracks",
  "type": "list"
}
`````





## Authors

- [@LucaBernardis](https://www.github.com/LucaBernardis)

