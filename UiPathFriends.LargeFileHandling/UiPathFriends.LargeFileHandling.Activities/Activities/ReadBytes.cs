using System;
using System.Activities;
using System.Threading;
using System.Threading.Tasks;
using UiPathFriends.LargeFileHandling.Activities.Properties;
using UiPath.Shared.Activities;
using UiPath.Shared.Activities.Localization;
using UiPath.Shared.Activities.Utilities;
using System.IO;

namespace UiPathFriends.LargeFileHandling.Activities
{
    [LocalizedDisplayName(nameof(Resources.ReadBytes_DisplayName))]
    [LocalizedDescription(nameof(Resources.ReadBytes_Description))]
    public class ReadBytes : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.ReadBytes_Length_DisplayName))]
        [LocalizedDescription(nameof(Resources.ReadBytes_Length_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<int> Length { get; set; }

        [LocalizedDisplayName(nameof(Resources.ReadBytes_Result_DisplayName))]
        [LocalizedDescription(nameof(Resources.ReadBytes_Result_Description))]
        [LocalizedCategory(nameof(Resources.Output_Category))]
        public OutArgument<byte[]> Result { get; set; }

        #endregion


        #region Constructors

        public ReadBytes()
        {
            Constraints.Add(ActivityConstraints.HasParentType<ReadBytes, FileHandlingScope>(string.Format(Resources.ValidationScope_Error, Resources.FileHandlingScope_DisplayName)));
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (Length == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(Length)));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            // Object Container: Use objectContainer.Get<T>() to retrieve objects from the scope
            var objectContainer = context.GetFromContext<IObjectContainer>(FileHandlingScope.ParentContainerPropertyTag);

            // Inputs
            var length = Length.Get(context);

            ///////////////////////////
            // Add execution logic HERE
            ///////////////////////////
            Byte[] resultBytes=new byte[length];
            objectContainer.Get<FileStream>().Read(resultBytes, 0, length);

            // Outputs
            return (ctx) => {
                Result.Set(ctx, resultBytes);
            };
        }

        #endregion
    }
}

