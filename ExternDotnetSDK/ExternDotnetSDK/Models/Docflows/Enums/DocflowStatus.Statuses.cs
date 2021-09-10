using System.Diagnostics.CodeAnalysis;

namespace Kontur.Extern.Client.Models.Docflows.Enums
{
    [SuppressMessage("ReSharper", "CommentTypo")]
    partial struct DocflowStatus
    {
        /// <summary>
        /// отправлен
        /// </summary>
        public static readonly DocflowStatus Sent = "urn:docflow-common-status:sent";
        
        /// <summary>
        /// доставлен
        /// </summary>
        public static readonly DocflowStatus Delivered = "urn:docflow-common-status:delivered";
        
        /// <summary>
        /// получен ответ
        /// </summary>
        public static readonly DocflowStatus ResponseArrived = "urn:docflow-common-status:response-arrived";
        
        /// <summary>
        /// ответ обработан
        /// </summary>
        public static readonly DocflowStatus ResponseProcessed = "urn:docflow-common-status:response-processed";
        
        /// <summary>
        /// завершен
        /// </summary>
        public static readonly DocflowStatus Finished = "urn:docflow-common-status:finished";
    }
}