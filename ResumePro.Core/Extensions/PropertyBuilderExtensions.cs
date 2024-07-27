using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResumePro.Core.Extensions
{
    public enum StringColumnSize : int
    {
        VerySmall = 64,
        Small = 255,
        Medium = 512,
        Large = 1024,
        VeryLarge = 8000
    }
    
    public static class PropertyBuilderExtensions
    {
        public static PropertyBuilder<string> ConfigureColumn(
            this PropertyBuilder<string> propertyBuilder, StringColumnSize size, bool required = true)
        {
            var columnSize = "varchar(max)";
            if (size == StringColumnSize.VerySmall)
            {
                columnSize = "varchar(64)";
            }
            else if (size == StringColumnSize.Small)
            {
                columnSize = "varchar(255)";
            }
            else if (size == StringColumnSize.Medium)
            {
                columnSize = "varchar(512)";
            }
            else if (size == StringColumnSize.Large)
            {
                columnSize = "varchar(1024)";
            }
            else if (size == StringColumnSize.VeryLarge)
            {
                columnSize = "varchar(8000)";
            }

            propertyBuilder.HasColumnType(columnSize);
            propertyBuilder.IsRequired(required);
            
            return propertyBuilder;
        }
    }
}
