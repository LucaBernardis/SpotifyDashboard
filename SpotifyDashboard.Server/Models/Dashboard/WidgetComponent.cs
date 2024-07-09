using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpotifyDashboard.Server.Models.Dashboard
{
    // Class to represent the single tile of the dashboard and his properties
    public class WidgetComponent
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = "";

        [BsonElement("widgetName")]
        public string WidgetName { get; set; } = "";

        [BsonElement("widgetProperty")]
        public string WidgetProperty { get; set; } = "";

        [BsonElement("widgetLabel")]
        public string WidgetLabel { get; set; } = "";

        [BsonElement("type")]
        public string Type { get; set; } = "";

        [BsonElement("width")]
        public int Width { get; set; } = 0;

        [BsonElement("heigth")]
        public int Heigth { get; set; } = 0;

        [BsonElement("position")]
        public string Position { get; set; } = "";


        public WidgetComponent()
        {
            
        }

        public WidgetComponent(string name)
        {
            WidgetName = name;
        }
        
    }
}
