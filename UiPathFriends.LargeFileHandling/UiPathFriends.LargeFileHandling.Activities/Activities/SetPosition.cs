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
    [LocalizedDisplayName(nameof(Resources.SetPosition_DisplayName))]
    [LocalizedDescription(nameof(Resources.SetPosition_Description))]
    public class SetPosition : ContinuableAsyncCodeActivity
    {
        #region Properties

        /// <summary>
        /// If set, continue executing the remaining activities even if the current activity has failed.
        /// </summary>
        [LocalizedCategory(nameof(Resources.Common_Category))]
        [LocalizedDisplayName(nameof(Resources.ContinueOnError_DisplayName))]
        [LocalizedDescription(nameof(Resources.ContinueOnError_Description))]
        public override InArgument<bool> ContinueOnError { get; set; }

        [LocalizedDisplayName(nameof(Resources.SetPosition_Position_DisplayName))]
        [LocalizedDescription(nameof(Resources.SetPosition_Position_Description))]
        [LocalizedCategory(nameof(Resources.Input_Category))]
        public InArgument<long> Position { get; set; }

        #endregion


        #region Constructors

        public SetPosition()
        {
            Constraints.Add(ActivityConstraints.HasParentType<SetPosition, FileHandlingScope>(string.Format(Resources.ValidationScope_Error, Resources.FileHandlingScope_DisplayName)));
        }

        #endregion


        #region Protected Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            if (Position == null) metadata.AddValidationError(string.Format(Resources.ValidationValue_Error, nameof(Position)));

            base.CacheMetadata(metadata);
        }

        protected override async Task<Action<AsyncCodeActivityContext>> ExecuteAsync(AsyncCodeActivityContext context, CancellationToken cancellationToken)
        {
            // Object Container: Use objectContainer.Get<T>() to retrieve objects from the scope
            var objectContainer = context.GetFromContext<IObjectContainer>(FileHandlingScope.ParentContainerPropertyTag);

            // Inputs
            var position = Position.Get(context);

            ///////////////////////////
            // Add execution logic HERE
            ///////////////////////////

            objectContainer.Get<FileStream>().Position = position;

            // Outputs
            return (ctx) => {
            };
        }

        #endregion
    }
}

