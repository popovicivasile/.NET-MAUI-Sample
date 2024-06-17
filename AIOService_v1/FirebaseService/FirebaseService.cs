using System.Threading.Tasks;
using AIOService_v1.Models;
using Google.Cloud.Firestore;
public class FirebaseService
{
    FirestoreDb db;
    public async Task FirebaseServices()
    {
        var localPath = Path.Combine(FileSystem.CacheDirectory, "aioservice-1f01d-7a296c275ee0.json");

        using var json = await FileSystem.OpenAppPackageFileAsync("aioservice-1f01d-7a296c275ee0.json");
        using var dest = File.Create(localPath);
        await json.CopyToAsync(dest);

        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", localPath);
        dest.Close();
        db = FirestoreDb.Create("aioservice-1f01d");
    }


    public async Task<string> GetDataAsync(string error)
    {
        await FirebaseServices();
        CollectionReference collectionRef = db.Collection("JohnDeere");
        Query query = collectionRef.WhereEqualTo("Error", error);
        QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

        // Assuming you're retrieving a single item
        var document = querySnapshot.Documents.FirstOrDefault()?.ConvertTo<JohnDeere>();

        // Return the Description property of the document
        return document?.Description;
    }

}
