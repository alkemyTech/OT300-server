using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;

namespace OngProject.Core.Helper
{
    public class ImageStorageHelper : IImageStorageHerlper
    {
        private readonly AWS3ConfigurationModel _credentialsConfig;

        public ImageStorageHelper(IOptions<AWS3ConfigurationModel> credentialsConfig)
        {
            _credentialsConfig = credentialsConfig.Value;
        }

        public async Task<string> UploadImageAsync(Stream imageFile, string fileName)
        {

            var credentials = new BasicAWSCredentials(_credentialsConfig.AWSAccessKey, _credentialsConfig.AWSSecretKey);

            var regionEndpoint = new AmazonS3Config()
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast1
            };

            var uploadRequest = new TransferUtilityUploadRequest()
            {
                InputStream = imageFile,
                Key = fileName,
                BucketName = _credentialsConfig.AWSBucketName,
                CannedACL = new S3CannedACL("public-read")
            };

            using var amazonClient = new AmazonS3Client(credentials, regionEndpoint);

            var transferUtility = new TransferUtility(amazonClient);

            await transferUtility.UploadAsync(uploadRequest);

            var absolutePath =
                $"https://{_credentialsConfig.AWSBucketName}.s3.amazonaws.com/{fileName}";

            return absolutePath;

        }


    }
}