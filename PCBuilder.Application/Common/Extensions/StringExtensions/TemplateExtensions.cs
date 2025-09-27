using PCBuidler.Domain.Enums;

namespace PCBuilder.Application.Common.Extensions.StringExtensions;

public static class TemplateExtensions
{
    public static string FillPlaceholders(this string template, Dictionary<EmailPlaceholders, string>? values)
    {
        if (template == null) throw new ArgumentNullException(nameof(template));
        if (values == null || values.Count == 0) return template;
        
        foreach (var kv in values)
        {
            template = template.Replace($"{{{kv.Key.ToString()}}}", kv.Value);
        }
        return template;
    }
}