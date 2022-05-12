using System.Threading.Tasks;
using Shiny.Locations;
using Shiny.Notifications;

public class GeofenceDelegate : IGeofenceDelegate
    {
        readonly INotificationManager notificationManager;
        


        public GeofenceDelegate(INotificationManager notificationManager)
        {
            this.notificationManager = notificationManager;
            
        }


        public async Task OnStatusChanged(GeofenceState newStatus, GeofenceRegion region)
        {
            var state = newStatus.ToString().ToUpper();

           
            await this.notificationManager.Send(
                "Geofencing",
                $"You {state} the geofence"
            );
        }
    }