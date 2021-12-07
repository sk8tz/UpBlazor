﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marten;
using UpBlazor.Core.Models;
using UpBlazor.Core.Repositories;

namespace UpBlazor.Infrastructure.Repositories
{
    public class RecurringExpenseRepository : GenericRepository<RecurringExpense>, IRecurringExpenseRepository
    {
        public RecurringExpenseRepository(IDocumentStore store) : base(store) { }

        public async Task<RecurringExpense> GetByIdAsync(Guid id)
        {
            var session = Store.QuerySession();
            await using var _ = session.ConfigureAwait(false);

            return await session.Query<RecurringExpense>()
                .SingleOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
        }

        public async Task<IReadOnlyList<RecurringExpense>> GetAllByUserIdAsync(string userId)
        {
            var session = Store.QuerySession();
            await using var _ = session.ConfigureAwait(false);

            return await session.Query<RecurringExpense>()
                .Where(x => x.UserId == userId)
                .ToListAsync().ConfigureAwait(false);
        }
    }
}