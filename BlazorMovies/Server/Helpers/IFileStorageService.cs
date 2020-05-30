using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Server.Helpers
{
    public interface IFileStorageService
    {
        Task<string> EditFile(byte[] content, string extension, string containerName, string fileRoute);
        Task DeleteFile(string fileRoute, string containerName);
        Task<string> SaveFile(byte[] content, string extension, string containerName);
        //containerName - folder in AzureStorage that will allow us to separate different fils in to categories f.e we can have a container that is called actprs
        //and that container will have the pictures of that actor and we can have a container that we call movie in which we can put the posters of the movies
    }
}
