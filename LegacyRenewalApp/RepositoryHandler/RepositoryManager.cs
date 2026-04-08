using System;
using System.Collections.Generic;

namespace LegacyRenewalApp.RepositoryHandler;

public class RepositoryManager
{
    private readonly Dictionary<string, IRepository> _database;

    public RepositoryManager()
    {
        _database = new Dictionary<string, IRepository>()
        {
            { "customers", new CustomerRepository() },
            { "plans", new SubscriptionPlanRepository() },
        };
    }

    public IRepository GetRepository(string name)
    {
        if (_database.TryGetValue(name, out var repo))
            return repo;
        throw new ArgumentException($"Repository {name} not found");
    }
}