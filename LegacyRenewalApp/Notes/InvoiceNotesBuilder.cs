using System.Text;
using LegacyRenewalApp.Context;

namespace LegacyRenewalApp.Notes;

public class InvoiceNotesBuilder : INotesBuilder
{
    public string Build(params IContext[] contexts)
    {
        StringBuilder builder = new StringBuilder();
        foreach(IContext context in contexts)
        {
            builder.Append(context.GetNotes());
        }
        return builder.ToString();
    }
}