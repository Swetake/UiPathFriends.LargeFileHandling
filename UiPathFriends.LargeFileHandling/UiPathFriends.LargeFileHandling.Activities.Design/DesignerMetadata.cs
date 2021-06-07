using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using System.ComponentModel.Design;
using UiPathFriends.LargeFileHandling.Activities.Design.Designers;
using UiPathFriends.LargeFileHandling.Activities.Design.Properties;

namespace UiPathFriends.LargeFileHandling.Activities.Design
{
    public class DesignerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            var builder = new AttributeTableBuilder();
            builder.ValidateTable();

            var categoryAttribute = new CategoryAttribute($"{Resources.Category}");

            builder.AddCustomAttributes(typeof(FileHandlingScope), categoryAttribute);
            builder.AddCustomAttributes(typeof(FileHandlingScope), new DesignerAttribute(typeof(FileHandlingScopeDesigner)));
            builder.AddCustomAttributes(typeof(FileHandlingScope), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(GetPosition), categoryAttribute);
            builder.AddCustomAttributes(typeof(GetPosition), new DesignerAttribute(typeof(GetPositionDesigner)));
            builder.AddCustomAttributes(typeof(GetPosition), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(SetPosition), categoryAttribute);
            builder.AddCustomAttributes(typeof(SetPosition), new DesignerAttribute(typeof(SetPositionDesigner)));
            builder.AddCustomAttributes(typeof(SetPosition), new HelpKeywordAttribute(""));

            builder.AddCustomAttributes(typeof(ReadBytes), categoryAttribute);
            builder.AddCustomAttributes(typeof(ReadBytes), new DesignerAttribute(typeof(ReadBytesDesigner)));
            builder.AddCustomAttributes(typeof(ReadBytes), new HelpKeywordAttribute(""));


            MetadataStore.AddAttributeTable(builder.CreateTable());
        }
    }
}
