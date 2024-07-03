using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpotifyDashboard.Server.Models.Dashboard
{
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
    }
}
