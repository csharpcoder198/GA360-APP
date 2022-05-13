using System.Threading.Tasks;
using Shiny.Locations;
using Shiny.Notifications;

public class GeofenceDelegate : IGeofenceDelegate
    {
        private readonly INotificationManager _notificationManager;
        


        public GeofenceDelegate(INotificationManager notificationManager)
        {
            _notificationManager = notificationManager;
            
        }


        public async Task OnStatusChanged(GeofenceState newStatus, GeofenceRegion region)
        {
            var state = newStatus.ToString().ToUpper();

           
            await _notificationManager.Send(
                "Geofencing",
                $"You {state} the geofence"
            );
        }
    }