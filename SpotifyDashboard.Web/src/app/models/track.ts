import { Artist } from "./artist";

export interface Track{
    id: string
    artist: string; // data about the artist who made the track
    name: string; // track name
    image: string; // link to the track album image
    duration: string; // duration in ms of the track ( to be converted )
    spotifyUrl: string; // external link to the spotify page of the track
}