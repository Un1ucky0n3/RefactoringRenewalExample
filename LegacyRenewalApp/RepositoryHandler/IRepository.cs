using System;

namespace LegacyRenewalApp.RepositoryHandler;

public interface IRepository
{
    Object GetByValue(Object value);
}