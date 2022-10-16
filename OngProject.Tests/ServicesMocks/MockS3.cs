using OngProject.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngProject.Tests.ServicesMocks
{
    internal class MockS3 : IImageStorageHerlper
    {
        public Task<string> UploadImageAsync(Stream imageFile, string fileName)
        {
            return  Task.FromResult("url-mocked-" + Guid.NewGuid());
        }
    }
}
