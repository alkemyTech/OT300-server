using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;

namespace OngProject.Core.Helper
{
    public class ImageStorageHelper : IImageStorageHerlper
    {
        private readonly AWS3ConfigurationModel _credentialsConfig;

        public ImageStorageHelper(AWS3ConfigurationModel credentialsConfig)
        {
            _credentialsConfig = credentialsConfig;
        }

        public async Task<string> UploadImageAsync(FileStream imageFile)
        {
            var fileName = imageFile.Name.Normalize().Trim().ToLower();

            var credentials = new BasicAWSCredentials(_credentialsConfig.AWSAccessKey, _credentialsConfig.AWSSecretKey);

            var regionEndpoint = new AmazonS3Config()
            {
                //Todo: Complete when get the credentials
                // RegionEndpoint = Amazon.RegionEndpoint
            };

            var uploadRequest = new TransferUtilityUploadRequest()
            {
                InputStream = imageFile,
                Key = fileName,
                BucketName = _credentialsConfig.AWSBuctketName
            };

            using var amazonClient = new AmazonS3Client(credentials, regionEndpoint);

            var transferUtility = new TransferUtility(amazonClient);

            await transferUtility.UploadAsync(uploadRequest);

            var absolutePath =
                $"https://{_credentialsConfig.AWSBuctketName}.s3.{regionEndpoint}.amazonaws.com/{fileName}";

            return absolutePath;

        }

        public Task<string> UploadImageAsync(Stream imageFile, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}