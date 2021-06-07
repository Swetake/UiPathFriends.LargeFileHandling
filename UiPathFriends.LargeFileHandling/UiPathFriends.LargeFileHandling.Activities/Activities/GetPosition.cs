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
    [LocalizedDisplayName(nameof(Resources.GetPosition_DisplayName))]
    [LocalizedDescription(nameof(Resources.GetPosition_Description))]
    public class GetPosition : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.GetPosition_Position_DisplayName))]
        [LocalizedDescription(nameof(Resources.GetPosition_Position_Description))]
        [LocalizedCategory(nameof(Resources.Output_Category))]
        public OutArgument<long> Position { get; set; }

        #endregion


        #region Constructors

        public GetPosition()
        {
            Constraints.Add(ActivityConstraints.HasParentType<GetPosition, FileHandlingScope>(string.Format(Resources.ValidationScope_Error, Resources.FileHandlingScope_DisplayName)));
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            // Object Container: Use objectContainer.Get<T>() to retrieve objects from the scope
            var objectContainer = context.GetFromContext<IObjectContainer>(FileHandlingScope.ParentContainerPropertyTag);

            // Inputs
    
            ///////////////////////////
            // Add execution logic HERE
            ///////////////////////////

            // Outputs
            return (ctx) => {
                Position.Set(ctx, objectContainer.Get<FileStream>().Position);
            };
        }

        #endregion
    }
}

