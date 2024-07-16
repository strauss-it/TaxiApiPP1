﻿using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageWrapper
{
    public class AzureBlobWrapper
    {
        private readonly BlobServiceClient blobServiceClient;
        private readonly BlobContainerClient blobContainerClient;
        private readonly string containerName;

        public AzureBlobWrapper(string storageUri, string containerName)
        {
            blobServiceClient = new BlobServiceClient(storageUri);
            this.containerName = containerName;
            blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            blobContainerClient.CreateIfNotExists();
        }

        public async Task<string> UploadBlob(string blobName, Stream blobContent)
        {
            var res = await blobContainerClient.UploadBlobAsync(blobName, blobContent);
            return res != null ? $"{blobContainerClient.Uri}/{blobName}" : null;
        }
    }
}
