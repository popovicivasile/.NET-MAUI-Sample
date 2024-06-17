using Google.Cloud.Firestore;

namespace AIOService_v1.Models
{

    [FirestoreData]
    public class JohnDeere
    {

        [FirestoreProperty]
        public string Ecu { get; set; } = string.Empty;

        [FirestoreProperty]
        public string Error { get; set; } = string.Empty;

        [FirestoreProperty]
        public string Description { get; set; } = string.Empty;
    }

}
