using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpotifyDashboard.Server.Models.Dashboard
{
    /// <summary>
    /// <para>Represent the configuration of the widget, the configuration manage the way the widget is visualized on the dashboard.</para>
    /// <para>To change the configuration you have to change the widget properties on the related mongodb.</para>
    /// <seealso cref="Services.ConfigService.GetDashboardConfig"/>
    /// </summary>
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

        // Constructor for test method
        public WidgetComponent(string name)
        {
            WidgetName = name;
        }
        
    }
}
