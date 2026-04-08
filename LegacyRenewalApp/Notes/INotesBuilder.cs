using LegacyRenewalApp.Context;

namespace LegacyRenewalApp.Notes;

public interface INotesBuilder
{
    public string Build(params IContext[] contexts);
}