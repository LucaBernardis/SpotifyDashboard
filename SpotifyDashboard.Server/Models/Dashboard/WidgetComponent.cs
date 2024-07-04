using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpotifyDashboard.Server.Models.Dashboard
{
    // Class to represent the single tile of the dashboard and his properties
    public class WidgetComponent
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("widgetName")]
        public string? WidgetName { get; set; }

        [BsonElement("widgetProperty")]
        public string? Widgetproperty { get; set; }

        [BsonElement("widgetLabel")]
        public string? WidgetLabel { get; set; }

        [BsonElement("type")]
        public string? Type { get; set; }



        public WidgetComponent(string name)
        {
            WidgetName = name;
        }
        public WidgetComponent( string? widgetName, string? widgetproperty, string? widgetLabel)
        {
            WidgetName = widgetName;
            Widgetproperty = widgetproperty;
            WidgetLabel = widgetLabel;
        }
    }
}
