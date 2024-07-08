export interface ListItem{

    // Common format model for all elements of a list in the dashboard
    // They all have the same structure, but with differents value 
    // so we can use e general model to represent them

    mainText: string
    secondText: string 
    image: string
    spotifyUrl: string
}