export interface Album{
    id: string
    name: string // album name
    artist: string // name of the artist
    image: string // link to the album image
    spotifyUrl: string // external link to the spotify page of the album
    total_tracks: number
}