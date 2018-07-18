using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace MyPortfolioWebApp.Controllers
{
    public static class BlobConnector
    {
        private static readonly CloudBlobContainer IconsContainer;
        private static CloudBlobContainer TempProjectImages;
        private static CloudBlobContainer ProjectImages;


        static BlobConnector()
        {
            //TODO: Nie ustawia automatycznie uprawnień na kontenerach na publiczne, przez co nie będzie mozna pobrać obrazka. Trzeba robić to ręcznie przy tworzeniu kontenera.
            var storageCredentials = new StorageCredentials("damiandziuraportfolio", "eL6FmFEJ8sp+zC3NRIfFY8rBg4cS1ySypJs8b+52p7OJv2si1+YiaARwZyp6GOzvrPH3EC89Op8rU4gYbB47+w==");
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            IconsContainer = cloudBlobClient.GetContainerReference("icons");
            IconsContainer.CreateIfNotExistsAsync();

            TempProjectImages = cloudBlobClient.GetContainerReference("tempprojectimages");
            TempProjectImages.CreateIfNotExistsAsync();

            ProjectImages = cloudBlobClient.GetContainerReference("projectimages");
            ProjectImages.CreateIfNotExistsAsync();
        }

        public static void UploadIcon(HttpPostedFileBase file, int projectId, bool temp)
        {
            string fileName;
            if (temp)
            {
                fileName = "Project" + projectId + "Icon.png";
            }else fileName = "TempProject" + projectId + "Icon.png";

            CloudBlockBlob cblob = IconsContainer.GetBlockBlobReference(fileName);
            cblob.UploadFromStream(file.InputStream);
        }

        public static void RemoveIcon(int projectId, bool temp)
        {
            string fileName;
            if (temp)
            {
                fileName = "Project" + projectId + "Icon.png";
            }
            else fileName = "TempProject" + projectId + "Icon.png";

            CloudBlockBlob cblob = IconsContainer.GetBlockBlobReference(fileName);
            cblob.DeleteIfExists();
        }
    }
}