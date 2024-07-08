import { Artist } from "./artist";
import { Card } from "./card";
import { ListItem } from "./listItem";
import { Track } from "./track";
import { User } from "./user";

export interface DashboardData{
    user: User // User data
    topArtist: Card // data about the user favourite artist
    artistTopTrack: Card // data ofbest song of the user fvaourite artist
    newReleases: ListItem[] // List of new Releases
    artistAlbums: ListItem[] // list of albums from the user's favourite artist
    userTopTracks: ListItem[] // user favourite tracks
    userPlaylists: ListItem[] // list of user playlists
    recommendedTracks: ListItem[] // list of recommended tracks
}